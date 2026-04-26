using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombGunTowerScript : MonoBehaviour
{
    [Header("Параметры")]
    public float range = 6f;            // радиус поиска цели
    public float fireRate = 0.5f;       // выстрелов в секунду
    public int damage = 25;
    public float aoeRadius = 3f;        // радиус взрыва
    public float maxHeight = 25f;       // высшая точка дуги
    public float flightTime = 1.5f;     // время полёта бомбы до цели

    [Header("Префаб бомбы")]
    public GameObject bombPrefab;       // префаб со скриптом BombProjectile

    private float fireTimer = 0f;
    private EnemyScript currentTarget;

    void Update()
    {
        if (fireTimer > 0f)
        {
            fireTimer -= Time.deltaTime;
            return;
        }

        FindHighestHPEnemy();
        if (currentTarget == null) return;

        Shoot();
        fireTimer = 1f / fireRate;
    }

    void FindHighestHPEnemy()
    {
        EnemyScript[] enemies = FindObjectsOfType<EnemyScript>();
        float maxHP = 0;
        EnemyScript best = null;

        foreach (EnemyScript enemy in enemies)
        {
            if (enemy == null) continue;
            float dist = Vector3.Distance(transform.position, enemy.transform.position);
            if (dist <= range && enemy.hp > maxHP)
            {
                maxHP = enemy.hp;
                best = enemy;
            }
        }
        currentTarget = best;
    }

    void Shoot()
    {
        if (bombPrefab == null || currentTarget == null) return;

        // Запоминаем позицию самого жирного врага в момент выстрела
        Vector3 targetPos = currentTarget.transform.position;

        // Создаём бомбу в центре башни
        GameObject bombObj = Instantiate(bombPrefab, transform.position, Quaternion.identity);
        BombScript bomb = bombObj.GetComponent<BombScript>();
        bomb.Setup(targetPos, maxHeight, flightTime, damage, aoeRadius);
    }
}
