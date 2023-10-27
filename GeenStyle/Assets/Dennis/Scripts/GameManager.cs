using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool gameEnded = false;

    public GameObject hudUI, gameOverUI;


    void Update()
    {
        if (gameEnded)
        {
            return;
        }

        if (PlayerStats.lives <= 0)
        {
            Time.timeScale = 0;
            EndGame();
        }
    }

    void EndGame()
    {
        gameEnded = true;
        hudUI.SetActive(false);
        gameOverUI.SetActive(true);
    }
}
