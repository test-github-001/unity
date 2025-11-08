using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyBG : MonoBehaviour
{
    // float height = 11.62f;
    public float height = 7.24f;
    float returnY;

    public float flySpeed = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        height *= transform.localScale.y;
        transform.position = new Vector3(0, 0, transform.position.z);
        returnY = -height * 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * flySpeed * Time.deltaTime);
        if (transform.position.y < returnY)
        {
            transform.position += Vector3.up * height;
        }
    }
}
