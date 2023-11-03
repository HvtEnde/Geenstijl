using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool gameEnded = false;

    public GameObject hudUI, gameOverUI;

    #region Update
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
    #endregion

    #region End Game Functionality
    void EndGame()
    {
        gameEnded = true;
        hudUI.SetActive(false);
        gameOverUI.SetActive(true);
    }
    #endregion

    #region Win Level Functionality
    public void WinLevel()
    {
        PlayerPrefs.SetInt("levelReached", 2);
    }
    #endregion
}
