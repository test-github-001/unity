using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;

public class MiniGunTowerScript : MonoBehaviour
{
    [Header("Параметры башни")]
    public float range = 10f;
    public float fireRate = 3f;      // выстрелов в секунду
    public GameObject bulletPrefab;

    [Header("Параметры пули")]
    public float bulletSpeed = 15f;
    public float bulletRange = 10f;
    public int damage = 5;

    private float fireTimer = 0f;
    private EnemyScript currentTarget;

    void Update()
    {
        
        if (fireTimer > 0f)
        {
            fireTimer -= Time.deltaTime;
            return;
        }


        FindClosestEnemy();
        if (currentTarget == null) return;
        
        Shoot();
        fireTimer = 1f / fireRate;
    }

    void FindClosestEnemy()
    {
        EnemyScript[] enemies = FindObjectsOfType<EnemyScript>();
        float closestDist = range;
        EnemyScript closest = null;

        foreach (EnemyScript enemy in enemies)
        {
            if (enemy == null) continue;
            float dist = Vector3.Distance(transform.position, enemy.transform.position);
            if (dist < closestDist)
            {
                closestDist = dist;
                closest = enemy;
            }
        }
        currentTarget = closest;
    }

    void Shoot()
    {
        if (currentTarget == null) return;

        // Точка вылета: позиция башни, но Y берём от врага
        Vector3 spawnPos = transform.position;
        spawnPos.y = currentTarget.transform.position.y;

        // Направление к цели
        Vector3 direction = (currentTarget.transform.position - spawnPos).normalized;

        // Создаём пулю, повёрнутую в сторону цели
        GameObject bulletObject = Instantiate(bulletPrefab, spawnPos, Quaternion.LookRotation(direction));

        // Передаём пуле параметры (как только BulletScript будет готов)
        BulletScript bullet = bulletObject.GetComponent<BulletScript>();
        bullet.Setup(direction, bulletSpeed, bulletRange, damage);
    }
}
