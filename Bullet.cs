using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 3f;
    private float direction = 1f;

    void Start()
    {
        Destroy(gameObject, lifeTime); // Удаляем пулю через 3 секунды
    }

    public void SetDirection(float dir)
    {
        direction = dir;
    }

    void Update()
    {
        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);
    }
}
