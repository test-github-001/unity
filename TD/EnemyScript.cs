using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [Header("Характеристики")]
    public float hp = 30;
    public int damage = 1;          // урон базе при достижении
    public float speed = 3f;
    public int goldReward = 5;      // золото за убийство

    private int currentWaypointIndex = 0;
    private Transform targetPoint;   // текущая цель (трансформ точки)
    private SpawnScript spawner;
    // Start is called before the first frame update
    void Start()
    {
        spawner = SpawnScript.Instance;
        RequestNextWaypoint();
    }

    // Update is called once per frame
    void Update()
    {

        if (targetPoint == null) return;

        // Движение к цели
        Vector3 direction = (targetPoint.position - transform.position).normalized;
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

        // Поворот лицом к цели (чтобы было визуально приятно)
        if (direction != Vector3.zero) transform.rotation = Quaternion.LookRotation(direction);

        // Проверка: дошли до текущей точки?
        if (Vector3.Distance(transform.position, targetPoint.position) < 0.3f) RequestNextWaypoint();
    }

    void RequestNextWaypoint()
    {
        
        Transform newTarget = spawner.GetWaypoint(currentWaypointIndex);
        currentWaypointIndex++;

        if (newTarget != null) targetPoint = newTarget;
        else AttackBaseAndDie();
    }

    void AttackBaseAndDie()
    {
        // Наносим урон базе
        PlayerScript.Instance.AddDamage(damage);
        spawner.AddKill();
        Destroy(gameObject);
    }

    // Вызывается башнями при попадании
    public void AddDamage(float value)
    {
        hp -= value;
        if (hp > 0) return;

        PlayerScript.Instance.AddGold(goldReward);
        spawner.AddKill();
        Destroy(gameObject);
    }

    // Дополнительная страховка: если враг коллизией коснулся базы
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BaseTag"))
        {
            AttackBaseAndDie();
        }
    }
}