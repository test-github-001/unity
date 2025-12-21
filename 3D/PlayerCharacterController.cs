using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// PlayerCharacterController
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class PlayerCharacterController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float walkSpeed = 3f;
    [SerializeField] private float backwardSpeed = 2f;
    [SerializeField] private float rotationSpeed = 180f;
    [SerializeField] private float jumpForce = 7f; // Увеличил силу прыжка

    [Header("Ground Check")]
    [SerializeField] private float groundCheckDistance = 0.3f; // Увеличил дистанцию
    [SerializeField] private LayerMask groundLayer = -1; // Все слои

    // Components
    private Rigidbody rb;
    private Animator animator;

    // Movement
    private Vector3 moveDirection;
    private bool isGrounded = false;
    private int groundContactCount = 0;

    // Animation parameters
    private readonly int speedHash = Animator.StringToHash("Speed");
    private readonly int jumpHash = Animator.StringToHash("Jump");
    private readonly int isGroundedHash = Animator.StringToHash("IsGrounded");

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        // Настройки Rigidbody
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        rb.interpolation = RigidbodyInterpolation.Interpolate; // Добавил для плавности
    }

    void Update()
    {
        // Получаем ввод и обновляем анимации
        HandleInput();

        // Проверка земли
        CheckGround();

        // Обработка прыжка
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        // Движение в FixedUpdate для физики
        MoveCharacter();
    }

    void HandleInput()
    {
        float verticalInput = 0f;
        float horizontalInput = 0f;

        // Движение вперед/назад
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            verticalInput = 1f;
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            verticalInput = -1f;

        // Повороты
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            horizontalInput = -1f;
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            horizontalInput = 1f;

        // Поворот персонажа
        if (horizontalInput != 0)
        {
            transform.Rotate(0, horizontalInput * rotationSpeed * Time.deltaTime, 0);
        }

        // Движение
        moveDirection = transform.forward * verticalInput;

        // Обновляем аниматор - ВАЖНО: Mathf.Abs для скорости!
        animator.SetFloat(speedHash, Mathf.Abs(verticalInput));
    }

    void MoveCharacter()
    {
        if (moveDirection != Vector3.zero)
        {
            float currentSpeed = moveDirection.z < 0 ? backwardSpeed : walkSpeed;
            Vector3 movement = moveDirection.normalized * currentSpeed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + movement);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Упрощенная проверка - любой контакт снизу
        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.normal.y > 0.5f) // Нормаль смотрит вверх = поверхность под нами
            {
                groundContactCount++;
                isGrounded = true;
                animator.SetBool(isGroundedHash, true);
                Debug.Log("Ground ENTER: " + collision.gameObject.name);
                break;
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        groundContactCount--;

        if (groundContactCount <= 0)
        {
            groundContactCount = 0;
            isGrounded = false;
            animator.SetBool(isGroundedHash, false);
            Debug.Log("Ground EXIT: " + collision.gameObject.name);
        }
    }

    void CheckGround()
    {
        animator.SetBool(isGroundedHash, isGrounded);
    }

    void Jump()
    {
        Debug.Log($"JUMP! Grounded={isGrounded}, Force={jumpForce}");
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        animator.SetTrigger(jumpHash);
    }

    void OnDrawGizmos()
    {
        // Визуализация проверки земли
        Gizmos.color = Color.blue;
        if (TryGetComponent<CapsuleCollider>(out var collider))
        {
            Vector3 rayStart = transform.position;
            float rayLength = collider.height / 2 + 0.1f;
            Gizmos.DrawLine(rayStart, rayStart + Vector3.down * rayLength);
        }
    }
}
