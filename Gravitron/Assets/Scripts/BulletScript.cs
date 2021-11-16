using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float bulletSpeed = 5;
    Vector3 movement = Vector3.zero;

    /* This currently only shoots bullets left to right across the screen.
        I don't have time to change this right now, so I'm leaving myself instructions
        on how to do it later:
            Add a new public variable to BulletController for direction (needs 4 options, enum? Does c# have enums???)
            Instantiate every instance of BulletPrefab with that direction as an argument
            Add that variable to this script and use it to determine movement
            ???
            Profit */
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        movement.x = bulletSpeed * Time.deltaTime;
        transform.position += movement;
    }

    // Destroy the bullet when it hits something
    void OnCollisionEnter2D()
    {
        Destroy(gameObject);
    }
}
