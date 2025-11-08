using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject enemyDiscPrefab;

    public float addEnemyMinTime = 1f;
    public float addEnemyMaxTime = 2f;

    public float addEnemyDiscMinTime = 5f;
    public float addEnemyDiscMaxTime = 10f;

    private float addEnemyTimeout = 1f;
    private float addEnemyDiscTimeout = 1f;

    void Start()
    {
        addEnemyTimeout = Random.Range(addEnemyMinTime, addEnemyMaxTime);
        addEnemyDiscTimeout = Random.Range(addEnemyDiscMinTime, addEnemyDiscMaxTime);
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

        addEnemyDiscTimeout -= Time.deltaTime;
        if (addEnemyDiscTimeout < 0f)
        {
            addEnemyDiscTimeout = Random.Range(addEnemyDiscMinTime, addEnemyDiscMaxTime);
            AddEnemyDisc();
        }
    }

    void AddEnemy()
    {
        GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }

    void AddEnemyDisc()
    {
        GameObject enemy = Instantiate(enemyDiscPrefab, transform.position, Quaternion.identity);
    }
}
