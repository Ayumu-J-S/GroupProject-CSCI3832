using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightForBullet : MonoBehaviour
{
    private GameObject lightObj;
    public float lightIntensity = 8.5f;
    public float lightRange = 3f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Whenever a ball is shot, assign that to Ball variable
        GameObject Ball = GameObject.Find("BallPrefab(Clone)");

        // If the ball is there and the light for the ball is not assigned
        if (lightObj == null && Ball != null)
        {
            // Create a new object for light
            lightObj = new GameObject();
            //Attach a light component
            Light lightComp = lightObj.AddComponent<Light>();

            // Set the light as Point Light and assign intensity and range
            lightComp.type = LightType.Point;
            lightComp.range = lightRange;
            lightComp.intensity = lightIntensity;

            // Set the position of the ball as same as position of theball
            // -2 here is that so the point light would actually light up object
            Vector3 posBall = Ball.transform.position;
            posBall.z = -2;
            lightObj.transform.position = posBall;

        }
        else if (lightObj != null && Ball != null)
        {
            // Change the position of the light same as ball
            Light lightComp = lightObj.GetComponent<Light>();
            Vector3 posBall = Ball.transform.position;
            posBall.z = -2;
            lightObj.transform.position = posBall;
        }
    }
}
