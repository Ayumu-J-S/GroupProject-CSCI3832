using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CutsceneAnimation : MonoBehaviour
{

    // variables for animation
    public Animator animator;
    public bool isStanding = true;
    public bool inTransition = false;

    // Variables for speech box (Note: This requires that a TextMeshPro called "Speech Text" is in the current scene)
    GameObject speechTextObject;
    TextMeshProUGUI speechText;


    // Start is called before the first frame update
    void Start()
    {
        // Get the text mesh
        speechTextObject = GameObject.Find("Speech Text");
        speechText = speechTextObject.GetComponent<TextMeshProUGUI>();

        // Start the cutscene
        StartCoroutine("Cutscene");
    }

    void FixedUpdate()
    {
        // Switch between the dogs sit and stand animations
        // this is tied to keyboard input for testing
        // but can be changed so that the transition happens at different times
        if (Input.GetKey(KeyCode.S) && !inTransition)
        {

            inTransition = true;
            animator.SetBool("InTransition", inTransition);

            if (isStanding)
            {
                isStanding = !isStanding;
                StartCoroutine(SitRoutine());
                animator.SetBool("Standing", isStanding);
            }
            else
            {
                isStanding = !isStanding;
                StartCoroutine(SitRoutine());
                animator.SetBool("Standing", isStanding);

            }

        }

    }

    //  Make sure the shoooting animation plays completely before stopping
    private IEnumerator SitRoutine()
    {
        // Wait for the rest of the animation to play
        yield return new WaitForSeconds(.5f);

        inTransition = false;
        animator.SetBool("InTransition", inTransition);
    }

    private IEnumerator StandRoutine()
    {
        // Wait for the rest of the animation to play
        yield return new WaitForSeconds(.5f);

        inTransition = false;
        animator.SetBool("InTransition", inTransition);
    }

    // The cutscene can be controlled from here
    IEnumerator Cutscene()
    {
        // Set speech text, check to see if space has been hit, and then wait for a second (to avoid zooming through)
        speechText.text = "Hit space!";
        yield return hitSpace();
        speechText.text = "Thanks! Hit space again!!";
        yield return hitSpace();
        speechText.text = "You follow rules really well.";
        yield return hitSpace();
        speechText.text = "Well that's it for this placeholder text!";

    }

    // Waits until the space key has been hit
    private IEnumerator hitSpace()
    {
        while (!Input.GetKeyDown(KeyCode.Space))
        {
            yield return null;
        }

        // Wait to prevent one key press spanning multiple frames as counting for multiple key presses
        yield return new WaitForSeconds(0.2f);
    }

}
