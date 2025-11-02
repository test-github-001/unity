using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

using UnityEngine.SceneManagement; // Для перезагрузки сцены

public class PlayerBullet : MonoBehaviour
{
    public float speed = 12f; // Скорость перемещения
    public float killY = 6f;

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
        if (transform.position.y > killY)
        {
            Destroy(gameObject);
        }
    }
}
