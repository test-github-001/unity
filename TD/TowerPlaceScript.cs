using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.ObjectChangeEventStream;

public class TowerPlaceScript : MonoBehaviour
{
    public bool isEmpty = true;   // свободна ли площадка

    private BuildUIScript buildUI;    // ссылка на общую панель строительства

    void Start()
    {
        // »щем панель на сцене (она одна)
        buildUI = FindObjectOfType<BuildUIScript>();
    }

    void OnMouseDown()
    {
        if (!isEmpty) return;

        buildUI.Show(this);
    }

    public void AddTower(GameObject towerPrefab, int cost)
    {
        if (!isEmpty) return;
        if (!PlayerScript.Instance.CheckGold(cost)) return;

        PlayerScript.Instance.AddGold(-cost);

        Vector3 pos = transform.position + Vector3.up * 0.5f;
        Instantiate(towerPrefab, pos, Quaternion.identity);

        isEmpty = false;
    }
}
