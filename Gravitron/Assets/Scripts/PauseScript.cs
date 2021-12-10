using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{

    GameObject pauseMenu;
    
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu = GameObject.Find("Pause Menu");
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // If escape key hit
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            // If not paused, pause
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                pauseMenu.SetActive(true);
            }
            // If paused, unpause
            else if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
                pauseMenu.SetActive(false);
            }
        }
    }

    // Go back to the main menu
    public void MenuButton()
    {
        SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
    }
}
