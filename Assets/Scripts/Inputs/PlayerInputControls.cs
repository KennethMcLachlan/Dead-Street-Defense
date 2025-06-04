using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputControls : MonoBehaviour
{
    public static InputManager instance;
    public static InputManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("Instance is NULL");
            }
            return instance;
        }
    }

    [SerializeField] private InputActionReference _middleClickAction;
    [SerializeField] private InputActionReference _lookAction;

    [SerializeField] private float _dragSpeed = 0.5f;

    private bool _isDragging;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            return;
        }
        //instance = this;

    }
    private void OnEnable()
    {
        EnableInputActions();
    }

    private void OnDisable()
    {
        DisableInputActions();
    }

    private void Update()
    {
        if (!_isDragging)
            return;

        Vector2 mouseDelta = _lookAction.action.ReadValue<Vector2>();
        Vector3 move = new Vector3(-mouseDelta.x, -mouseDelta.y, 0) * _dragSpeed;
        transform.Translate(move, Space.World);
    }

    private void MiddleClickStarted(InputAction.CallbackContext context)
    {
        _isDragging = true;
    }

    private void MiddleClickCanceled(InputAction.CallbackContext context)
    {
        _isDragging = false;
    }

    private void LookActionStarted(InputAction.CallbackContext context)
    {
        
    }

    private void LookActionCanceled(InputAction.CallbackContext context)
    {
        
    }

    private void EnableInputActions()
    {
        //Middle Mouse Button
        _middleClickAction.action.started += MiddleClickStarted;
        _middleClickAction.action.canceled += MiddleClickCanceled;
        _middleClickAction.action.Enable();

        //Mouse Delta / Mouse Movement / Vector 2
        _lookAction.action.started += LookActionStarted;
        _lookAction.action.canceled += LookActionCanceled;
        _lookAction.action.Enable();

    }

    private void DisableInputActions()
    {
        //Middle Mouse Button
        _middleClickAction.action.started -= MiddleClickStarted;
        _middleClickAction.action.canceled -= MiddleClickCanceled;
        _middleClickAction.action.Disable();

        //Mouse Delta / Mouse Movement / Vector 2
        _lookAction.action.started -= LookActionStarted;
        _lookAction.action.canceled -= LookActionCanceled;
        _lookAction.action.Disable();
    }
}
