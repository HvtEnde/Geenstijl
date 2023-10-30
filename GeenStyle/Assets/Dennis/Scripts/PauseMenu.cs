using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private PlayerControls playerControls;

    [Header("Attributes")]
    [SerializeField]
    private GameObject pauseUI;
    [SerializeField]
    private GameObject hudUI;

    void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.UI.Pause.performed += x => PauseGame();
    }

    void PauseGame()
    {
         pauseUI.SetActive(!pauseUI.activeSelf);

        if (pauseUI.activeSelf)
        {
            hudUI.SetActive(false);
            Time.timeScale = 0f;

        }
        else
        {
            hudUI.SetActive(true);
            Time.timeScale = 1f;
        }
    }

    public void Retry()
    {
        PauseGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public void QuitGame()
    {
        PauseGame();
        SceneManager.LoadScene(1);
    }
}
