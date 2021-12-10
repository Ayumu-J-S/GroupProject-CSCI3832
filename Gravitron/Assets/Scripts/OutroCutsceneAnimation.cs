using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OutroCutsceneAnimation : MonoBehaviour
{

    // variables for animation
    public Animator dogAnimator;
    public bool isStanding = false;
    public bool inTransition = false;
    public Animator cageAnimator;

    // Variables for speech box (Note: This requires that a TextMeshPro called "Speech Text" is in the current scene)
    private GameObject speechTextObject;
    private TextMeshProUGUI speechText;
    private RawImage speakerSpriteObject;
    private Texture gravitronSprite;
    private Texture wretchedSprite;
    public Canvas dialogueOverlay;

    // In-game sprites
    public GameObject gravitron;
    public GameObject atom;
    public GameObject cage;

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

        // Get the animators
        dogAnimator = atom.GetComponent<Animator>();
        cageAnimator = cage.GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        // Switch between the dogs sit and stand animations
        // this is tied to keyboard input for testing
        // but can be changed so that the transition happens at different times
        if (Input.GetKey(KeyCode.S) && !inTransition)
        {

            inTransition = true;
            dogAnimator.SetBool("InTransition", inTransition);

            if (isStanding)
            {
                isStanding = !isStanding;
                StartCoroutine(SitRoutine());
                dogAnimator.SetBool("Standing", isStanding);
            }
            else
            {
                isStanding = !isStanding;
                StartCoroutine(StandRoutine());
                dogAnimator.SetBool("Standing", isStanding);
            }

        }

    }

    //  Make sure the shoooting animation plays completely before stopping
    private IEnumerator SitRoutine()
    {
        // Wait for the rest of the animation to play
        yield return new WaitForSeconds(.5f);

        inTransition = false;
        dogAnimator.SetBool("InTransition", inTransition);
    }

    private IEnumerator StandRoutine()
    {
        // Wait for the rest of the animation to play
        yield return new WaitForSeconds(.5f);

        inTransition = false;
        dogAnimator.SetBool("InTransition", inTransition);
    }

    // The cutscene can be controlled from here
    IEnumerator Cutscene()
    {
        // Wait for scene to load in
        dialogueOverlay.enabled = false;
        yield return new WaitForSeconds(1.0f);
        dialogueOverlay.enabled = true;

        // Inital dialogue
        yield return advanceDialogue(gravitronSprite, "Give me back my dog!!");
        yield return advanceDialogue(wretchedSprite, "Admit that you stole my research!!");
        yield return advanceDialogue(gravitronSprite, "You've never done a bit of research in your life!");
        yield return advanceDialogue(wretchedSprite, "Liar!! I'm keeping the dog!!!!");
        yield return advanceDialogue(gravitronSprite, "(Well shoot, what do I do now?)");

        // Disable overlay
        dialogueOverlay.enabled = false;

        // Allow player to shoot
        gravitron.GetComponent<PlayerMovement>().shootInOutro = true;

        // Wait for player to shoot
        yield return WaitForShoot();

        // If player still hasn't shot yet, prompt
        if (!ButtonScript.buttonPressed)
        {
            dialogueOverlay.enabled = true;
            yield return advanceDialogue(gravitronSprite, "(Well SHOOT, what do I do now?)");
            dialogueOverlay.enabled = false;
            yield return WaitForShoot();
        }

        // If player still hasn't shot yet, prompt
        if (!ButtonScript.buttonPressed)
        {
            dialogueOverlay.enabled = true;
            yield return advanceDialogue(gravitronSprite, "(SHOOT man, if only I had some way of opening that door...)");
            dialogueOverlay.enabled = false;
            yield return WaitForShoot();
        }

        // If player still hasn't shot yet, prompt (this prompt will continue until player shoots)
        while (!ButtonScript.buttonPressed)
        {
            dialogueOverlay.enabled = true;
            yield return advanceDialogue(gravitronSprite, "(Dude seriously??? There's a button up there!! What have you been doing this whole game????)");
            dialogueOverlay.enabled = false;
            yield return WaitForShoot();
        }

        // Disable shooting
        gravitron.GetComponent<PlayerMovement>().shootInOutro = false;

        // Play animation to free the dog
        cageAnimator.Play("Cage Animation");
        inTransition = true;
        dogAnimator.SetBool("InTransition", inTransition);
        StartCoroutine(StandRoutine());
        dogAnimator.SetBool("Standing", isStanding);

        // Finish cutscene
        yield return new WaitForSeconds(0.5f);
        dialogueOverlay.enabled = true;
        yield return advanceDialogue(wretchedSprite, "Wait what? This is my lab, why did I make that button do that???");
        yield return advanceDialogue(gravitronSprite, "I dunno, probably because this is a student game and they ran out of time to come up with a plausible reason for it.");
        yield return advanceDialogue(wretchedSprite, "...\nFair enough.");
        yield return advanceDialogue(gravitronSprite, "C'mon Atom, let's go home.");
        dialogueOverlay.enabled = false;


        // Move gravitron to the right and stop at the door
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
        while (!(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.R)))
        {
            yield return null;
        }

        // Wait to prevent one key press spanning multiple frames as counting for multiple key presses
        yield return new WaitForSeconds(0.2f);

    }

    // Wait until player shoots
    private IEnumerator WaitForShoot()
    {
        // Run for 2.5 seconds or until player shoots
        for (int i = 25; i > 0; i--)
        {
            // If player shoots, break
            if (ButtonScript.buttonPressed)
            {
                i = 0;
                yield return null;
            } 
            else
            {
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

}
