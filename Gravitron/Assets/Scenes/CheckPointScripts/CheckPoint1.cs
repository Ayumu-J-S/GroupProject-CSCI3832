using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint1 : MonoBehaviour
{
    public static bool checkSender = false;
    private static CheckPoint1 checkPoint1;

    // If the player comes to the trigger set the checkSender to True
    private void OnTriggerEnter2D(Collider2D other)
    {
        checkSender = true;
    }

    private void Awake()
    {
        if (checkPoint1 == null)
        {
            // Assign this gameobject itself 
            checkPoint1 = this;
            // THis is so that once the player comes to the Trigger,
            // the value of checkSender will be preserved
            DontDestroyOnLoad(checkPoint1);
        }
        else
        {
            // This is to delete the duplicate of the gameObject
            Destroy(gameObject);
        }
    }
}
