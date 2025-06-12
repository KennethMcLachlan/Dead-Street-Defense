using UnityEngine;
using UnityEngine.InputSystem;

public class Draggable : MonoBehaviour
{
    private Camera _mainCamera;
    public bool isDragging;
    private LayerMask _groundMask;

    private void Start()
    {
        _mainCamera = Camera.main;
        _groundMask = LayerMask.GetMask("Ground");
    }

    private void Update()
    {
        if (isDragging)
        {
            Ray ray = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(ray, out RaycastHit hit, 100f, _groundMask))
            {
                transform.position = hit.point;
            }

            if (PlayerInputControls.Instance._actionButton.action.WasReleasedThisFrame())
            {
                isDragging = false;
            }
        }
    }

    public void BeginDrag()
    {
        isDragging = true;
    }
}
