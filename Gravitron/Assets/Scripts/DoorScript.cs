using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * When applying this script to the Door, make sure the closed door (Sprite) has
 * an opned door (Sprite) behind it, and these doors have the floowing conditions.
 *     -The front sprite (opened door) has boxCollider2D, which is a trigger.
 *     
 *     -The rear sprite (closed door) is the child of the front sprite
 *     (opened door).
 */
public class DoorScript : MonoBehaviour
{
    private int sortingOrder;

    // Start is called before the first frame update
    void Start()
    {
        sortingOrder = transform.GetComponent<SpriteRenderer>().sortingOrder;
        sortingOrder += 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        transform.gameObject
            .transform.GetChild(0)
            .GetComponent<SpriteRenderer>()
            .sortingOrder = sortingOrder;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        transform.gameObject
            .transform.GetChild(0)
            .GetComponent<SpriteRenderer>()
            .sortingOrder = sortingOrder - 2;
    }
}
