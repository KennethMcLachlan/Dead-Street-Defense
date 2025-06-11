using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class MouseInteractions : MonoBehaviour
{
    private static MouseInteractions _instance;
    public static MouseInteractions Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("MouseInteractions is NULL");
            }
            return _instance;
        }
    }

    private Camera _mainCamera;

    public Vector3 mouseWorldPosition;
    public bool isMouseOverWorld;

    private void Awake()
    {
        _instance = this;
    }
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

        //PlayerInputControls.Instance._lookAction.action.ReadValue<Vector2>();

        //if (PlayerInputControls.Instance._actionButton.action.WasPressedThisFrame())
        //{
        //    if (EventSystem.current.IsPointerOverGameObject())
        //        return;

        //    Ray ray = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        //    if (Physics.Raycast(ray, out RaycastHit hit))
        //    {
        //        IClickable clickable = hit.collider.GetComponent<IClickable>();
        //        if (clickable != null)
        //        {
        //            clickable.OnClick();
        //        }
        //    }
        //}

        PlayerInputControls.Instance._lookAction.action.ReadValue<Vector2>();

        if (EventSystem.current.IsPointerOverGameObject())
        {
            isMouseOverWorld = false;
            return;
        }

        Ray ray = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            mouseWorldPosition = hit.point;
            isMouseOverWorld = true;


            if (PlayerInputControls.Instance._actionButton.action.WasPressedThisFrame())
            {
                IClickable clickable = hit.collider.GetComponent<IClickable>();
                if (clickable != null)
                {
                    clickable.OnClick();
                }
            }
        }
        else
        {
            isMouseOverWorld = false;
        }
    }
}
