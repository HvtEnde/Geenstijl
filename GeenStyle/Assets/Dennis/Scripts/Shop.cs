using UnityEngine;
using UnityEngine.InputSystem;

public class Shop : MonoBehaviour
{
    private PlayerControls playerControls;
    private InputAction shopInput;

    [Header("Attributes")]
    [SerializeField]
    private GameObject shopUI;
    [SerializeField] 
    private GameObject shopIconEnable;
    [SerializeField]
    private GameObject shopIconDisable;
    public bool shopEnabled;

    #region Awake
    private void Awake()
    {
        playerControls = new PlayerControls();
    }
    #endregion

    #region Shop Functionality
    void OpenShop(InputAction.CallbackContext context)
    {
        shopEnabled = !shopEnabled;

        if (shopEnabled)
        {
            ActivateShop();
        }
        else
        {
            DeactivateShop();
        }
    }

    void ActivateShop()
    {
        shopUI.SetActive(true);
        shopIconEnable.SetActive(false);
        shopIconDisable.SetActive(true);
    }

    public void DeactivateShop()
    {
        shopUI.SetActive(false);
        shopEnabled = false;
        shopIconDisable.SetActive(false);
        shopIconEnable.SetActive(true);
    }
    #endregion

    #region Shop Functionality (Button)

    public void OnClickShopEnabled()
    {
        shopEnabled = true;
    }

    public void OnClickShopDisabled()
    {
        shopEnabled = false;
    }
    #endregion

    #region Enable/Disable
    private void OnEnable()
    {
        shopInput = playerControls.UI.Shop;
        shopInput.Enable();

        shopInput.performed += OpenShop;
    }

    private void OnDisable()
    {
        shopInput.Disable();
    }
    #endregion
}
