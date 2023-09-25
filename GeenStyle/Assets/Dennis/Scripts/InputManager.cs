using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Editor;

public class InputManager : MonoBehaviour
{
    [SerializeField] 
    private Camera sceneCamera;

    public InputAction mouseControl;

    private Vector3 lastPosition;

    [SerializeField]
    private LayerMask placementLayermask;

    public Vector3 GetSelectedMapPosition()
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        mousePos.z = sceneCamera.nearClipPlane;
        Ray ray = sceneCamera.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, placementLayermask))
        {
            lastPosition = hit.point;
        }
        return lastPosition;
    }

    private void OnEnable()
    {
        mouseControl.Enable();
    }

    private void OnDisable()
    {
        mouseControl.Disable();
    }
}
