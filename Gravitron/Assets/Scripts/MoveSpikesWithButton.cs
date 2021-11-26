using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpikesWithButton : MonoBehaviour
{
    public GameObject SpikesUp;
    public GameObject SpikesDown;

    // Start is called before the first frame update
    void Start()
    {
        Animator spikesUpAnim = SpikesUp.GetComponent<Animator>();
        Animator spikesDownAnim = SpikesUp.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
