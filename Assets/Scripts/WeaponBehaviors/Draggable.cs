using UnityEngine;
using UnityEngine.InputSystem;

public class Draggable : MonoBehaviour
{
    private Camera _mainCamera;
    private LayerMask _groundMask;
    private ColorChange _colorChange;

    public bool isDragging;
    private bool _isInZone;

    private void Start()
    {
        _mainCamera = Camera.main;
        _groundMask = LayerMask.GetMask("Ground");
        _colorChange = GetComponent<ColorChange>();
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

            if (_isInZone)
            {
                _colorChange.TurnGreen();
            }
            else
            {
                _colorChange.TurnRed();
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

    public void IsInZone()
    {
        _isInZone = true;
    }

    public void NotInZone()
    {
        _isInZone = false;
    }
}
