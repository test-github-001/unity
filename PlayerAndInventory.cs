using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

using TMPro; // Для текста (text mesh pro)
using UnityEngine.SceneManagement; // Для перезагрузки сцены

public class PlayerAndInventory : MonoBehaviour
{
    public float speed = 5f; // Скорость перемещения
    public float stopFriction = 0.5f; // Уровень трения при остановке
    public float jumpForce = 6f; // Сила прыжка

    public Rigidbody2D rb;
    public SpriteRenderer sr;

    public float GameOverY = -12f; // Смещение камеры относительно игрока

    private bool isGrounded = false;

    public int coins = 0;
    public int keys = 0;
    public TMP_Text coinText; // Ссылка на текстовый элемент UI
    public TMP_Text keyText; // Ссылка на текстовый элемент UI

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            // transform.Translate(Vector2.left * speed * Time.deltaTime);
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            sr.flipX = true;
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            // transform.Translate(Vector2.right * speed * Time.deltaTime);
            rb.velocity = new Vector2(speed, rb.velocity.y);
            sr.flipX = false;
        }
        // нужно только если используете rb.velocity для перемещения и нужно быстрее замедляться
        else
        {
            rb.velocity = new Vector2(rb.velocity.x * stopFriction, rb.velocity.y);
        }

        if (isGrounded && (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space)))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false;
        }

        // Проверяем, если позиция игрока по оси Y меньше GameOverY
        if (transform.position.y < GameOverY)
        {
            // Перезагружаем текущую сцену
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("platform_tag"))
        {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("enemy_tag"))
        {
            // Перезагружаем текущую сцену
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("platform_tag"))
        {
            isGrounded = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("coin_tag"))
        {
            coins++;
            Destroy(collision.gameObject); // Удаляем монету из сцены
            coinText.text = "x " + coins.ToString(); // Обновляем интерфейс
        }

        if (collision.gameObject.CompareTag("key_tag"))
        {
            keys++;
            Destroy(collision.gameObject); // Удаляем монету из сцены
            keyText.text = "x" + keys.ToString(); // Обновляем интерфейс
        }
    }
}
