using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        // If collided with player
        if (col.gameObject.name == "Player")
        {
            // Get the player's script
            PlayerMovement playerScript = col.gameObject.GetComponent<PlayerMovement>();

            // Kill and respawn the player
            playerScript.Die();
        }
    }
}
