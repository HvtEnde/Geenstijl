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

    public bool regularTurretButton, sniperTurretButton, flamethrowerTurretButton, landmineButton = false;

    [SerializeField]
    private GameObject mouseIndicator;
    [SerializeField]
    private InputManager inputManager;

    public GameObject turretPrefab, sniperPrefab, flamethrowerPrefab, landminePrefab;


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
        OnHoverMouseColor();
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

    public void TurretOnClickFlamethrower()
    {
        if (flamethrowerTurretButton == false)
        {
            flamethrowerTurretButton = true;
            mouseIndicator.SetActive(true);
        }
        else
        {
            flamethrowerTurretButton = false;
            mouseIndicator.SetActive(false);
        }
    }

    public void TurretOnClickLandmine()
    {
        if (landmineButton == false)
        {
            landmineButton = true;
            mouseIndicator.SetActive(true);
        }
        else
        {
            landmineButton = false;
            mouseIndicator.SetActive(false);
        }
    }

    void OnHoverMouseColor()
    {
        if (mouseIndicator.activeInHierarchy)
        {
            if (regularTurretButton || sniperTurretButton || flamethrowerTurretButton == true)
            {
                Ray ray = sceneCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100f))
                {
                    if (!hit.collider.CompareTag("Path") && !hit.collider.CompareTag("Towers"))
                    {
                        mouseIndicator.GetComponent<Renderer>().material.color = Color.green;
                    }
                    else
                    {
                        mouseIndicator.GetComponent<Renderer>().material.color = Color.red;
                    }
                }
            }
        }

        if (mouseIndicator.activeInHierarchy)
        {
            if (landmineButton == true)
            {
                Ray ray = sceneCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100f))
                {
                    if (hit.collider.CompareTag("Path"))
                    {
                        mouseIndicator.GetComponent<Renderer>().material.color = Color.green;
                    }
                    else
                    {
                        mouseIndicator.GetComponent<Renderer>().material.color = Color.red;
                    }
                }
            }
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

        if (flamethrowerPrefab != null)
        {
            if (flamethrowerTurretButton == true)
            {
                Ray ray = sceneCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100f))
                {
                    if (!hit.collider.CompareTag("Path") && !hit.collider.CompareTag("Towers"))
                    {
                        Instantiate(flamethrowerPrefab, hit.point, Quaternion.identity);
                    }
                }
            }
            flamethrowerTurretButton = false;
            mouseIndicator.SetActive(false);
        }

        if (landminePrefab != null)
        {
            if (landmineButton == true)
            {
                Ray ray = sceneCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100f))
                {
                    if (hit.collider.CompareTag("Path"))
                    {
                        Instantiate(landminePrefab, hit.point, Quaternion.identity);
                    }
                }
            }
            landmineButton = false;
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
