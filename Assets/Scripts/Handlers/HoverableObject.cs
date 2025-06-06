using UnityEngine;
using UnityEngine.InputSystem;

public class HoverableObject : MonoBehaviour, IHoverable
{
    [SerializeField] private Camera _mainCamera;

    private IHoverable _currentHoverable;

    private void Start()
    {
        //_mainCamera = GetComponent<Camera>();
    }

    private void Update()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            IHoverable hoverTarget = hit.collider.GetComponent<IHoverable>();

            if (hoverTarget != _currentHoverable)
            {
                Debug.Log("Mouse is hovering over the hoverable object");

                if (_currentHoverable != null)
                {
                    _currentHoverable.OnHoverExit();
                }

                //_currentHoverable?.OnHoverExit();
                _currentHoverable = hoverTarget;
                //_currentHoverable?.OnHoverEnter();

                if (_currentHoverable != null)
                {
                    _currentHoverable.OnHoverExit();
                }
            }
        }
        else
        {
            if (_currentHoverable != null)
            {
                _currentHoverable.OnHoverExit();
            }

            //_currentHoverable?.OnHoverExit();
            _currentHoverable = null;
        }
    }
    public void Hoverable()
    {

    }

    public void OnHoverEnter()
    {
        //Change mesh color
        //Change Emission Level
    }

    public void OnHoverExit()
    {
        //Return Mesh Color
        //Return Emission Level
    }
}
