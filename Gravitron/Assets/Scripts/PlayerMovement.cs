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

    // Variables for flipping sprite
    [HideInInspector]
    public bool isFacingLeft;
    public bool isUpsideDown;
    public bool spawnFacingLeft = false;
    private Vector2 upsideDown;
    private Vector2 facingLeft;


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

    // Start is called before the first frame update
    void Start()
    {
        rgb = transform.GetComponent<Rigidbody2D>();
        horizontalMovement = Vector3.zero;
        playerGravityDown = true;

        // handles flipping the characters direction
        facingLeft = new Vector2(-transform.localScale.x, transform.localScale.y);
        upsideDown = new Vector2(transform.localScale.x, -transform.localScale.y);
        if(spawnFacingLeft)
        {
            transform.localScale = facingLeft;
            isFacingLeft = true;
        }
        isUpsideDown = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   

    void FixedUpdate()
    {
        // Get the inputs from the player (A, D, and left and right arrows)
        float horizontalInputs = Input.GetAxis("Horizontal");

        // sets the Speed for the animator paramater so that the player switches to
        // running when running
        animator.SetFloat("Speed", Mathf.Abs(horizontalInputs));

        // this put the character in the shooting state when the r key is pressed and then 
        // calls a corouting to make sure that the animation plays in full
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


        /*
        Very Broken!!
        */

        // Flips the character based on movement direction
        if (playerGravityDown && isUpsideDown)
        {
            isUpsideDown = false;
            FlipVertical();
        }
        if (!playerGravityDown && !isUpsideDown)
        {
            isUpsideDown = true;
            FlipVertical();
        }

        // Flips the character based on movement direction
        if (
            (horizontalInputs > 0 && isFacingLeft && !isUpsideDown) ||
            (horizontalInputs < 0 && !isFacingLeft && isUpsideDown))
        {
            isFacingLeft = false;
            FlipHorizontal();
        }
        if (
            (horizontalInputs < 0 && !isFacingLeft && !isUpsideDown) ||
            (horizontalInputs > 0 && isFacingLeft && isUpsideDown))
        {
            isFacingLeft = true;
            FlipHorizontal();
        }




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
        yield return new WaitForSeconds(1);

        isShooting = false;
        animator.SetBool("Shooting", isShooting);
    }

    // Function for flipping the character's horizontal direction 
    protected virtual void FlipHorizontal()
    {
        if (isFacingLeft)
        {
            transform.localScale = facingLeft;
        }
        if (!isFacingLeft)
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }

    // Function for flipping the character's vertical direction
    protected virtual void FlipVertical()
    {
        if (isUpsideDown)
        {
            transform.localScale = upsideDown;
        }
        if (!isUpsideDown)
        {
            transform.localScale = new Vector2(transform.localScale.x, -transform.localScale.y);
        }
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
}
