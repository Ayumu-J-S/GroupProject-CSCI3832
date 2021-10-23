using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseGravity : MonoBehaviour
{
    private Rigidbody2D rgb;
    private float gravityScale = 0.5f;
    private bool shooted = false;
    // Start is called before the first frame update
    void Start()
    {
        rgb = transform.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            rgb.gravityScale = -gravityScale;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            rgb.gravityScale = gravityScale;
        }
        if (Input.GetKey(KeyCode.RightArrow) && !shooted)
        {
            rgb.AddForce(new Vector2(500, 0));
            shooted = true;
        }
    }
}
