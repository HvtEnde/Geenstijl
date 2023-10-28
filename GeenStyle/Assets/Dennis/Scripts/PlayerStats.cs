using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    [Header("Player Money")]
    public static int money;
    public int startMoney = 250;
    public TMP_Text moneyText;

    [Header("Player Life")]
    public static int lives;
    public int startLives = 3;
    public TMP_Text lifeText;

    // Start is called before the first frame update
    void Start()
    {
        money = startMoney;
        lives = startLives;
    }

    // Update is called once per frame
    void Update()
    {
        moneyText.text = "AU$" + PlayerStats.money.ToString();
        lifeText.text = PlayerStats.lives.ToString();
    }
}
