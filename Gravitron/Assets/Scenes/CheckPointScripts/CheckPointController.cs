using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointController : MonoBehaviour
{
    private static CheckPointController checkPointerController;
    public GameObject Player;
    public Vector3 CheckPoint1Pos =
        new Vector3(23.0722237f, -0.730000019f, 0);
    public Vector3 CheckPoint2Pos =
        new Vector3(52.7599983f, -2.50999999f, 0);

    // Start is called before the first frame update
    void Start()
    {
        bool checkPoint1 = CheckPoint1.checkSender;
        bool checkPoint2 = CheckPoint2.checkSender;

        // Bring the player depends on the checkSender that is sent
        if (checkPoint1 && !checkPoint2)
        {
            Player.transform.position = CheckPoint1Pos;
            Debug.Log(CheckPoint1Pos);
        }
        else if (checkPoint1 && checkPoint2)
        {
            Player.transform.position = CheckPoint2Pos;
        }
        else
        {
            Player.transform.position =
                new Vector3(-10.6999998f, -2.69000006f, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
