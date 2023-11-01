using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public Button[] levelButtons;

    void Start()
    {
        int levelReached = PlayerPrefs.GetInt("levelReached", 1);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i + 1 > levelReached)
            {
                levelButtons[i].interactable = false;
            }
        }
    }

    #region Level Select in Level Selector
    public void LevelSelect1()
    {
        SceneManager.LoadScene(3);
    }

    public void LevelSelect2()
    {
        SceneManager.LoadScene(4);
    }
    #endregion

    #region Level Select in Main Game
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void ContinueNextLevel()
    {
        SceneManager.LoadScene(4);
    }

    public void ReturnToLevelSelect()
    {
        SceneManager.LoadScene(2);
    }
    #endregion
}
