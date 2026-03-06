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

    

    void Start()
    {
        _wayPoints = WayPointManager.Instance.SendWayPoints();
        if (_wayPoints == null || _wayPoints.Count == 0)
        {
            Debug.LogError("Waypoints are not set or empty");
            return;
        }

        _agent = GetComponent<NavMeshAgent>();
        if (_agent != null)
        {
            _agent.destination = _wayPoints[_currentPoint].position;
        }
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WayPoint"))
        {
            Debug.Log("Enemy has triggered a Waypoint");

            _currentPoint++;
            if (_currentPoint >= _wayPoints.Count)
            {
                _currentPoint = 0;
                ResetEnemy();
                //Damage Player
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
        _enemyPool.Release(gameObject);
        WaveManager.Instance.OnEnemyReturnedToPool();
    }

    public void ActivateEnemy()
    {
        //is active true;
    }
    
}
