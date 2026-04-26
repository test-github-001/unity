using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Vector3 direction;
    private float speed = 15f;
    private float remainingDistance = 10f;   // сколько ещЄ может пролететь
    private int damage = 5;
    private LayerMask enemyLayer;            // слой, на котором наход€тс€ враги

    public void Setup(Vector3 dir, float spd, float range, int dmg)
    {
        direction = dir.normalized;
        speed = spd;
        remainingDistance = range;
        damage = dmg;

        // ѕредполагаем, что враги наход€тс€ на слое "EnemiesLayer" (создай его и назначь префабам врагов)
        enemyLayer = LayerMask.GetMask("EnemiesLayer");

        // ѕоворачиваем пулю в направлении движени€
        transform.forward = direction;
    }

    void Update()
    {
        float step = speed * Time.deltaTime;

        // ѕровер€ем пересечение с врагами на отрезке от текущей позиции до следующей
        RaycastHit hit;
        Vector3 start = transform.position;
        Vector3 end = start + direction * step;

        if (Physics.Linecast(start, end, out hit, enemyLayer))
        {
            EnemyScript enemy = hit.collider.GetComponent<EnemyScript>();
            if (enemy != null)
            {
                enemy.AddDamage(damage);
                Destroy(gameObject);
                return;
            }
        }

        // ѕеремещаем пулю
        transform.position = end;
        remainingDistance -= step;

        // ≈сли пройденный путь превысил допустимую дальность Ч уничтожаем
        if (remainingDistance <= 0f) Destroy(gameObject);
    }
}
