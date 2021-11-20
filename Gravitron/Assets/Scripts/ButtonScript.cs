using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public GameObject Door;
    public static bool buttonPressed = false;
    private Animator animDoor;
    private Animator animButton;

    // Start is called before the first frame update
    void Start()
    {
        animDoor = Door.GetComponent<Animator>();
        animButton = transform.GetComponent<Animator>();
    }

    private void OnTriggerStay2D(Collider2D other)
    { 
        animButton.Play("Press");
        animDoor.Play("Open");
        buttonPressed = true;
    }

}

