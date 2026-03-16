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
        if (EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log("Pointer over UI - Blocking Raycast!");
            return;
        }

        PlayerInputControls.Instance._lookAction.action.ReadValue<Vector2>();

        if (!EventSystem.current.IsPointerOverGameObject())
        {
            Ray ray = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                mouseWorldPosition = hit.point;
                isMouseOverWorld = true;

                if (PlayerInputControls.Instance._actionButton.action.WasPressedThisFrame())
                {
                    if (UIManager.Instance.IsUpgradePopUpOpen())
                    {
                        return;
                    }

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
}
