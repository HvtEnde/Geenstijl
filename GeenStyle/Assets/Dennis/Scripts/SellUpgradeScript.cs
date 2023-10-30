using UnityEngine;
using UnityEngine.InputSystem;

public class SellUpgradeScript : MonoBehaviour
{
    private PlayerControls playerControls;
    public GameObject target;
    [SerializeField]
    private GameObject placementSystem;

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
        bool turretUpgraded = target.GetComponent<TurretBehavior>().turretUpgraded;
        if (target)
        {
            int turretCost = target.GetComponent<TurretBehavior>().turretCost;
            PlayerStats.money += turretCost / 2;
            selectUI.SetActive(false);
            Destroy(target);
        }

        if (turretUpgraded == true)
        {
            int turretCost = target.GetComponent<TurretBehavior>().turretCost;
            PlayerStats.money += turretCost / 2;
            selectUI.SetActive(false);
            Destroy(target);
        }
    }

    public void UpgradeTurret()
    {
        int turretCost = target.GetComponent<TurretBehavior>().turretCost;
        if (target)
        {
            if (PlayerStats.money < turretCost)
            {
                Debug.Log("Not enough currency");
                selectUI.SetActive(false);
                return;
            }

            PlayerStats.money -= turretCost;

            GameObject turretUpgrade = target.GetComponent<TurretBehavior>().turretUpgrade;

            GameObject placedTurret = Instantiate(turretUpgrade, target.transform.position, Quaternion.identity);

            placedTurret.transform.SetParent(placementSystem.transform);

            Destroy(target);

            Debug.Log("Turret Upgraded");

            selectUI.SetActive(false);

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
