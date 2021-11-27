using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    /* Up and down arrows switch gravity, left and right arrows move (A and D also do this,
     * if we want to change that we'll have to change the Axis), and space jumps
     */
    public Camera camera;
    private Rigidbody2D rgb;
    private Vector3 horizontalMovement;
    public static bool playerDead = false;

    // variables for animation
    public Animator animator;
    public bool isShooting = false;

    // Affects player's horizontal speed
    float movementSpeed = 0.2f;

    // Affects player's jump height
    float jumpForce = 150.0f;

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

    // Start is called before the first frame update
    void Start()
    {
        // Get this object's Rigidbody component
        rgb = transform.GetComponent<Rigidbody2D>();

        // Get this object's Renderer component
        renderer = transform.GetComponent<Renderer>();

        // Initialize horizontal movement vector
        horizontalMovement = Vector3.zero;

        // Initialize player gravity direction and scale
        playerGravityDown = true;
        rgb.gravityScale = gravityScale;

        // Get the ball prefab
        ballPrefab = (UnityEngine.GameObject) Resources.Load("BallPrefab");

        // Get the particle effect prefab
        deathParticles = (UnityEngine.GameObject) Resources.Load("Death Particles");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        // NOTE: I made this a public variable so I can access it in the ball's script to know which way
        // the character is facing (so the ball shoots in the right direction) - Theresa
        characterScale = transform.localScale;

        // Get the inputs from the player (A, D, and left and right arrows)
        float horizontalInputs = Input.GetAxis("Horizontal");

        // sets the Speed for the animator paramater so that the player switches to
        // running when running
        animator.SetFloat("Speed", Mathf.Abs(horizontalInputs));

        // this put the character in the shooting state when the r key is pressed and then 
        // calls a coroutine to make sure that the animation plays in full
        if(Input.GetKey(KeyCode.R) && isShooting == false)
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
        if (Input.GetKey(KeyCode.DownArrow))
        {
            playerGravityDown = true;
            rgb.gravityScale = gravityScale;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            playerGravityDown = false;
            rgb.gravityScale = -gravityScale;
        }


        // Flips the character based on movement direction

        // Flips horizontal direction based on horizontalInputs
        if(horizontalInputs < 0)
        {
            characterScale.x = -0.3f;
        }
        if(horizontalInputs > 0)
        {
            characterScale.x = 0.3f;
        }

        // Flips vertical direction based on direction of gravity
        if(playerGravityDown)
        {
            characterScale.y = 0.3f;
        }
        if(!playerGravityDown)
        {
            characterScale.y = -0.3f;
        }

        // Applies direction changes to character
        transform.localScale = characterScale;


        // Jump if space pressed and player is on the ground (prevents jumping midair)
        if (Input.GetKey(KeyCode.Space) && onGround)
        {
            if (playerGravityDown)
            {
                rgb.AddForce(Vector2.up * jumpForce);
            }
            else
            {
                rgb.AddForce(Vector2.down * jumpForce);
            }
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

        /* DELETE THIS COMMENT LATER
         * The raycasting may cause issues depending on the origin point of our character sprite's position
         * if this script is put on the player and suddenly the jumping doesn't work that could be the cause
         */

        // Raycast direction depends on whether gravity is going up or down for the player
        if (playerGravityDown)
        {
            // Raycast below player to find collider within a distance of 0.01 (directly below)
            collider = Physics2D.Raycast(playerPosition, new Vector2(0, -1), 0.01f);
        }
        else
        {
            // Raycast above player to find collider within a distance of 0.01 (directly above)
            collider = Physics2D.Raycast(playerPosition, new Vector2(0, 1), 0.01f);
        }

        // If a collider is found, there is ground under the player
        if (collider)
        {
            onGround = true;
        }
    }

    // Calls the respawn coroutine
    public void Die()
    {
        StartCoroutine("Respawn");
    }

    // The player's death animation and respawn when an obstacle is hit
    public IEnumerator Respawn()
    {
        // Instantiate the particle effect
        ParticleSystem deathParticleAnimation = Instantiate(deathParticles, transform.position, transform.rotation).GetComponent<ParticleSystem>();

        // Make the player disappear
        renderer.enabled = false;

        // Destroy the ball
        Destroy(ball);

        // Play the particle effect
        deathParticleAnimation.Play();

        Debug.Log("a");

        // Wait
        yield return new WaitForSeconds(1);

        //This will be seen in the levelLoader script and scene will reload
        playerDead = true;

        Debug.Log("b");

        // Respawn the player
        //transform.position = new Vector3(-12f, -3f, 0);

        Debug.Log("c");

        // Make player visible again
        //renderer.enabled = true;

        Debug.Log("d");

        // Destroy particle effect object
        Destroy(deathParticleAnimation.gameObject);

        Debug.Log("e");
    }
}
