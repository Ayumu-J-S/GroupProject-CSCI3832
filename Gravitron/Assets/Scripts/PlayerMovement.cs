using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    /* Up and down arrows switch gravity, left and right arrows move (A and D also do this,
     * if we want to change that we'll have to change the Axis), and space jumps
     */
    private Rigidbody2D rgb;
    private Vector3 horizontalMovement;
    float movementSpeed = 0.1f;
    float jumpForce = 150.0f;
    private float gravityScale = 1.0f;
    bool onGround;
    bool gravityDown;

    // Start is called before the first frame update
    void Start()
    {
        rgb = transform.GetComponent<Rigidbody2D>();
        horizontalMovement = Vector3.zero;
        gravityDown = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        // Get the inputs from the player (A, D, and left and right arrows)
        float horizontalInputs = Input.GetAxis("Horizontal");

        // Set the horizontal movement
        horizontalMovement.x = movementSpeed * horizontalInputs;

        // Add movement to position
        transform.position += horizontalMovement;

        // Switch gravity if up or down arrows are pressed
        if (Input.GetKey(KeyCode.DownArrow))
        {
            gravityDown = true;
            rgb.gravityScale = gravityScale;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            gravityDown = false;
            rgb.gravityScale = -gravityScale;
        }

        // Jump if space pressed and player is on the ground (prevents jumping midair)
        if (Input.GetKey(KeyCode.Space) && onGround)
        {
            if (gravityDown)
            {
                rgb.AddForce(Vector2.up * jumpForce);
            }
            else
            {
                rgb.AddForce(Vector2.down * jumpForce);
            }
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
        if (gravityDown)
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
