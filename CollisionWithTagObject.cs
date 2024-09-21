using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionWithTagObject : MonoBehaviour
{
    private int keys = 0;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Key"))
        {
            Destroy(collision.gameObject);
            keys++;
        }
        else if (collision.gameObject.CompareTag("Door") && keys > 0)
        {
            Destroy(collision.gameObject);
            keys--;
        }
        else if (collision.gameObject.CompareTag("Trap"))
        {
            Destroy(collision.gameObject);
        }
    }
}