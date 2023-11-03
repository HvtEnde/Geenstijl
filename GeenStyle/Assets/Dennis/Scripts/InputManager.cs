using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private Camera sceneCamera;

    private Vector3 lastPosition;
    private PlayerControls playerControls;

    [SerializeField]
    private LayerMask placementLayerMask;

    #region Awake
    private void Awake()
    {
        playerControls = new PlayerControls();
    }
    #endregion

    #region Map Position
    public Vector3 GetSelectedMapPosition()
    {
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        mousePosition.z = sceneCamera.nearClipPlane;
        Ray ray = sceneCamera.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100, placementLayerMask))
        {
            lastPosition = hit.point;
        }
        return lastPosition;
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
