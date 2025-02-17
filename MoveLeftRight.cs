using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

/*
3 СПОСОБА ПЕРЕМЕЩЕНИЯ ОБЪЕКТА
*/

public class PlayerControl : MonoBehaviour
{
    // скорость перемещения 
    public float speed = 2f;
    // функция вызывается при старте сцены
    void Start()
    {
        
    }

    // функция вызывается каждый кадр
    void Update()
    {
        // Если игрок нажал стелку влево или (|| - или) букву "A"
        if ( Input.GetKey(KeyCode.LeftArrow) ||  Input.GetKey(KeyCode.A))
        {
            /*
            Vector2 point = transform.position;
            point.x -= speed * Time.deltaTime;
            transform.position = point;
            */
            // transform.Translate(Vector2.left * speed * Time.deltaTime);
            transform.Translate(-speed * Time.deltaTime, 0f, 0f);
        }
       // Иначе, если игрок нажал стелку вправо или (|| - или) букву "D"
        else if (Input.GetKey(KeyCode.RightArrow) ||  Input.GetKey(KeyCode.D))
        {
            /*
            Vector2 point = transform.position;
            point.x += speed * Time.deltaTime;
            transform.position = point;
            */
            // transform.Translate(Vector2.right * speed * Time.deltaTime);
            transform.Translate(speed * Time.deltaTime, 0f, 0f);
        }
    }
}
