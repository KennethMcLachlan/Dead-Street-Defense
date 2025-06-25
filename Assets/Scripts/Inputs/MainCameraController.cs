using UnityEngine;

public class MainCameraController : MonoBehaviour
{
    [Header("Camera Boundaries")]
    [SerializeField] private float _movementSpeed = 1.0f;
    private float _fixedY;

    [SerializeField] private Vector2 _xBounds = new Vector2(-10f, 10f);
    [SerializeField] private Vector2 _zBounds = new Vector2(-10f, 10f);

    private bool _isDragging;

    [Header("Camera Zoom")]
    [SerializeField] private float _zoomSpeed = 3f;
    [SerializeField] private float _minFov = 15f;
    [SerializeField] private float _maxFov = 90f;



    void Start()
    {
        _fixedY = transform.position.y;
    }

    void Update()
    {
        //Click & Drag
        var input = PlayerInputControls.Instance;

        if (input._middleClickAction.action.WasPressedThisFrame())
            _isDragging = true;

        if (input._middleClickAction.action.WasReleasedThisFrame())
            _isDragging = false;

        if (_isDragging)
        {
            var lookAction = PlayerInputControls.Instance._lookAction.action;
            Vector2 mouseDelta = lookAction.ReadValue<Vector2>();

            Vector3 right = Vector3.ProjectOnPlane(transform.right, Vector3.up).normalized;
            Vector3 forward = Vector3.ProjectOnPlane(transform.forward, Vector3.up).normalized;
            Vector3 move = (-right * mouseDelta.x + -forward * mouseDelta.y) * _movementSpeed;

            transform.position += move;
            transform.position = new Vector3(transform.position.x, _fixedY, transform.position.z);

            //Clamps the camera to stay within a boundary
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, _xBounds.x, _xBounds.y), _fixedY, Mathf.Clamp(transform.position.z, _zBounds.x, _zBounds.y));
        }

        //Camera Zoom
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView - scroll * _zoomSpeed, _minFov, _maxFov);
            Debug.Log("Current FOV: " + Camera.main.fieldOfView);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Vector3 center = new Vector3((_xBounds.x + _xBounds.y) / 2f, _fixedY, (_zBounds.x + _zBounds.y) / 2f);
        Vector3 size = new Vector3(_xBounds.y - _xBounds.x, 0.1f, _zBounds.y - _zBounds.x);
        Gizmos.DrawWireCube(center, size);
    
    }
}
