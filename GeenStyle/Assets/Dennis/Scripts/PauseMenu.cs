using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    private PlayerControls playerControls;
    private InputAction ui;

    [Header("Attributes")]
    [SerializeField]
    private GameObject pauseUI;
    [SerializeField]
    private GameObject hudUI;
    [SerializeField]
    private GameObject settingsUI;
    [SerializeField]
    private bool isPaused;

    #region Awake
    void Awake()
    {
        playerControls = new PlayerControls();
    }
    #endregion

    #region Pause Functionality
    void PauseGame(InputAction.CallbackContext context)
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            ActivatePauseUI();
        }
        else
        {
            DeactivatePauseUI();
        }
    }

    void ActivatePauseUI()
    {
        hudUI.SetActive(false);
        Time.timeScale = 0;
        pauseUI.SetActive(true);
        ui.Disable();
    }

    public void DeactivatePauseUI()
    {
        hudUI?.SetActive(true);
        Time.timeScale = 1;
        pauseUI.SetActive(false);
        isPaused = false;
        ui.Enable();
    }
    #endregion

    #region Button Functions
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void SettingsInGame()
    {
        pauseUI.SetActive(false);
        settingsUI.SetActive(true);
    }

    public void QuitGame()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }
    #endregion

    #region Enable/Disable
    private void OnEnable()
    {
        ui = playerControls.UI.Pause;
        ui.Enable();

        ui.performed += PauseGame;
    }

    private void OnDisable()
    {
        ui.Disable();
    }
    #endregion
}
