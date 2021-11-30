using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpikesWithButton : MonoBehaviour
{
    public GameObject SpikesUp;
    public GameObject SpikesDown;
    private Animator spikesUpAnim;
    private Animator spikesDownAnim;

    // Start is called before the first frame update
    void Start()
    {
        //Get the animator of the spikes
        spikesUpAnim = SpikesUp.GetComponent<Animator>();
        spikesDownAnim = SpikesDown.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Play the animation if the button is pressed
        if (ButtonScript.buttonPressed)
        {
            spikesUpAnim.Play("MoveSpikesUp");
            spikesDownAnim.Play("MoveSpikesDown");
        }
    }
}
