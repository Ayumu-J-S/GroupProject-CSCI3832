using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script defines the AIAgent's behaviour
// The AIAgent moves at a predetermined speed towards the player and kills him upon impact
public class AIAgent : MonoBehaviour
{

    // speed of AI agent
    public float speed = 0.04f;

    // holds the Player object in order to access player's position
    private GameObject player;
    
    // holds player's and agent's positions 
    private Vector2 playerLocation;
    private Vector2 agentLocation;

    // on start, find the Player game object and store it in 'player'
    void Start()
    {
        player = GameObject.Find("Player");
    }

    void FixedUpdate()
    {
        // get current player and agent locations
        playerLocation = player.transform.position;
        agentLocation = this.transform.position;

        // calculate the difference between the player and the agents x and y position
        // use difference to calculate a new agent location that is closer to the player
        if(playerLocation.x - agentLocation.x > 0)
        {
            agentLocation.x += speed;
        }
        else
        {
            agentLocation.x -= speed;
        }

        if (playerLocation.y - agentLocation.y > 0)
        {
            agentLocation.y += speed;
        }
        else
        {
            agentLocation.y -= speed;
        }

        // adjust the agent's location
        this.transform.position = agentLocation;


    }
}
