using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

using UnityEngine.SceneManagement; // Для перезагрузки сцены

public class EnemyBullet : MonoBehaviour
{
    public float speed = 5f; // Скорость перемещения
    public float killY = -6f;

    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
        if (transform.position.y < killY)
        {
            Destroy(gameObject);
        }
    }
}
