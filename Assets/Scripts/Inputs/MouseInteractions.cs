using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class MouseInteractions : MonoBehaviour
{
    private Camera _mainCamera;
    void Start()
    {
        _mainCamera = GetComponent<Camera>();

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void Update()
    {
        //var mouse = PlayerInputControls.Instance._lookAction.action;
        //Vector2 mouseMovement = mouse.ReadValue<Vector2>();

        PlayerInputControls.Instance._lookAction.action.ReadValue<Vector2>();

        if (PlayerInputControls.Instance._actionButton.action.WasPressedThisFrame())
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            Ray ray = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                IClickable clickable = hit.collider.GetComponent<IClickable>();
                if (clickable != null)
                {
                    clickable.OnClick();
                }
            }
        }
    }
}
