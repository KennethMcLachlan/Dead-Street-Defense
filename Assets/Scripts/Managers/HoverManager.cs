using UnityEngine;
using UnityEngine.InputSystem;

public class HoverManager : MonoBehaviour
{
    private Camera _mainCamera;
    private IHoverable _currentHoverable;

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            IHoverable hoverTarget = hit.collider.GetComponent<IHoverable>();

            if (hoverTarget != _currentHoverable)
            {
                if (_currentHoverable != null && (_currentHoverable as UnityEngine.Object) != null)
                {
                    _currentHoverable.OnHoverExit();
                }

                _currentHoverable = hoverTarget;

                if (_currentHoverable != null && (_currentHoverable as UnityEngine.Object) != null)
                    _currentHoverable.OnHoverEnter();
            }
        }
        else
        {
            if (_currentHoverable != null && (_currentHoverable as UnityEngine.Object) != null)
            {
                _currentHoverable.OnHoverExit();
                _currentHoverable = null;
            }
            else
            {
                _currentHoverable = null;
            }
        }
    }
}
