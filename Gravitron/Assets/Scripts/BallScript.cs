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

        ballGravityDown = !playerScript.playerGravityDown;

        if(!ballGravityDown)
        {
            rgb.gravityScale = -1.0f * gravityScale;
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        ballGravityDown = !playerScript.playerGravityDown;
        if (!ballGravityDown)
        {
            rgb.gravityScale = -1.0f * gravityScale;
        }
        else
        {
            rgb.gravityScale = 1.0f * gravityScale;
        }

        Debug.Log(playerScript.playerGravityDown);
    }
}
