using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;

public class Enemy : MonoBehaviour
{
    private ObjectPool<GameObject> _enemyPool;

    [Header("Navigation")]
    private List<Transform> _wayPoints;
    private int _currentPoint = 0;
    private NavMeshAgent _agent;
    [SerializeField] private float _minSpeed = 1f;
    [SerializeField] private float _maxSpeed = 8f;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _wayPoints = WayPointManager.Instance.SendWayPoints();
    }

    void Start()
    {
        if (_wayPoints == null || _wayPoints.Count == 0)
        {
            Debug.LogError("Waypoints are not set or empty");
            return;
        }

        if (_agent != null)
        {
            _agent.destination = _wayPoints[_currentPoint].position;
            _agent.speed = Random.Range(_minSpeed, _maxSpeed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WayPoint"))
        {
            _currentPoint++;
            if (_currentPoint >= _wayPoints.Count)
            {
                _currentPoint = 0;
                ResetEnemy();
                return;
            }
            _agent.SetDestination(_wayPoints[_currentPoint].position);
        }
    }

    //Object Pooling Methods
    public void SetPool(ObjectPool<GameObject> pool)
    {
        _enemyPool = pool;
    }

    public void ResetEnemy()
    {
        _currentPoint = 0;
        GetComponent<HealthHandler>().ResetHealth();
        _enemyPool.Release(gameObject);
        WaveManager.Instance.OnEnemyReturnedToPool();
    }

    public void ActivateEnemy()
    {
        _currentPoint = 0;
        gameObject.SetActive(false);
        transform.position = SpawnManager.Instance._spawnPoint.position;
        gameObject.SetActive(true);
        _agent.SetDestination(_wayPoints[_currentPoint].position);
    }
    
}
