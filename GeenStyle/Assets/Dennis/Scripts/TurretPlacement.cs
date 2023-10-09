using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class TurretPlacement : MonoBehaviour
{
    private PlayerControls playerControls;

    [SerializeField]
    private Camera sceneCamera;

    public bool regularTurretButton, sniperTurretButton = false;

    [SerializeField]
    private GameObject mouseIndicator;
    [SerializeField]
    private InputManager inputManager;

    public GameObject turretPrefab, sniperPrefab;


    void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.Player.Select.performed += x => TowerPlacement();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        mouseIndicator.transform.position = mousePosition;
    }

    public void TurretOnClickRegular()
    {
        if (regularTurretButton == false)
        {
            regularTurretButton = true;
            mouseIndicator.SetActive(true);
        }
        else
        {
            regularTurretButton = false;
            mouseIndicator.SetActive(false);
        }
    }

    public void TurretOnClickSniper()
    {
        if (regularTurretButton == false)
        {
            sniperTurretButton = true;
            mouseIndicator.SetActive(true);
        }
        else
        {
            sniperTurretButton = false;
            mouseIndicator.SetActive(false);
        }
    }

    void TowerPlacement()
    {
        if (turretPrefab != null)
        {
            if (regularTurretButton == true)
            {
                Ray ray = sceneCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100f))
                {
                    if (!hit.collider.CompareTag("Path") && !hit.collider.CompareTag("Towers"))
                    {
                        Instantiate(turretPrefab, hit.point, Quaternion.identity);
                    }
                }
            }
            regularTurretButton = false;
            mouseIndicator.SetActive(false);
        }

        if (sniperPrefab != null)
        {
            if (sniperTurretButton == true)
            {
                Ray ray = sceneCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100f))
                {
                    if (!hit.collider.CompareTag("Path") && !hit.collider.CompareTag("Towers"))
                    {
                        Instantiate(sniperPrefab, hit.point, Quaternion.identity);
                    }
                }
            }
            sniperTurretButton = false;
            mouseIndicator.SetActive(false);
        }
    }

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
