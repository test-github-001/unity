using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    public GameObject bulletPrefab;     // Префаб пули
    public Transform firePoint;         // Точка появления пули

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        firePoint = GetComponent<Transform>();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.LeftControl) || Input.GetKeyUp(KeyCode.RightControl))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // Направление в зависимости от флипа спрайта
        float direction = spriteRenderer.flipX ? -1f : 1f;

        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.SetDirection(direction);
        }

    }
}
