using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    public static int money;
    public int startMoney = 200;
    public TMP_Text moneyText;

    public static int lives;
    public int startLives = 3;

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
    }
}
