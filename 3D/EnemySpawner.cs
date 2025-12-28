using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    bool isGenerate = true;
    float fixTime = 5f;
    float time = 30f;
    
    void FixedUpdate()
    {
    if(isGenerate == true)
    {
    Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    StartCoroutine(StartTimer());
    isGenerate = false;
    }
    }
    IEnumerator StartTimer()
    {
    yield return new WaitForSeconds(time);
    isGenerate = true;
    }
}
