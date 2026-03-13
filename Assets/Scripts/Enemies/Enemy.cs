using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;
using System.Collections;

public class Enemy : EnemyBase
{

    [Header("Navigation")]
    [SerializeField] private float _minSpeed = 2.5f;
    [SerializeField] private float _maxSpeed = 5.5f;
    private float _currentSpeed;

    protected override void Start()
    {
        {
            base.Start();
            _currentSpeed = Random.Range(_minSpeed, _maxSpeed);
            _agent.speed = _currentSpeed;
        }
    }

    public override void ResetEnemy()
    {
        _currentSpeed = Random.Range(_minSpeed, _maxSpeed);
        _agent.speed = _currentSpeed;
        base.ResetEnemy();
    }

    public override void ActivateEnemy()
    {
        base.ActivateEnemy();
        _currentSpeed = Random.Range(_minSpeed, _maxSpeed);
        _agent.speed = _currentSpeed;
    }

    //private ObjectPool<GameObject> _enemyPool;

    //[Header("Navigation")]
    //private List<Transform> _wayPoints;
    //private int _currentPoint = 0;
    //private NavMeshAgent _agent;
    //[SerializeField] private float _minSpeed = 1f;
    //[SerializeField] private float _maxSpeed = 8f;
    //private float _currentSpeed;

    //[Header("Death")]
    //[SerializeField] private Material _dissolveMaterial;
    //[SerializeField] private float _dissolveDuration = 2f;
    //[SerializeField] private float _dissolveDelay = 0.5f;
    //private SkinnedMeshRenderer _skinnedMeshRenderer;
    //private Animator _animator;
    //private Material _originalMaterial;
    //private bool _isDying;

    //private void Awake()
    //{
    //    _agent = GetComponent<NavMeshAgent>();
    //    _wayPoints = WayPointManager.Instance.SendWayPoints();
    //    _animator = GetComponentInChildren<Animator>();
    //    _skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
    //    _originalMaterial = _skinnedMeshRenderer.material;
    //}

    //void Start()
    //{
    //    if (_wayPoints == null || _wayPoints.Count == 0)
    //    {
    //        Debug.LogError("Waypoints are not set or empty");
    //        return;
    //    }

    //    if (_agent != null)
    //    {
    //        _agent.destination = _wayPoints[_currentPoint].position;
    //        _currentSpeed = Random.Range(_minSpeed, _maxSpeed);
    //        _agent.speed = _currentSpeed;
    //    }
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("WayPoint"))
    //    {
    //        _currentPoint++;
    //        if (_currentPoint >= _wayPoints.Count)
    //        {
    //            _currentPoint = 0;
    //            ResetEnemy();
    //            return;
    //        }
    //        _agent.SetDestination(_wayPoints[_currentPoint].position);
    //    }
    //}

    //public void Die()
    //{
    //    if (_isDying) return;

    //    _isDying = true;
    //    _agent.speed = 0f;
    //    _animator.SetTrigger("Death");
    //    StartCoroutine(DissolveRoutine());
    //}

    //private IEnumerator DissolveRoutine()
    //{
    //    yield return new WaitForSeconds(_dissolveDelay);

    //    _skinnedMeshRenderer.material = _dissolveMaterial;

    //    float elapsed = 0f;

    //    while (elapsed < _dissolveDuration)
    //    {
    //        elapsed += Time.deltaTime;
    //        float dissolveValue = Mathf.Clamp01(elapsed / _dissolveDuration);
    //        _skinnedMeshRenderer.material.SetFloat("_Dissolve", dissolveValue);
    //        yield return null;
    //    }

    //    ResetEnemy();
    //}

    ////Object Pooling Methods
    //public void SetPool(ObjectPool<GameObject> pool)
    //{
    //    _enemyPool = pool;
    //}

    //public void ResetEnemy()
    //{
    //    _isDying = false;
    //    _skinnedMeshRenderer.material = _originalMaterial;
    //    _currentPoint = 0;
    //    _currentSpeed = Random.Range(_minSpeed, _maxSpeed);
    //    _agent.speed = _currentSpeed;
    //    GetComponent<HealthHandler>().ResetHealth();
    //    _enemyPool.Release(gameObject);
    //    WaveManager.Instance.OnEnemyReturnedToPool();
    //}

    //public void ActivateEnemy()
    //{
    //    _currentPoint = 0;
    //    gameObject.SetActive(false);
    //    transform.position = SpawnManager.Instance._spawnPoint.position;
    //    gameObject.SetActive(true);
    //    _currentSpeed = Random.Range(_minSpeed, _maxSpeed);
    //    _agent.speed = _currentSpeed;
    //    _agent.SetDestination(_wayPoints[_currentPoint].position);
    //}

}
