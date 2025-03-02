using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

using UnityEngine.SceneManagement; // Для перезагрузки сцены

public class Player : MonoBehaviour
{
    public float speed = 5f; // Скорость перемещения
    public float stopFriction = 0.5f; // Уровень трения при остановке
    public float jumpForce = 6f; // Сила прыжка

    public Rigidbody2D rb;
    public SpriteRenderer sr;

    public float GameOverY = -12f; // Смещение камеры относительно игрока

    private bool isGrounded = false;

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            // rb.velocity = new Vector2(-speed, rb.velocity.y);
            sr.flipX = true;
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            // rb.velocity = new Vector2(speed, rb.velocity.y);
            sr.flipX = false;
        }
        // нужно только если используете rb.velocity для перемещения и нужно быстрее замедляться
        /*
        else
        {
            rb.velocity = new Vector2(rb.velocity.x * stopFriction, rb.velocity.y);
        }
        */

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
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("platform_tag"))
        {
            isGrounded = false;
        }
    }
}
