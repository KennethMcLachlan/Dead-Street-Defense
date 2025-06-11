using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _gatlingPrefab;
    [SerializeField] private GameObject _missleTurretPrefab;
    [SerializeField] private GameObject _laserCannonPrefab;

    public void SpawnGatling()
    {
        if (MouseInteractions.Instance.isMouseOverWorld)
        {
            Vector3 spawnPosition = MouseInteractions.Instance.mouseWorldPosition;
            //Vector3 spawnPosition = PlayerInputControls.Instance._lookAction.action.ReadValue<Vector2>(); ;

            Instantiate(_gatlingPrefab, spawnPosition, Quaternion.identity);
            Debug.Log("Gatling GUn has Spawned");
        }
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
}
