using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script creates and keeps track of AIAgents active in the current scene
// At the start of the scene it creates a single agent and then creates new ones
// everytime the player changes the direction of gravity
public class AIAgentControl : MonoBehaviour
{
    // this variable determines whether or not the AIAgent will duplicate
    // if only one AIAgent is desired, set this to false
    public bool duplicate = true;

    // this variable determines the max number of AIAgents that can be 
    // created with a given AIAgentController
    public int maxAgentNum = 5;

    // this variable keeps track of how many AIAgents are currently 
    // active in order to guarantee only a given number are created
    private int currentAgentNum;
    
    // state variable to create a cooldown between spawning agents
    private bool inCooldown = false;

    // holds the AIAgent prefab
    private GameObject agentPrefab;

    // array of currently active agents
    private GameObject[] agentList;

    void Start()
    {
        currentAgentNum = 0;

        // on start instantiate the AIAgent array, load the AIAgent prefab
        // and instantiate the first AIAgent
        agentList = new GameObject[maxAgentNum];
        agentPrefab = (UnityEngine.GameObject)Resources.Load("AIAgent");
        agentList[0] = Instantiate(agentPrefab, this.transform.position, Quaternion.identity);
 
    }

    void Update()
    {
        // on update, the player is switching gravity, and the maximum number of AIAgents has 
        // not been reached and duplication is allowed,
        // instantiates a new agent, increments the number of agents and starts the cooldown
        if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.UpArrow)) 
            && (currentAgentNum < maxAgentNum-1)
            && (inCooldown == false)
            && (duplicate == true))
        {
            inCooldown = true;
            currentAgentNum++;
            agentList[currentAgentNum] = Instantiate(agentPrefab, this.transform.position, Quaternion.identity);
            StartCoroutine(AgentCooldown());
        }
       
    }

    // coroutine to impose a cooldown timer on agent duplication
    private IEnumerator AgentCooldown()
    {
        yield return new WaitForSeconds(.5f);
        inCooldown = false;
    }

}
