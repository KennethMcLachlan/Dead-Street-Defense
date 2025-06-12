using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _gatlingPrefab;
    [SerializeField] private GameObject _missleTurretPrefab;
    [SerializeField] private GameObject _laserCannonPrefab;

    [SerializeField] private Camera _camera;
    private Ray _ray;

    public void SpawnGatling()
    {
        Camera camera = Camera.main;
        Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 spawnPosition = hit.point;
            Instantiate(_gatlingPrefab, spawnPosition, Quaternion.identity);
            Debug.Log("Gatling Gun has Spawned");
        }

        //Ray ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        //if (MouseInteractions.Instance.isMouseOverWorld)
        //{
        //    Debug.Log("MouseInteraction Instance has been called and isMouseOverWorld == true");
        //    Vector3 spawnPosition = MouseInteractions.Instance.mouseWorldPosition;
        //    //Vector3 spawnPosition = PlayerInputControls.Instance._lookAction.action.ReadValue<Vector2>(); ;

        //    Instantiate(_gatlingPrefab, spawnPosition, Quaternion.identity);
        //    Debug.Log("Gatling GUn has Spawned");
        //}
    }

    public void SpawnMissileTurret()
    {
        if (MouseInteractions.Instance.isMouseOverWorld)
        {
            Vector3 spawnPosition = MouseInteractions.Instance.mouseWorldPosition;
            Instantiate(_missleTurretPrefab, spawnPosition, Quaternion.identity);
        }
    }
    
    public void SpawnLaserCannon()
    {
        if (MouseInteractions.Instance.isMouseOverWorld)
        {
            Vector3 spawnPosition = MouseInteractions.Instance.mouseWorldPosition;
            Instantiate(_laserCannonPrefab, spawnPosition, Quaternion.identity);
        }
    }

    public void SpawnEnemy()
    {
        //Not Ready Yet
    }

    public void TestClick()
    {
        Debug.Log("Button has been clicked");
    }
}
