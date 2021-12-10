using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint3 : MonoBehaviour
{
    public static bool checkSender = false;
    private static CheckPoint3 checkPoint3;

    // If the player comes to the trigger set the checkSender to True
    private void OnTriggerEnter2D(Collider2D other)
    {
        checkSender = true;
        Debug.Log("Collided");
    }

    private void Awake()
    {
        if (checkPoint3 == null)
        {
            // Assign this gameobject itself 
            checkPoint3 = this;
            // THis is so that once the player comes to the Trigger,
            // the value of checkSender will be preserved
            DontDestroyOnLoad(checkPoint3);
        }
        else
        {
            //This is to delete the duplicate of the gameObject
            Destroy(gameObject);
        }
    }
}
