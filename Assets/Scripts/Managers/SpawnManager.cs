using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour
{
    private static SpawnManager _instance;
    public static SpawnManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("SpawnManager is NULL!");
            }
            return _instance;
        }
    }

    [Header("Camera")]
    [SerializeField] private Camera _camera;
    private Ray _ray;

    [Header("Weapons")]
    [SerializeField] private GameObject _gatlingPrefab;
    [SerializeField] private GameObject _missleTurretPrefab;
    [SerializeField] private GameObject _laserCannonPrefab;

    [Header("Enemies)")]
    [SerializeField] private GameObject _enemy01Prefab;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private GameObject _enemyContainer;
    [SerializeField] private List<GameObject> _enemyPool;
    private bool _enemiesAreActive;

    [Header("Navigation")]
    [SerializeField] private List<Transform> _wayPoints;




    private void Awake()
    {
        _instance = this;
    }
    private void Update()
    {
        //For Testing Purposes
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(_enemy01Prefab, _spawnPoint);
        }
    }
    public void SpawnGatling()
    {
        Camera camera = Camera.main;
        Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 spawnPosition = hit.point;
            Instantiate(_gatlingPrefab, spawnPosition, Quaternion.identity);
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
        // Not Ready Yet
    }

    public void TestClick()
    {
        Debug.Log("Button has been clicked");
    }
}