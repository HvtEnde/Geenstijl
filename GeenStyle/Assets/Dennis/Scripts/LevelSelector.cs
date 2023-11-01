using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{

    public void LevelSelect1()
    {
        SceneManager.LoadScene(3);
    }

    public void LevelSelect2()
    {
        SceneManager.LoadScene(4);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void ReturnToLevelSelect()
    {
        SceneManager.LoadScene(2);
    }
}
