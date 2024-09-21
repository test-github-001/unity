using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceDelete : MonoBehaviour
{
    public GameObject targetObject;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(targetObject);
    }
}