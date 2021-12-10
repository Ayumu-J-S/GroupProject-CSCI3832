using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Start the game
    public void PlayGame()
    {
        SceneManager.LoadScene("Intro", LoadSceneMode.Single);
    }

    // Open the How to Play menu
    public void HowToPlay()
    {
        SceneManager.LoadScene("How to Play", LoadSceneMode.Single);
    }

    // Go back to the main menu
    public void BackToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
    }

    // Close the application
    public void EndGame()
    {
        Application.Quit();
    }
}
