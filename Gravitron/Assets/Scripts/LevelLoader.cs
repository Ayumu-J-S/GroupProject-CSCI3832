using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;
    private float diffxTrans = 0.5f;
    private float diffyTrans = 0.7f;
    public GameObject character;
    public GameObject door;

    // Update is called once per frame
    void Update()
    {

        Vector2 posCharacter = character.transform.position;
        Vector2 posDoor = door.transform.position;

        float diffx = Mathf.Abs(posCharacter.x - posDoor.x);
        float diffy = Mathf.Abs(posCharacter.y - posDoor.y);


        if (diffx < diffxTrans &&
            diffy < diffyTrans &&
            ButtonScript.buttonPressed)
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

