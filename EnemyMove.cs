using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float startX;
    public float startY;
    public float endX;
    public float endY;
    public float speed;

    private Vector2 startPosition;
    private Vector2 endPosition;
    private float journeyLength;
    private float startTime;
    private bool movingToEnd = true;

    void Start()
    {
        // Устанавливаем начальную позицию объекта
        startPosition = new Vector2(startX, startY);
        endPosition = new Vector2(endX, endY);
        transform.position = startPosition;

        // Вычисляем длину пути
        journeyLength = Vector2.Distance(startPosition, endPosition);
        startTime = Time.time;
    }

    void Update()
    {
        // Вычисляем пройденное расстояние
        float distanceCovered = (Time.time - startTime) * speed;

        // Обновляем позицию объекта с использованием Lerp для плавного движения
        float t = distanceCovered / journeyLength;
        transform.position = Vector2.Lerp(startPosition, endPosition, t);

        // Проверяем, достиг ли объект конечной точки
        if (distanceCovered >= journeyLength)
        {
            // Меняем направление движения
            movingToEnd = !movingToEnd;

            // Сбрасываем время
            startTime = Time.time;

            // Меняем начальную и конечную точки
            Vector2 temp = startPosition;
            startPosition = endPosition;
            endPosition = temp;

            // Обновляем длину пути
            journeyLength = Vector2.Distance(startPosition, endPosition);
        }
    }
}
