using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private static BackgroundMusic backgroundMusic;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void Awake()
    {
        // If backgroundMusic is null (first time being called)
        if (backgroundMusic == null)
        {
            //Assign this gameobject itself 
            backgroundMusic = this;
            DontDestroyOnLoad(backgroundMusic);
        }
        else
        {
            // If backgroundMusic exits in the scene, destory it
            Destroy(gameObject);
        }
    }
}
