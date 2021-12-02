using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint2 : MonoBehaviour
{
    public static bool checkSender = false;
    private static CheckPoint2 checkPoint2;

    // If the player comes to the trigger set the checkSender to True
    private void OnTriggerEnter2D(Collider2D other)
    {
        checkSender = true;
    }

    private void Awake()
    {
        if (checkPoint2 == null)
        {
            // Assign this gameobject itself 
            checkPoint2 = this;
            // THis is so that once the player comes to the Trigger,
            // the value of checkSender will be preserved
            DontDestroyOnLoad(checkPoint2);
        }
        else
        {
            //This is to delete the duplicate of the gameObject
            Destroy(gameObject);
        }
    }
}
