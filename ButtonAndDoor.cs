using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAndDoor : MonoBehaviour
{
    public GameObject door;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("player_tag"))
        {
            Destroy(door);
        }
    }
}
