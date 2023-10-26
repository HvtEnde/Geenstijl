using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{

    public static int money;
    public int startMoney = 200;
    public TMP_Text moneyText;

    // Start is called before the first frame update
    void Start()
    {
        money = startMoney;
    }

    // Update is called once per frame
    void Update()
    {
        moneyText.text = "AU$" + PlayerStats.money.ToString();
    }
}
