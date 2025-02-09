using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed = 0.5f;
    public float minX = -1.5f;
    public float maxX = 1.5f;

    private bool isToLeft = false;

    private void Start()
    {
        isToLeft = Random.Range(0f, 1f) > 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if(isToLeft)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            if (transform.position.x <= minX)
            {
                isToLeft = false;
            }
        }
        else
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            if (transform.position.x >= maxX)
            {
                isToLeft = true;
            }
        }
    }
}
