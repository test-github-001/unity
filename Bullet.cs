using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Добавьте пуле RigitBogy2D, и выбирите в нем Body Type -> Kinematic
// Добавьте пули коллайдер и установите галочку в [V] Is Trigger

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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("enemy_tag"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
