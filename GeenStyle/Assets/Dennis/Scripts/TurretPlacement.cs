using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretPlacement : MonoBehaviour
{
    [SerializeField] Camera sceneCamera;
    private PlayerControls playerControls;

    public GameObject turretPrefab;


    // Start is called before the first frame update
    void Start()
    {
        playerControls = new PlayerControls();
    }

    // Update is called once per frame
    void Update()
    {
        if (turretPrefab != null)
        {

        }
    }

    void TurretOnClick(GameObject turret)
    {
        turretPrefab = Instantiate(turret, Vector3.zero, Quaternion.identity);
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
