using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Pool;
using Unity.VisualScripting;

public class SpawnManager : MonoBehaviour
{
    #region [Variables]
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
    [SerializeField] private GameObject _missileLauncherPrefab;

    [Header("Enemies")]
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] public Transform _spawnPoint;
    private bool _enemiesAreActive;

    [Header("Pooling")]
    private ObjectPool<GameObject> _enemyPool;
    [SerializeField] private Transform _poolContainer;
    [SerializeField] private GameObject _explosionPrefab;
    private ObjectPool<GameObject> _explosionPool;
    #endregion

    private void Awake()
    {
        _instance = this;

        EnableEnemyPools();
        EnableExplosionPool();
    }

    #region [Spawn Weapons]
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
        Camera camera = Camera.main;
        Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 spawnPosition = hit.point;
            Instantiate(_missileLauncherPrefab, spawnPosition, Quaternion.identity);
        }
    }
    #endregion

    #region[Enemy Spawn & Pooling]
    public void SpawnEnemy()
    {
        _enemyPool.Get();
    }

    private GameObject CreateEnemy(GameObject prefab)
    {
        GameObject enemy = Instantiate(prefab, _spawnPoint.position, _spawnPoint.rotation, _poolContainer);
        enemy.GetComponent<Enemy>().SetPool(GetPoolForEnemy(enemy));
        return enemy;
    }

    private void OnGetEnemy(GameObject gameObject)
    {
        Enemy enemy = gameObject.GetComponent<Enemy>();
        enemy.ActivateEnemy();

        gameObject.SetActive(true);
    }

    private void OnDestroyEnemy(GameObject gameObject)
    {
        //May not need to Destroy Enemy
    }

    private ObjectPool<GameObject> GetPoolForEnemy(GameObject enemy)
    {
        return _enemyPool;
    }

    private void OnReleaseEnemy(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }

    private void EnableEnemyPools()
    {
        _enemyPool = new ObjectPool<GameObject>(
            createFunc: () => CreateEnemy(_enemyPrefab),
            actionOnGet: OnGetEnemy,
            actionOnRelease: OnReleaseEnemy,
            actionOnDestroy: OnDestroyEnemy,
            collectionCheck: true,
            defaultCapacity: 25,
            maxSize: 100
            );
    }
    #endregion

    #region [Missile Explosion Pooling]
    private void EnableExplosionPool()
    {
        _explosionPool = new ObjectPool<GameObject>(
            createFunc: () =>
            {
                GameObject obj = Instantiate(_explosionPrefab);
                obj.GetComponent<MissileExplosionBehavior>().SetPool(_explosionPool);
                return obj;
            },
            actionOnGet: obj => obj.SetActive(true),
            actionOnRelease: obj => obj.SetActive(false),
            actionOnDestroy: obj => Destroy(obj),
            defaultCapacity: 10,
            maxSize: 20
            );
    }

    public GameObject SpawnExplosion(Vector3 position)
    {
        GameObject explosion = _explosionPool.Get();
        explosion.transform.position = position;
        return explosion;
    }
    #endregion
}