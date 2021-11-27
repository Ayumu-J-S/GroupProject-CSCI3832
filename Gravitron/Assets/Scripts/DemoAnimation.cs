using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoAnimation : MonoBehaviour
{
    public GameObject SpikesUp;
    public GameObject SpikesDown;
    public GameObject Button;
    private Animator spikesUpAnim;
    private Animator spikesDownAnim;
    private Animator animButton;
    private bool shown = false;


    //Plays the demo 3 time with the interval of seconds 
    IEnumerator PlayDemo(float seconds)
    {
        for (int i = 0; i < 5; i++)
        {
            animButton.Play("Press");
            spikesUpAnim.Play("MoveSpikesUp");
            spikesDownAnim.Play("MoveSpikesDown");
            yield return new WaitForSeconds(seconds);
            animButton.Play("Idle");
            spikesUpAnim.Play("Idle");
            spikesDownAnim.Play("IdleDown");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        spikesUpAnim = SpikesUp.GetComponent<Animator>();
        spikesDownAnim = SpikesDown.GetComponent<Animator>();
        animButton = Button.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!shown)
        {
            StartCoroutine(PlayDemo(1f));
            shown = true;
        }
    }
}
