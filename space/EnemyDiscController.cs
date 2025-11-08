using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDiscController : MonoBehaviour
{
    public float speed = 0.5f; // Скорость перемещения
    public float minX = -11f;
    public float maxX = 11f;
    public float startY = 6f;
    public float endY = -6f;

    public Transform spriteTransform; // дочерний объект со спрайтом

    public GameObject bulletDiscPrefab;     // Префаб пули
    public float bulletSpeed = 1.5f;      // Скорость пули

    public int numBullets = 5; // число пуль для кругового выстрела
    public float rotationSpeed = 30f;   // скорость вращения (градусы/сек)

    public float reloadSpeedMin = 2f;
    public float reloadSpeedMax = 3f;

    private float reloadTimeout = 1f;

    private void Start()
    {
        float startX = Random.Range(minX, maxX);
        transform.position = new Vector3(startX, startY, transform.position.z);

        reloadTimeout = Random.Range(reloadSpeedMin, reloadSpeedMax);
    }

    void Update()
    {
        // Вращаем врага
        spriteTransform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);

        // движение вниз
        transform.Translate(Vector2.down * speed * Time.deltaTime);
        if (transform.position.y < endY)
        {
            Destroy(gameObject);
        }

        // таймер стрельбы
        reloadTimeout -= Time.deltaTime;
        if (reloadTimeout < 0f)
        {
            reloadTimeout = Random.Range(reloadSpeedMin, reloadSpeedMax);
            ShootFive();
        }
    }

    // Стрельба 5 пулями под разными углами
    void ShootFive()
    {
        float angleStep = 360f / numBullets; // 72 градуса
        float startAngle = 90f; // направление "вверх" относительно спрайта
        startAngle += spriteTransform.eulerAngles.z; // поворот самого врага

        for (int i = 0; i < numBullets; i++)
        {
            float angle = startAngle + angleStep * i;
            float rad = angle * Mathf.Deg2Rad;

            // направление движения пули
            Vector2 dir = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));

            GameObject bullet = Instantiate(bulletDiscPrefab, transform.position, Quaternion.identity);

            // передаем направление в пули
            bullet.GetComponent<EnemyDiscBullet>().SetDirection(dir, bulletSpeed);
        }
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
