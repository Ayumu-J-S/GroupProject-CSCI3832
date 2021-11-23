using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    // The force of the bullet's gravity
    private float gravityScale;

    // Tells whether gravity is up or down for the ball
    private bool ballGravityDown;

    // The script object attached to the player
    PlayerMovement playerScript;

    // The Rigidbody attached to this ball
    private Rigidbody2D rgb;

    // The direction the ball is being shot in (negative is left, positive is right)
    private float direction;

    // The force at which the ball is shot
    public float shootingForce = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        // Get the ball's Rigidbody component
        rgb = transform.GetComponent<Rigidbody2D>();

        // Get the gravity scale
        gravityScale = rgb.gravityScale;

        // Get the player
        GameObject player = GameObject.Find("Player");

        // Get the player's script
        playerScript = player.GetComponent<PlayerMovement>();

        // Set the ball's gravity opposite that of the player
        ballGravityDown = !playerScript.playerGravityDown;

        // Reverse the gravity scale if in the wrong direction
        if(!ballGravityDown)
        {
            rgb.gravityScale = -1.0f * gravityScale;
        }

        // Get the direction the character is facing
        direction = playerScript.characterScale.x;

        Debug.Log(direction);

        // If the character is facing right
        if (direction > 0)
        {
            rgb.AddForce(Vector2.right * shootingForce, ForceMode2D.Impulse);
        }
        else // the character is facing left
        {
            rgb.AddForce(Vector2.left * shootingForce, ForceMode2D.Impulse);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        // Set the ball gravity opposite to the player's gravity
        ballGravityDown = !playerScript.playerGravityDown;

        // Change the gravity scale based on the direction of the ball's gravity
        if (!ballGravityDown)
        {
            rgb.gravityScale = -1.0f * gravityScale;
        }
        else
        {
            rgb.gravityScale = 1.0f * gravityScale;
        }

    }
}
