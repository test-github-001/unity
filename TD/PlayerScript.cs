using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

    public static PlayerScript Instance;   // чтобы враги и спавнер могли обращаться без поисков
    // В любом другом скрипте достаточно написать: Player.Instance.TakeDamage(1); // для вызова TakeDamage(1)

    [Header("Настройки")]
    public int maxHP = 10;
    public int startGold = 100;

    private int HP;
    private int Gold;

    [Header("UI")]
    public Text hpText;
    public Text goldText;
    public GameObject gameOverText;   // сюда перетащи скрытый GameOverText
    public GameObject winText;        // сюда перетащи скрытый WinText

    void Awake() // вызывается раньше, чем Start()
    {
        if (Instance == null) Instance = this; // если ещё никто не записал себя в Instance, то записываем себя
        else Destroy(gameObject);     // иначе уже есть другой – уничтожаем этот дубликат
    }

    void Start()
    {
        HP = maxHP;
        Gold = startGold;
        UpdateHPText();
        UpdateGoldText();
        gameOverText.SetActive(false);
        winText.SetActive(false);
    }

    public void AddDamage(int damage)
    {
        if (HP <= 0) return;

        HP -= damage;
        if (HP < 0) HP = 0;
        UpdateHPText();

        if (HP <= 0) gameOverText.SetActive(true);
    }

    public void AddGold(int value)
    {
        Gold += value;
        UpdateGoldText();
    }

    public void ShowWin()
    {
        winText.SetActive(true);
    }

    public bool CheckGold(int value)
    {
        return Gold >= value;
    }

    public bool CheckHP()
    {
        return HP > 0;
    }

    void UpdateHPText()
    {
        hpText.text = $"HP: {HP}";
    }

    void UpdateGoldText()
    {
        goldText.text = $"Gold: {Gold}";
    }
}