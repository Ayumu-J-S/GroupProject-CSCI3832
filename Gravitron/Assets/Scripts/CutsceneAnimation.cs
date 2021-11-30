using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CutsceneAnimation : MonoBehaviour
{

    // variables for animation
    public Animator animator;
    public bool isStanding = true;
    public bool inTransition = false;

    // Variables for speech box (Note: This requires that a TextMeshPro called "Speech Text" is in the current scene)
    private GameObject speechTextObject;
    private TextMeshProUGUI speechText;
    private RawImage speakerSpriteObject;
    public Texture gravitronSprite;
    public Texture wretchedSprite;
    public Canvas dialogueOverlay;

    // Ball (for making door open)
    public GameObject ballPrefab;

    // In-game sprites
    public GameObject gravitron;
    public GameObject wretched;

    // Start is called before the first frame update
    void Start()
    {
        // Get the text mesh
        speechTextObject = GameObject.Find("Speech Text");
        speechText = speechTextObject.GetComponent<TextMeshProUGUI>();

        // Get sprites
        gravitronSprite = (UnityEngine.Texture)Resources.Load("Gravitron-Sprite");
        wretchedSprite = (UnityEngine.Texture)Resources.Load("Wretched-Sprite");

        // Get the speaker sprite object and set the intial sprite to Gravitron
        speakerSpriteObject = GameObject.Find("Speaker Sprite").GetComponent<RawImage>();
        speakerSpriteObject.texture = gravitronSprite;

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
        // Wait for scene to load in
        dialogueOverlay.enabled = false;
        yield return new WaitForSeconds(1.0f);
        dialogueOverlay.enabled = true;

        // Gravitron monologue
        yield return advanceDialogue(gravitronSprite, "Finally, my anti-gravity inventions have been completed!\n(Press Space to continue)");
        yield return advanceDialogue(gravitronSprite, "With these anti-gravity boots and anti-gravity ball shooter, I'll finally be respected in the science world!!");
        
        // Disable dialogue, open door, have Wretched come through, then resume dialogue
        dialogueOverlay.enabled = false;
        yield return new WaitForSeconds(0.5f);
        Instantiate(ballPrefab, new Vector2(-12.0f, -3.45f), transform.rotation);
        yield return new WaitForSeconds(1.0f);
        wretched.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        dialogueOverlay.enabled = true;

        // Conversation
        yield return advanceDialogue(wretchedSprite, "Gravitron! You're not going to pass off my work as your own anymore!!!");
        yield return advanceDialogue(gravitronSprite, "What are you talking about Wretched? Your research had nothing to do with anti-gravity.");
        yield return advanceDialogue(wretchedSprite, "Well I've devised some anti-gravity tech of my own!");
        yield return advanceDialogue(gravitronSprite, "(Press X to doubt)");
        yield return advanceDialogue(gravitronSprite, "Yeah I doubt that.");
        yield return advanceDialogue(wretchedSprite, "I've devised a horrible maze in the depths of my lab to test how well your little \"anti-gravity\" technology works!");
        yield return advanceDialogue(gravitronSprite, "Okay well...\nSounds sketchy. I'm not coming to try it.");
        yield return advanceDialogue(wretchedSprite, "Not even to save your mangy little mutt?");
        yield return advanceDialogue(gravitronSprite, "YOU STOLE MY DOG?!?!");
        yield return advanceDialogue(wretchedSprite, "MWAHAHAHA!!!\nI'll be waiting for you in my lab, if you have what it takes to save him.");

        dialogueOverlay.enabled = false;
        wretched.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        dialogueOverlay.enabled = true;

        yield return advanceDialogue(gravitronSprite, "Well shoot... I better go get Atom.");
        dialogueOverlay.enabled = false;

        //Move gravitron to the right and stop at the door
        gravitron.GetComponent<PlayerMovement>().horizontalInputs = 1.0f;
        yield return new WaitForSeconds(1.32f);
        gravitron.GetComponent<PlayerMovement>().horizontalInputs = 0f;
    }

    // Advance the dialogue
    private IEnumerator advanceDialogue(Texture speaker, string text)
    {
        // Set the text and speaker sprite
        speakerSpriteObject.texture = speaker;
        speechText.text = text;

        // Wait until space is hit to advance
        while (!(Input.GetKeyDown(KeyCode.Space) || (Input.GetKeyDown(KeyCode.X))))
        {
            yield return null;
        }

        // Wait to prevent one key press spanning multiple frames as counting for multiple key presses
        yield return new WaitForSeconds(0.2f);

    }

}
