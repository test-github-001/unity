using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;     // Префаб пули

    public float addEnemyMinTime = 1f;
    public float addEnemyMaxTime = 2f;

    private float addEnemyTimeout = 1f;

    void Start()
    {
        addEnemyTimeout = Random.Range(addEnemyMinTime, addEnemyMaxTime);
    }

    // Update is called once per frame
    void Update()
    {
        addEnemyTimeout -= Time.deltaTime;
        if (addEnemyTimeout < 0f)
        {
            addEnemyTimeout = Random.Range(addEnemyMinTime, addEnemyMaxTime);
            AddEnemy();
        }
    }

    void AddEnemy()
    {
        GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }
}
