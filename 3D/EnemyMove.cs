using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyMove : MonoBehaviour
{
    public float speed = 3f; //переменная для хранения скорости движения
    public float distance = 1f;//переменная для хранения дистанции между объектом и целью
    public Transform target;//переменная для хранения координат цели, к которым хочет приблизиться объект
    
    void Update()
    {
        Vector3 newDist = target.position - transform.position; //узнаем реальную дистанцию между объектами
        if(newDist.sqrMagnitude > distance)
        {
            float step = speed * Time.deltaTime; //шаг, с которым будет двигаться объект к своей цели
            transform.position = Vector3.MoveTowards(transform.position, target.position, step); 
            transform.LookAt(target);
        }
    }
}
