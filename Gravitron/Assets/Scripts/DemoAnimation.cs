using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DemoAnimation : MonoBehaviour
{
    public GameObject SpikesUp;
    public GameObject SpikesDown;
    public GameObject Button;
    public Light pointLight;
    public AudioClip buttonAudio;
    private Animator spikesUpAnim;
    private Animator spikesDownAnim;
    private Animator animButton;
    private AudioSource audioSource;
    private bool shown = false;

    private static DemoAnimation demoAnimation;


    //Plays the demo 3 time with the interval of seconds 
    IEnumerator PlayDemo(float seconds)
    {
        for (int i = 0; i < 3; i++)
        {
            animButton.Play("Press");
            audioSource.PlayOneShot(buttonAudio);
            spikesUpAnim.Play("MoveSpikesUp");
            spikesDownAnim.Play("MoveSpikesDown");
            pointLight.intensity = 4;
            yield return new WaitForSeconds(seconds);
            pointLight.intensity = 0;
            animButton.Play("Idle");
            spikesUpAnim.Play("Idle");
            spikesDownAnim.Play("IdleDown");
            yield return new WaitForSeconds(seconds/2);
        }
        pointLight.intensity = 4;
    }

    // Start is called before the first frame update
    void Start()
    {
        spikesUpAnim = SpikesUp.GetComponent<Animator>();
        spikesDownAnim = SpikesDown.GetComponent<Animator>();
        animButton = Button.GetComponent<Animator>();
        audioSource = transform.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!shown)
        {
            StartCoroutine(PlayDemo(0.8f));
            shown = true;
        }
    }

    private void Awake()
    {
        // Get the name of the current scene

        // When the level first load there should be nothing assigned in
        // demoAnimation variable, so it should call this line
        if (demoAnimation == null)
        {
            //Assign the gameObject that this script is attached to
            demoAnimation = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            /* After the second time Awake() function is called,
             since there is already object attached to demoAnimation,
             delete the duplicated objects.*/
            Destroy(gameObject);
        }


    }
}

