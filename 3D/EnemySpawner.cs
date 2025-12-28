using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float addEnemyMinTime = 1f;
    public float addEnemyMaxTime = 2f;
    float addEnemyTimeout = 0f;

    void Start()
    {
        addEnemyTimeout = Random.Range(addEnemyMinTime, addEnemyMaxTime);
    }
    
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
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }
}
