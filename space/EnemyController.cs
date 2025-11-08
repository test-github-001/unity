using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

using UnityEngine.SceneManagement; // Для перезагрузки сцены

public class EnemyController : MonoBehaviour
{
    public float speed = 1f; // Скорость перемещения
    public float minX = -11f;
    public float maxX = 11f;
    public float startY = 6f;
    public float endY = -6f;

    public GameObject bulletPrefab;     // Префаб пули

    public float reloadSpeedMin = 1f;
    public float reloadSpeedMax = 2f;

    private float reloadTimeout = 1f;

    private void Start()
    {
        float startX = Random.Range(minX, maxX);
        transform.position = new Vector3(startX, startY, transform.position.z);

        reloadTimeout = Random.Range(reloadSpeedMin, reloadSpeedMax);
    }

    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
        if (transform.position.y < endY) { 
            Destroy(gameObject);
        }

        reloadTimeout-= Time.deltaTime;
        if (reloadTimeout < 0f)
        {
            reloadTimeout = Random.Range(reloadSpeedMin, reloadSpeedMax);
            Shut();
        }
    }

    void Shut()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("player_bullet"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
