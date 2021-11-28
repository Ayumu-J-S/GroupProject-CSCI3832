using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneAnimation : MonoBehaviour
{

    // variables for animation
    public Animator animator;
    public bool isStanding = true;
    public bool inTransition = false;

    // Start is called before the first frame update
    void Start()
    {
        
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

}
