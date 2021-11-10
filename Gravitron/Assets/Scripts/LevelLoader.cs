using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LoadNextLevel();
        }
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        //Play animation
        //Start is the name of the animation under animation -> parameter
        transition.SetTrigger("Start");

        //This decides how long it will wait
        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }

    public void LoadNextLevel()
    {
        //This will load the next index scene
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

}

