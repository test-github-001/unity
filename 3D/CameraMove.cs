using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CameraMove : MonoBehaviour
{
    [Header("Настройки движения")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float speedChangeStep = 2f;
    [SerializeField] private float minSpeed = 1f;
    [SerializeField] private float maxSpeed = 50f;

    [Header("Настройки поворота")]
    [SerializeField] private float rotationSpeed = 50f;
    [SerializeField] private float defaultPitchAngle = 20f; // Угол наклона камеры по умолчанию

    [Header("Элементы UI")]
    [SerializeField] private GameObject speedInfoPanel;
    [SerializeField] private TMP_Text speedText;

    private Vector3 moveDirection;
    private bool isCameraControlActive = false;
    private float currentYaw = 0f;
    private float originalPitch = 0f;

    void Start()
    {
        // Сохраняем начальный угол наклона камеры
        originalPitch = transform.eulerAngles.x;

        // Инициализируем UI
        UpdateUI();

        // Инициализируем поворот
        currentYaw = transform.eulerAngles.y;

        // Устанавливаем начальный угол наклона, если нужно
        if (Mathf.Approximately(defaultPitchAngle, 0f))
            defaultPitchAngle = originalPitch;
    }

    void Update()
    {
        // Проверяем, зажата ли клавиша C
        bool wasActive = isCameraControlActive;
        isCameraControlActive = Input.GetKey(KeyCode.C);

        // Если состояние изменилось, обновляем UI
        if (wasActive != isCameraControlActive)
        {
            UpdateUI();
        }

        // Если клавиша C не зажата - выходим
        if (!isCameraControlActive)
            return;

        // Управление скоростью
        HandleSpeedControl();

        // Управление поворотом
        HandleCameraRotation();

        // Управление движением камеры
        HandleCameraMovement();

        // Обновляем текст скорости, если изменилась скорость
        if (Input.GetKeyDown(KeyCode.Plus) || Input.GetKeyDown(KeyCode.Equals) ||
            Input.GetKeyDown(KeyCode.KeypadPlus) ||
            Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            UpdateUI();
        }
    }

    void HandleSpeedControl()
    {
        // Увеличиваем скорость
        if (Input.GetKeyDown(KeyCode.Plus) || Input.GetKeyDown(KeyCode.Equals) ||
            Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            moveSpeed = Mathf.Min(moveSpeed + speedChangeStep, maxSpeed);
        }

        // Уменьшаем скорость
        if (Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            moveSpeed = Mathf.Max(moveSpeed - speedChangeStep, minSpeed);
        }
    }

    void HandleCameraRotation()
    {
        float rotationInput = 0f;

        // Поворот влево (Z)
        if (Input.GetKey(KeyCode.Z))
            rotationInput = -1f;
        // Поворот вправо (X)
        else if (Input.GetKey(KeyCode.X))
            rotationInput = 1f;

        // Если есть ввод для поворота
        if (rotationInput != 0f)
        {
            // Изменяем угол поворота по горизонтали
            currentYaw += rotationInput * rotationSpeed * Time.deltaTime;

            // Применяем поворот с сохранением угла наклона
            Quaternion rotation = Quaternion.Euler(defaultPitchAngle, currentYaw, 0f);
            transform.rotation = rotation;
        }
    }

    void HandleCameraMovement()
    {
        // Сбрасываем направление движения
        moveDirection = Vector3.zero;

        // Движение вперед/назад (относительно направления камеры)
        if (Input.GetKey(KeyCode.W))
            moveDirection += transform.forward;
        if (Input.GetKey(KeyCode.S))
            moveDirection -= transform.forward;

        // Движение влево/вправо (относительно направления камеры)
        if (Input.GetKey(KeyCode.D))
            moveDirection += transform.right;
        if (Input.GetKey(KeyCode.A))
            moveDirection -= transform.right;

        // Движение вверх/вниз (по мировой оси Y)
        if (Input.GetKey(KeyCode.E))
            moveDirection += Vector3.up;
        if (Input.GetKey(KeyCode.Q))
            moveDirection += Vector3.down;

        // Нормализуем вектор направления (если нужно)
        if (moveDirection.magnitude > 1)
            moveDirection.Normalize();

        // Применяем движение
        if (moveDirection != Vector3.zero)
        {
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
        }
    }

    void UpdateUI()
    {
        // Обновляем видимость панели
        if (speedInfoPanel != null)
            speedInfoPanel.SetActive(isCameraControlActive);

        // Обновляем текст
        UpdateSpeedText();
    }

    void UpdateSpeedText()
    {
        if (speedText != null)
        {
            speedText.text = "Скорость камеры: " + moveSpeed.ToString("F1") + "\n" + "Используйте +/- для изменения\n" + "Z/X - поворот";
        }
    }

    // Вспомогательные методы
    public void ResetCameraAngle()
    {
        // Восстанавливаем угол наклона к значению по умолчанию
        Quaternion rotation = Quaternion.Euler(defaultPitchAngle, currentYaw, 0f);
        transform.rotation = rotation;
    }

    public void SetCameraPitch(float pitchAngle)
    {
        // Устанавливаем новый угол наклона
        defaultPitchAngle = Mathf.Clamp(pitchAngle, -90f, 90f);
        ResetCameraAngle();
    }

    public void SetMoveSpeed(float newSpeed)
    {
        moveSpeed = Mathf.Clamp(newSpeed, minSpeed, maxSpeed);
        UpdateSpeedText();
    }

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }
}
