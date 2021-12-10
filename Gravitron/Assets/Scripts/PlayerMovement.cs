using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Camera camera;
    private Rigidbody2D rgb;
    private Vector3 horizontalMovement;
    public static bool playerDead = false;

    // variables for animation
    public Animator animator;
    public bool isShooting = false;

    // The inputs for player movement (public for cutscene manipulation)
    public float horizontalInputs = 0.0f;

    // Affects player's horizontal speed
    float movementSpeed = 0.2f;

    // Affects player's jump height
    public float jumpForce = 350.0f;

    // Affects player's fall speed
    private float gravityScale = 2.0f;

    // Tells whether or not player is currently standing on solid ground
    bool onGround;

    // NOTE: Made this public because we can probably reference it in the ball script to know when to flip the balls' gravity
    public bool playerGravityDown;

    // The ball prefab
    private GameObject ballPrefab;

    // The ball object in the scene
    private GameObject ball;

    // Tells which direction the character is facing
    public Vector3 characterScale;

    // The particle system component for death animation
    private GameObject deathParticles;

    // This object's Renderer component
    Renderer renderer;

    // This object's Collider component
    Collider2D playerCollider;

    // Audio
    public AudioClip shootAudio;
    public AudioClip gravityFlipAudio;
    public AudioClip spikeAudio;
    private AudioSource audioSource;

    // Variable to compare with the current scale of y in update
    private float previousScaleY;

    // Disables player controls when in cutscenes
    public bool isInCutscene = false;

    // Enables shooting for part of outro cutscene
    public bool shootInOutro = false;

    // Start is called before the first frame update
    void Start()
    {
        // Get this object's Rigidbody component
        rgb = transform.GetComponent<Rigidbody2D>();

        // Get this object's Renderer component
        renderer = transform.GetComponent<Renderer>();

        // Get this object's Collider component
        playerCollider = transform.GetComponent<Collider2D>();

        // Initialize horizontal movement vector
        horizontalMovement = Vector3.zero;

        // Initialize player gravity direction and scale
        playerGravityDown = true;
        rgb.gravityScale = gravityScale;

        // Get the ball prefab
        ballPrefab = (UnityEngine.GameObject) Resources.Load("BallPrefab");

        // Get the particle effect prefab
        deathParticles = (UnityEngine.GameObject) Resources.Load("Death Particles");

        // Get AudioSource component
        audioSource = transform.GetComponent<AudioSource>();

        // Get the current scale of the Y
        previousScaleY = transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        float currentScaleY = transform.localScale.y;
        if (currentScaleY != previousScaleY)
        {
            audioSource.PlayOneShot(gravityFlipAudio);
        }
        previousScaleY = currentScaleY;
    }

    void FixedUpdate()
    {
        characterScale = transform.localScale;

        // Get the inputs from the player (A, D, and left and right arrows)
        if (!isInCutscene)
        {
            horizontalInputs = Input.GetAxis("Horizontal");
        }

        // sets the Speed for the animator paramater so that the player switches to
        // running when running
        animator.SetFloat("Speed", Mathf.Abs(horizontalInputs));

        // this put the character in the shooting state when the r key is pressed and then 
        // calls a coroutine to make sure that the animation plays in full
        if(Input.GetKey(KeyCode.R) && isShooting == false && (isInCutscene == false || shootInOutro == true))
        {
            isShooting = true;
            animator.SetBool("Shooting", isShooting);
            StartCoroutine(ShootRoutine());
        }

        // switches to falling state if the character is in the air
        if(!onGround)
        {
            animator.SetBool("Falling", true);
        }
        else
        {
            animator.SetBool("Falling", false);
        }


        // Set the horizontal movement
        horizontalMovement.x = movementSpeed * horizontalInputs;

        // Add movement to position
        transform.position += horizontalMovement;

        // Switch gravity if up or down arrows are pressed
        if (Input.GetKey(KeyCode.DownArrow) && !isInCutscene)
        {
            playerGravityDown = true;
            rgb.gravityScale = gravityScale;
        }
        if (Input.GetKey(KeyCode.UpArrow) && !isInCutscene)
        {
            playerGravityDown = false;
            rgb.gravityScale = -gravityScale;
        }


        // Flips the character based on movement direction

        // Flips horizontal direction based on horizontalInputs
        if(horizontalInputs < 0 && !isInCutscene)
        {
            characterScale.x = -0.3f;
        }
        if(horizontalInputs > 0 && !isInCutscene)
        {
            characterScale.x = 0.3f;
        }

        // Flips vertical direction based on direction of gravity
        if(playerGravityDown && !isInCutscene)
        {
            characterScale.y = 0.3f;
        }
        if(!playerGravityDown && !isInCutscene)
        {
            characterScale.y = -0.3f;
        }

        // Applies direction changes to character
        transform.localScale = characterScale;


        // Jump if space pressed and player is on the ground (prevents jumping midair)
        if (Input.GetKey(KeyCode.Space) && onGround && !isInCutscene)
        {
            if (playerGravityDown)
            {
                rgb.AddForce(Vector2.up * jumpForce);
            }
            else
            {
                rgb.AddForce(Vector2.down * jumpForce);
            }
            onGround = false;
        }
    }

    //  Make sure the shoooting animation plays completely before stopping
    private IEnumerator ShootRoutine()
    {
        // Wait until halfway through the animation
        yield return new WaitForSeconds(0.5f);

        // Destroy the ball that has already been shot
        Destroy(ball);

        // Get position for the ball to spawn in (based on where character is facing)
        Vector3 ballPosition = transform.position;
        ballPosition.x += characterScale.x * 2.5f;

        audioSource.PlayOneShot(shootAudio);

        // Shoot the ball
        ball = Instantiate(ballPrefab, ballPosition, ballPrefab.transform.rotation);

        // Wait for the rest of the animation to play
        yield return new WaitForSeconds(0.5f);

        isShooting = false;
        animator.SetBool("Shooting", isShooting);
    }


    // When colliding with another collider
    void OnCollisionStay2D(Collision2D collider)
    {
        CheckForGround();
    }

    // When no longer colliding with another collider
    void OnCollisionExit2D(Collision2D collider)
    {
        onGround = false;
    }

    // Check to see if there is a collider (ground) underneath the player
    private void CheckForGround()
    {
        // The collider hit by the raycast
        RaycastHit2D collider;

        Vector2 playerPosition = transform.position;

        // Raycast direction depends on whether gravity is going up or down for the player
        if (playerGravityDown)
        {
            // Raycast above player to find collider directly below
            collider = Physics2D.Raycast(playerPosition, Vector2.down, 1.15f);
        }
        else
        {
            // Raycast above player to find collider directly above
            collider = Physics2D.Raycast(playerPosition, Vector2.up, 1.15f);
        }

        // If a collider is found, there is ground under the player
        if (collider)
        {
            onGround = true;
        }
        else
        {
            onGround = false;
        }
    }

    // Calls the respawn coroutine
    public void Die()
    {
        audioSource.PlayOneShot(spikeAudio);
        StartCoroutine("Respawn");
    }

    // The player's death animation and respawn when an obstacle is hit
    public IEnumerator Respawn()
    {
        // Instantiate the particle effect
        ParticleSystem deathParticleAnimation = Instantiate(deathParticles, transform.position, transform.rotation).GetComponent<ParticleSystem>();

        // Make the player disappear
        renderer.enabled = false;

        // Disable player collision
        playerCollider.enabled = false;

        // Destroy the ball
        Destroy(ball);

        // Play the particle effect
        deathParticleAnimation.Play();

        // Wait
        yield return new WaitForSeconds(1);

        //This will be seen in the levelLoader script and scene will reload
        playerDead = true;

        // Destroy particle effect object
        Destroy(deathParticleAnimation.gameObject);

    }
}
