using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class TurretPlacement : MonoBehaviour
{
    private PlayerControls playerControls;

    [SerializeField]
    private Camera sceneCamera;

    public bool turretButton = false;

    [SerializeField]
    private GameObject mouseIndicator;
    [SerializeField]
    private InputManager inputManager;

    public GameObject turretPrefab;


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

    public void TurretOnClick(GameObject turret)
    {
        if (turretButton == false)
        {
            turretButton = true;
        }
        else
        {
            turretButton = false;
        }
        mouseIndicator.SetActive(true);
    }

    void TowerPlacement()
    {
        if (turretPrefab != null)
        {
            if (turretButton == true)
            {
                Ray ray = sceneCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

                if (Physics.Raycast(ray, out RaycastHit hit, 100f))
                {
                    Instantiate(turretPrefab, hit.point, Quaternion.identity);
                }
            }
            turretButton = false;
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
