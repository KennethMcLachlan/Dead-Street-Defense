using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamageable
{

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
                //Send Back to pool
                //Damage Player

                //Temporary for Testing | Used to prevent an error
                var spawnPoint = SpawnManager.Instance._spawnPoint;
                gameObject.transform.position = spawnPoint.transform.position;
            }

            _agent.SetDestination(_wayPoints[_currentPoint].position);
        }
    }

    void Damage()
    {

    }
}
