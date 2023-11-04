using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DevTool : MonoBehaviour
{
    private PlayerControls playerControls;
    private InputAction devTool;

    [Header("Attributes")]
    [SerializeField]
    private GameObject devToolUI;
    [SerializeField]
    private GameObject winScreen;
    [SerializeField]
    private GameObject hudUI;
    [SerializeField]
    bool devToolEnabled;

    #region Awake
    void Awake()
    {
        playerControls = new PlayerControls();
    }
    #endregion

    #region Open Dev Tool
    void OpenDevTool(InputAction.CallbackContext context)
    {
        devToolEnabled = !devToolEnabled;

        if (devToolEnabled)
        {
            ActivateDevTool();
        }
        else
        {
            DeActivateDevTool();
        }
    }

    void ActivateDevTool()
    {
        devToolUI.SetActive(true);
    }

    public void DeActivateDevTool()
    {
        devToolUI.SetActive(false);
        devToolEnabled = false;
    }
    #endregion

    #region Button Functionality

    public void AddMoney()
    {
        PlayerStats.money += 500;
    }

    public void InstantWin()
    {
        winScreen.SetActive(true);
        hudUI.SetActive(false);
        PlayerPrefs.SetInt("levelReached", 2);
    }

    public void InstantLose()
    {
        PlayerStats.lives = 0;
    }
    #endregion

    #region Enable/Disable
    private void OnEnable()
    {
        devTool = playerControls.UI.DevTool;
        devTool.Enable();

        devTool.performed += OpenDevTool;
    }

    private void OnDisable()
    {
        devTool.Disable();
    }
    #endregion
}
