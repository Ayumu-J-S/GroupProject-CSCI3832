using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    // The bullet object to be recreated
    public GameObject bulletObject;

    // Time (in seconds) between the firing of each bullet
    public float shootingRate = 1f;

    // Position vector for bullet spawnpoint
    public Vector3 position;

    // Start is called before the first frame update
    void Start()
    {
        //bulletObject = GameObject.Find("BulletPrefab");
        StartCoroutine("ShootBullets");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ShootBullets()
    {
        // *** DELETE THIS COMMENT LATER ***
        // Will this run indefinitely in the background while the player is in other scenes, or will it stop
        // when the scene isn't loaded anymore? If we start having a heckton of lag this may be why
        // Also, this script should probably be attached to a null object and NOT the bullets, because new bullets
        // are constantly being created by this script
        while(true)
        {
            GameObject bullet = Instantiate(bulletObject, position, bulletObject.transform.rotation);
            
            yield return new WaitForSeconds(shootingRate);
        }
    }

}
