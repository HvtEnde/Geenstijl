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

    [Header("Attributes")]
    [SerializeField]
    private Camera sceneCamera;
    [SerializeField]
    private GameObject mouseIndicator;
    [SerializeField]
    private InputManager inputManager;
    [SerializeField]
    private GameObject placementSystem;

    private bool regularTurretButton, sniperTurretButton, flamethrowerTurretButton, landmineButton = false;

    [Header("Prefabs")]
    public GameObject turretPrefab;
    public GameObject sniperPrefab;
    public GameObject flamethrowerPrefab;
    public GameObject landminePrefab;

    [Header("Turret Costs")]
    public int turretCost = 100;
    public int sniperCost = 125;
    public int flamethrowerCost = 150;
    public int landmineCost = 75;

    #region Awake & Update
    void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.Player.Place.performed += x => TowerPlacement();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        mouseIndicator.transform.position = mousePosition;
        OnHoverMouseColor();
    }
    #endregion

    #region Button Clicks
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
    #endregion

    #region Hover Color
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
                    if (!hit.collider.CompareTag("Path") && !hit.collider.CompareTag("Towers") && PlayerStats.money >= turretCost)
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
            if (sniperTurretButton == true)
            {
                Ray ray = sceneCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100f))
                {
                    if (!hit.collider.CompareTag("Path") && !hit.collider.CompareTag("Towers") && PlayerStats.money >= sniperCost)
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
            if (flamethrowerTurretButton == true)
            {
                Ray ray = sceneCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100f))
                {
                    if (!hit.collider.CompareTag("Path") && !hit.collider.CompareTag("Towers") && PlayerStats.money >= flamethrowerCost)
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
                    if (hit.collider.CompareTag("Path") && PlayerStats.money >= landmineCost)
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
    #endregion

    #region Tower Placement
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
                        if (PlayerStats.money < turretCost)
                        {
                            Debug.Log("Not enough currency");
                            mouseIndicator.SetActive(false);
                            return;
                        }
                        PlayerStats.money -= turretCost;

                        GameObject placedTurret = Instantiate(turretPrefab, hit.point, Quaternion.identity);

                        placedTurret.transform.SetParent(placementSystem.transform);

                        Debug.Log("Turret Build. Money Left: " + PlayerStats.money);
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
                        if (PlayerStats.money < sniperCost)
                        {
                            Debug.Log("Not enough currency");
                            mouseIndicator.SetActive(false);
                            return;
                        }
                        PlayerStats.money -= sniperCost;

                        GameObject placedTurret = Instantiate(sniperPrefab, hit.point, Quaternion.identity);

                        placedTurret.transform.SetParent(placementSystem.transform);

                        Debug.Log("Turret Build. Money Left: " + PlayerStats.money);
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
                        if (PlayerStats.money < flamethrowerCost)
                        {
                            Debug.Log("Not enough currency");
                            mouseIndicator.SetActive(false);
                            return;
                        }
                        PlayerStats.money -= flamethrowerCost;

                        GameObject placedTurret = Instantiate(flamethrowerPrefab, hit.point, Quaternion.identity);

                        placedTurret.transform.SetParent(placementSystem.transform);

                        Debug.Log("Turret Build. Money Left: " + PlayerStats.money);
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
                        if (PlayerStats.money < landmineCost)
                        {
                            Debug.Log("Not enough currency");
                            mouseIndicator.SetActive(false);
                            return;
                        }
                        PlayerStats.money -= landmineCost;

                        GameObject placedTurret = Instantiate(landminePrefab, hit.point, Quaternion.identity);

                        placedTurret.transform.SetParent(placementSystem.transform);

                        Debug.Log("Turret Build. Money Left: " + PlayerStats.money);
                    }
                }
            }
            landmineButton = false;
            mouseIndicator.SetActive(false);
        }
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
