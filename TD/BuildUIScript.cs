using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildUIScript : MonoBehaviour
{
    [Header("Кнопки и панель")]
    public GameObject panel;               // сама панель (можно этот же объект)
    public Button miniGunButton;
    public Button bombGunButton;
    public Button cancelButton;

    [Header("Префабы башен")]
    public GameObject miniGunPrefab;
    public GameObject bombGunPrefab;
    public int miniGunCost = 20;
    public int bombGunCost = 30;

    private TowerPlaceScript targetPlace = null;   // площадка, на которой строим

    void Start()
    {
        panel.SetActive(false); // панель скрыта при старте

        miniGunButton.onClick.AddListener(BuildMiniGun);
        bombGunButton.onClick.AddListener(BuildBombGun);
        cancelButton.onClick.AddListener(HidePanel);
    }

    // Update is called once per frame
    public void Show(TowerPlaceScript place)
    {
        targetPlace = place;
        panel.SetActive(true);
    }

    void BuildMiniGun()
    {
        if (targetPlace == null) return;

        targetPlace.AddTower(miniGunPrefab, miniGunCost);
        HidePanel();
    }

    void BuildBombGun()
    {
        if (targetPlace == null) return;

        targetPlace.AddTower(bombGunPrefab, bombGunCost);
        HidePanel();
    }

    void HidePanel()
    {
        panel.SetActive(false);
        targetPlace = null;
    }
}
