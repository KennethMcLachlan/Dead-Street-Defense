using UnityEngine;
using UnityEngine.Pool;

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
    [SerializeField] private GameObject _enemyMonsterPrefab;
    [SerializeField] public Transform _spawnPoint;
    [SerializeField] private float _chanceRateToSpawn = 0.05f;

    [Header("Pooling")]
    private ObjectPool<GameObject> _enemyPool;
    private ObjectPool<GameObject> _enemyMonsterPool;
    [SerializeField] private Transform _poolContainer;
    [SerializeField] private GameObject _explosionPrefab;
    private ObjectPool<GameObject> _explosionPool;
    #endregion

    private void Awake()
    {
        _instance = this;

        EnableEnemyPools();
        EnableEnemyMonsterPool();
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
    public void SpawnEnemy(bool allowMonster = false)
    {
        if (allowMonster && Random.value < _chanceRateToSpawn)
        {
            _enemyMonsterPool.Get();
        }
        else
        {
            _enemyPool.Get();
        }
    }

    private GameObject CreateEnemy(GameObject prefab)
    {
        GameObject enemy = Instantiate(prefab, _spawnPoint.position, _spawnPoint.rotation, _poolContainer);
        enemy.GetComponent<EnemyBase>().SetPool(GetPoolForEnemy(enemy));
        return enemy;
    }

    private void OnGetEnemy(GameObject gameObject)
    {
        gameObject.GetComponent<EnemyBase>().ActivateEnemy();
    }

    private void OnDestroyEnemy(GameObject gameObject)
    {
        //May not need to Destroy Enemy
    }

    private ObjectPool<GameObject> GetPoolForEnemy(GameObject enemy)
    {
        if (enemy.GetComponent<EnemyMonster>() != null)
        {
            return _enemyMonsterPool;
        }
        else { return _enemyPool; }
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

    private void EnableEnemyMonsterPool()
    {
        _enemyMonsterPool = new ObjectPool<GameObject>(
            createFunc: () => CreateEnemy(_enemyMonsterPrefab),
            actionOnGet: OnGetEnemy,
            actionOnRelease: OnReleaseEnemy,
            actionOnDestroy: OnDestroyEnemy,
            collectionCheck: true,
            defaultCapacity: 10,
            maxSize: 50
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