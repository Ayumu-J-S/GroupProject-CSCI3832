using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpikesFinal : MonoBehaviour
{
    private Animator spikesAnim;

    // Start is called before the first frame update
    void Start()
    {
        GameObject Spikes = transform.gameObject;
        spikesAnim = Spikes.GetComponent<Animator>();
        StartCoroutine("moveSpikes");
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator moveSpikes()
    {
        while(true)
        {
            spikesAnim.Play("FinalSpikesIdle");
            yield return new WaitForSeconds(1);
            spikesAnim.Play("FinalSpikesUp");
            yield return new WaitForSeconds(2.8f);
            spikesAnim.Play("FinalSpikesDown");
            yield return new WaitForSeconds(0.25f);
        }
    }
}
