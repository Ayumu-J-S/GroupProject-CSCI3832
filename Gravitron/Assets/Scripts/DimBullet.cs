using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimBullet : MonoBehaviour
{
    //Get the diffuse material
    public Material diffuse;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Whenever a ball is shot, assign that to Ball variable
        GameObject Ball = GameObject.Find("BallPrefab(Clone)");
        //Attach the diffuse material to the ball so it that it is dimmed
        Ball.GetComponent<Renderer>().material = diffuse;
    }
}
