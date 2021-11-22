using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    // Bullet speed
    public float bulletSpeed = 2;
    
    // Bullet's movement
    Vector3 movement = Vector3.zero;

    // Possible directions for the bullet trajectory
    public enum Direction { Up, Down, Left, Right };

    // Direction of bullet trajectory
    private Direction bulletDirection;

    // Start is called before the first frame update
    void Start()
    {
        // Get the bullet controller
        GameObject controller = GameObject.Find("BulletController");

        // Get the controller's script
        BulletControl controllerScript = controller.GetComponent<BulletControl>();

        // Get direction from controller
        bulletDirection = (Direction)controllerScript.bulletDirection;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        // Get direction of bullet trajectory
        switch (bulletDirection)
        {
            case Direction.Up:
                movement.y = bulletSpeed * Time.deltaTime;
                break;
            case Direction.Down:
                movement.y = -1 * bulletSpeed * Time.deltaTime;
                break;
            case Direction.Left:
                movement.x = -1 * bulletSpeed * Time.deltaTime;
                break;
            case Direction.Right:
                movement.x = bulletSpeed * Time.deltaTime;
                break;
            default:
                Debug.Log("Error: Could not parse bullet direction");
                break;
        }

        // Move bullet
        transform.position += movement;

    }

    // Destroy the bullet when it hits something
    void OnCollisionEnter2D()
    {
        Destroy(gameObject);
    }
}
