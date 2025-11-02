using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

using UnityEngine.SceneManagement; // Для перезагрузки сцены

public class PlayerController : MonoBehaviour
{
    public float speed = 5f; // Скорость перемещения
    public float minX = 0f;
    public float maxX = 0f;
    public float minY = 0f;
    public float maxY = 0f;

    public GameObject bulletPrefab;     // Префаб пули

    public static float reloadSpeed = 0.5f;
    private float reloadTimeout = reloadSpeed;

    void Update()
    {
        // ВЛЕВО
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            if (transform.position.x < minX) {
                transform.position = new Vector3(minX, transform.position.y, transform.position.z);
            }
        }
        // ВПРАВО
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            if (transform.position.x > maxX)
            {
                transform.position = new Vector3(maxX, transform.position.y, transform.position.z);
            }
        }
        // ВВЕРХ
        else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
            if (transform.position.y > maxY)
            {
                transform.position = new Vector3(transform.position.x, maxY, transform.position.z);
            }
        }
        // ВНИЗ
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);
            if (transform.position.y < minY)
            {
                transform.position = new Vector3(transform.position.x, minY, transform.position.z);
            }
        }

        // ПЕРЕЗАРЯДКА
        if (reloadTimeout > 0)
        {
            reloadTimeout -= Time.deltaTime;
        }
        // СТРЕЛЬБА
        else if (Input.GetKey(KeyCode.Space) && reloadTimeout <= 0)
        {
            Shut();
            reloadTimeout = reloadSpeed;
        }
    }

    private void Shut()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
    }
}
