using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputControls : MonoBehaviour
{
    private static PlayerInputControls _instance;
    public static PlayerInputControls Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("Input Controls is NULL");
            }
            return _instance;
        }
    }

    public InputActionReference _middleClickAction;
    public InputActionReference _lookAction; //Located on the Camera Action Map || Mouse Movement Tracking
    public InputActionReference _actionButton;
    public InputActionReference _cancelButton;

    private Draggable _currentDraggable;

    private void Awake()
    {
        _instance = this;
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

        //Action Button || Left Click
        _actionButton.action.started += Action_started;
        _actionButton.action.canceled += Action_canceled;
        _actionButton.action.Enable();

        //Cancel Button  ||  Right Click
        _cancelButton.action.started += Cancel_Started;
        _cancelButton.action.canceled += Cancel_Canceled;
        _cancelButton.action.Enable();
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

        //Action Button || Left Click
        _actionButton.action.started -= Action_started;
        _actionButton.action.canceled -= Action_canceled;
        _actionButton.action.Disable();

        //Cancel Button  ||  Right Click
        _cancelButton.action.started -= Cancel_Started;
        _cancelButton.action.canceled -= Cancel_Canceled;
        _cancelButton.action.Disable();
    }

    private void MiddleClickStarted(InputAction.CallbackContext context)
    {
    }

    private void MiddleClickCanceled(InputAction.CallbackContext context)
    {
    }

    private void LookActionStarted(InputAction.CallbackContext context)
    {
        
    }

    private void LookActionCanceled(InputAction.CallbackContext context)
    {
        
    }

    private void Cancel_Canceled(InputAction.CallbackContext obj)
    {
    }

    private void Cancel_Started(InputAction.CallbackContext obj)
    {
    }
    private void Action_started(InputAction.CallbackContext obj)
    {
        Debug.Log("Action_started was called on");
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Draggable draggable = hit.collider.GetComponent<Draggable>();
            if (draggable != null)
            {
                Debug.Log("Raycast hit: " + hit.collider.name);
                _currentDraggable = draggable;
                _currentDraggable.BeginDrag();
            }

            IClickable clickable = hit.collider.GetComponent<IClickable>();
            if (clickable != null)
            {
                Debug.Log("IClickable was called on " + hit.collider.name);
                clickable.OnClick();
            }
        }
    }

    private void Action_canceled(InputAction.CallbackContext obj)
    {
    }

}
