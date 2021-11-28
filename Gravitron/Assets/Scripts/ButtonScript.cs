using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public GameObject Door;
    public static bool buttonPressed = false;
    public AudioClip doorAudio;
    public AudioClip buttonAudio;
    private Animator animDoor;
    private Animator animButton;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        animDoor = Door.GetComponent<Animator>();
        animButton = transform.GetComponent<Animator>();
        audioSource = transform.GetComponent<AudioSource>();
    }

    IEnumerator StartButtonDoorAudio()
    {
        audioSource.PlayOneShot(buttonAudio);
        yield return new WaitForSeconds(0.1f);
        audioSource.PlayOneShot(doorAudio);

    }

    private void OnTriggerEnter2D(Collider2D other)
    { 
        animButton.Play("Press");
        animDoor.Play("Open");

        if (!buttonPressed)
        {
            StartCoroutine("StartButtonDoorAudio");
        }


        buttonPressed = true;

        if (PlayerMovement.playerDead)
        {
            buttonPressed = false;
        }
    }

}

