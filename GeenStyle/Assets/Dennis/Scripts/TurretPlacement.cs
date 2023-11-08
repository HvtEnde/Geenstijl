using UnityEngine;
using UnityEngine.InputSystem;

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

    [Header("Range Effects")]
    public GameObject regularTurretRange;
    public GameObject sniperTurretRange;
    public GameObject flamethrowerTurretRange;

    [Header("Turret Costs")]
    public int turretCost;
    public int sniperCost;
    public int flamethrowerCost;
    public int landmineCost;

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
            if (regularTurretButton)
            {
                Ray ray = sceneCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100f))
                {
                    regularTurretRange.SetActive(true);
                    regularTurretRange.transform.position = hit.point;

                    if (!hit.collider.CompareTag("Path") && !hit.collider.CompareTag("Towers") && !hit.collider.CompareTag("Prop") && PlayerStats.money >= turretCost)
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
                    sniperTurretRange.SetActive(true);
                    sniperTurretRange.transform.position = hit.point;

                    if (!hit.collider.CompareTag("Path") && !hit.collider.CompareTag("Towers") && !hit.collider.CompareTag("Prop") && PlayerStats.money >= sniperCost)
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
                    flamethrowerTurretRange.SetActive(true);
                    flamethrowerTurretRange.transform.position = hit.point;

                    if (!hit.collider.CompareTag("Path") && !hit.collider.CompareTag("Towers") && !hit.collider.CompareTag("Prop") && PlayerStats.money >= flamethrowerCost)
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
                    Debug.Log(hit.collider.tag);
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
                    if (!hit.collider.CompareTag("Path") && !hit.collider.CompareTag("Towers") && !hit.collider.CompareTag("Prop"))
                    {
                        if (PlayerStats.money < turretCost)
                        {
                            Debug.Log("Not enough currency");
                            mouseIndicator.SetActive(false);
                            regularTurretRange.SetActive(false);
                            sniperTurretRange.SetActive(false);
                            flamethrowerTurretRange.SetActive(false);
                            return;
                        }
                        PlayerStats.money -= turretCost;

                        GameObject placedTurret = Instantiate(turretPrefab, hit.point, Quaternion.identity);

                        placedTurret.transform.SetParent(placementSystem.transform);

                        regularTurretRange.SetActive(false);

                        Debug.Log("Turret Build. Money Left: " + PlayerStats.money);
                    }
                    else
                    {
                        regularTurretRange.SetActive(false);
                        sniperTurretRange.SetActive(false);
                        flamethrowerTurretRange.SetActive(false);
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
                    if (!hit.collider.CompareTag("Path") && !hit.collider.CompareTag("Towers") && !hit.collider.CompareTag("Prop"))
                    {
                        if (PlayerStats.money < sniperCost)
                        {
                            Debug.Log("Not enough currency");
                            mouseIndicator.SetActive(false);
                            regularTurretRange.SetActive(false);
                            sniperTurretRange.SetActive(false);
                            flamethrowerTurretRange.SetActive(false);
                            return;
                        }
                        PlayerStats.money -= sniperCost;

                        GameObject placedTurret = Instantiate(sniperPrefab, hit.point, Quaternion.identity);

                        placedTurret.transform.SetParent(placementSystem.transform);

                        sniperTurretRange.SetActive(false);

                        Debug.Log("Turret Build. Money Left: " + PlayerStats.money);
                    }
                    else
                    {
                        regularTurretRange.SetActive(false);
                        sniperTurretRange.SetActive(false);
                        flamethrowerTurretRange.SetActive(false);
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
                    if (!hit.collider.CompareTag("Path") && !hit.collider.CompareTag("Towers") && !hit.collider.CompareTag("Prop"))
                    {
                        if (PlayerStats.money < flamethrowerCost)
                        {
                            Debug.Log("Not enough currency");
                            mouseIndicator.SetActive(false);
                            regularTurretRange.SetActive(false);
                            sniperTurretRange.SetActive(false);
                            flamethrowerTurretRange.SetActive(false);
                            return;
                        }
                        PlayerStats.money -= flamethrowerCost;

                        GameObject placedTurret = Instantiate(flamethrowerPrefab, hit.point, Quaternion.identity);

                        placedTurret.transform.SetParent(placementSystem.transform);

                        flamethrowerTurretRange.SetActive(false);

                        Debug.Log("Turret Build. Money Left: " + PlayerStats.money);
                    }
                    else
                    {
                        regularTurretRange.SetActive(false);
                        sniperTurretRange.SetActive(false);
                        flamethrowerTurretRange.SetActive(false);
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
