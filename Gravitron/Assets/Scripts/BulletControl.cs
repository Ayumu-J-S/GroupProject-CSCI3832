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
    private Vector3 position;
    
    // Possible directions for the bullet trajectory
    public enum Direction {Up, Down, Left, Right};

    // Direction of bullet trajectory
    public Direction bulletDirection;
    

    // Start is called before the first frame update
    void Start()
    {
        // Set bullet spawnpoint to controller's position
        position = transform.position;

        // Start spawning bullets
        StartCoroutine("ShootBullets");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Spawns a new bullet every x seconds, x being the shooting rate
    IEnumerator ShootBullets()
    {
        // Runs until scene is finished
        while(true)
        {
            // Create a new bullet
            GameObject bullet = Instantiate(bulletObject, position, bulletObject.transform.rotation);
            
            // Wait
            yield return new WaitForSeconds(shootingRate);
        }
    }

}
