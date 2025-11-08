using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDiscBullet : MonoBehaviour
{
    private Vector2 moveDirection;
    private float speed;
    public float killDistance = 20f; // дистанция, после которой пуля удаляется

    public void SetDirection(Vector2 dir, float spd)
    {
        moveDirection = dir.normalized;
        speed = spd;
    }

    void Update()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime);

        // если пуля слишком далеко улетела, уничтожаем
        if (Mathf.Abs(transform.position.y) > killDistance || Mathf.Abs(transform.position.x) > killDistance)
        {
            Destroy(gameObject);
        }
    }
}
