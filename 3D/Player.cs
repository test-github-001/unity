using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

using UnityEngine.SceneManagement; // Для перезагрузки сцены

public class Player : MonoBehaviour
{
    public float speed = 8f;
    public float jumpForce = 7f;
    public float groundCheckRadius = 0.3f;
    public Transform groundCheck;
    public LayerMask terrainMask;

    private Rigidbody rb;
    private bool isGrounded;
    private Vector3 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        // Получаем ввод
        moveInput = new Vector3(
            Input.GetAxis("Horizontal"),
            0,
            Input.GetAxis("Vertical")
        ).normalized;

        // Проверка земли
        isGrounded = Physics.CheckSphere(
            groundCheck.position,
            groundCheckRadius,
            terrainMask
        );

        // Прыжок
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        // Движение
        if (moveInput.magnitude > 0)
        {
            Vector3 moveVelocity = moveInput * speed;
            moveVelocity.y = rb.velocity.y;
            rb.velocity = moveVelocity;

            // Поворот
            if (moveInput != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveInput);
                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    targetRotation,
                    Time.fixedDeltaTime * 10f
                );
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
