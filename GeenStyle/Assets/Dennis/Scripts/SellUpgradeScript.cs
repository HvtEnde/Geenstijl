using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SellUpgradeScript : MonoBehaviour
{
    private PlayerControls playerControls;
    public GameObject target;

    [Header("Attributes")]
    [SerializeField]
    private Camera sceneCamera;

    [Header("UI Attributes")]
    public GameObject selectUI;

    #region Awake
    void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.Player.Select.performed += x => SelectTurret();
    }
    #endregion

    #region Select Turret
    void SelectTurret()
    {
        if (!selectUI.activeInHierarchy)
        {
            Ray ray = sceneCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f))
            {
                if (hit.collider.tag == "Towers")
                {
                    target = hit.transform.gameObject;
                    Debug.Log(target.transform.name);
                    selectUI.transform.position = hit.collider.transform.position;
                    selectUI.SetActive(true);
                }
            }
        }

        if (selectUI.activeInHierarchy)
        {
            Ray ray = sceneCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f))
            {
                if (hit.collider.tag == "Towers")
                {
                    target = hit.transform.gameObject;
                    selectUI.transform.position = hit.collider.transform.position;
                    selectUI.SetActive(true);
                }
                else
                {
                    return;
                }
            }
        }

        if (selectUI.activeInHierarchy)
        {
            Ray ray = sceneCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f))
            {
                if (hit.collider.tag != "Towers")
                {
                    selectUI.SetActive(false);
                }
            }
        }
    }
    #endregion

    #region Sell & Upgrade Turret
    public void SellTurret()
    {
        if (target)
        {
            int turretCost = target.GetComponent<TurretBehavior>().turretCost;
            PlayerStats.money += turretCost / 2;
            selectUI.SetActive(false);
            Destroy(target);
        }
    }

    public void UpgradeTurret()
    {

    }
    #endregion

    #region Mem Leaks
    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }
    #endregion
}
