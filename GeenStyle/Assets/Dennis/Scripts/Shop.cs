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
    bool shopEnabled;

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
    }

    public void DeactivateShop()
    {
        shopUI?.SetActive(false);
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
