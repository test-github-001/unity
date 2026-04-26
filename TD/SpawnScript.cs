using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class SpawnScript : MonoBehaviour
{
    public static SpawnScript Instance;   // чтобы враги могли обращаться без поисков
    // В любом другом скрипте достаточно написать: SpawnScript.Instance.TakeDamage(1); // для вызова TakeDamage(1)

    [Header("Настройки")]
    public int[] enemiesIndex; // 0 - слабый быстрый, 1 - сильный медленный
    private int currentEnemyIndex = 0;
    private int kills = 0;
    private int targetKills;

    [Header("Префабы врагов")]
    public GameObject smallEnemy;
    public GameObject bigEnemy;

    [Header("Маршрут")]
    public Transform[] waypoints;   // 4 точки (без базы)
    public Transform basePoint;     // объект Базы

    [Header("UI")]
    public Text killsText;
    public float spawnTimeout;

    private float spawnTime;

    void Awake() // вызывается раньше, чем Start()
    {
        if (Instance == null) Instance = this; // если ещё никто не записал себя в Instance, то записываем себя
        else Destroy(gameObject);     // иначе уже есть другой – уничтожаем этот дубликат
    }

    void Start()
    {
        spawnTime = spawnTimeout;
        targetKills = enemiesIndex.Length;
        UpdateKillsText();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentEnemyIndex == enemiesIndex.Length) return;

        spawnTime -= Time.deltaTime;
        if (spawnTime > 0) return;

        spawnTime += spawnTimeout;
        int firstEnemyType = enemiesIndex[currentEnemyIndex];
        currentEnemyIndex++;
        AddEnemy(firstEnemyType);
    }

    public void AddKill()
    {
        kills++;
        UpdateKillsText();

        if (kills == targetKills) PlayerScript.Instance.ShowWin();
    }

    void UpdateKillsText()
    {
        killsText.text = $"Kills: {kills} / {targetKills}";
    }

    void AddEnemy(int enemyType)
    {
        print(enemyType);
        GameObject prefab = enemyType == 0 ? smallEnemy : bigEnemy;
        Instantiate(prefab, transform.position, Quaternion.identity);
    }

    public Transform GetWaypoint(int index)
    {
        if (index < waypoints.Length) return waypoints[index];
        else if (index == waypoints.Length) return basePoint;
        else return null; // маршрут закончен
    }
}
