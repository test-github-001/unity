using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    private Vector3 startPos;
    private Vector3 targetPos;
    private float maxHeight;
    private float flightTime;
    private int damage;
    private float aoeRadius;
    private float startTime;

    public void Setup(Vector3 target, float height, float time, int dmg, float radius)
    {
        startPos = transform.position;
        targetPos = target;
        maxHeight = height;
        flightTime = time;
        damage = dmg;
        aoeRadius = radius;
        startTime = Time.time;
    }

    void Update()
    {
        float elapsed = Time.time - startTime;
        float fraction = elapsed / flightTime;

        if (fraction >= 1f)
        {
            Explode();
            return;
        }

        // Движение по горизонтали (линейная интерполяция)
        Vector3 horizontalPos = Vector3.Lerp(startPos, targetPos, fraction);
        // Добавляем дугу: высота по синусу
        float yOffset = maxHeight * Mathf.Sin(fraction * Mathf.PI);
        transform.position = horizontalPos + Vector3.up * yOffset;
    }

    void Explode()
    {
        // Урон всем врагам в радиусе
        Collider[] hits = Physics.OverlapSphere(transform.position, aoeRadius);
        foreach (Collider hit in hits)
        {
            EnemyScript enemy = hit.GetComponent<EnemyScript>();
            if (enemy != null)
                enemy.AddDamage(damage);
        }

        // Создаём расширяющуюся оранжевую сферу
        SpawnExplosionEffect();

        Destroy(gameObject);
    }

    void SpawnExplosionEffect()
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = transform.position;
        sphere.transform.localScale = Vector3.zero;

        // Убираем коллайдер, чтобы не мешал
        Destroy(sphere.GetComponent<Collider>());

        // Оранжевый полупрозрачный материал
        Material mat = new Material(Shader.Find("Sprites/Default"));
        mat.color = new Color(1f, 0.5f, 0f, 0.75f);
        sphere.GetComponent<Renderer>().material = mat;

        // Аниматор расширения и затухания
        ExplosionSphereAnimator anim = sphere.AddComponent<ExplosionSphereAnimator>();
        anim.duration = 0.3f;
        anim.targetRadius = aoeRadius;
        anim.startAlpha = 0.75f;
    }
}
