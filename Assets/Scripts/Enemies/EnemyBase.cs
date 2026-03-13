using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine.Pool;

public abstract class EnemyBase : MonoBehaviour
{
    protected ObjectPool<GameObject> _enemyPool;
    protected List<Transform> _waypoints;
    protected int _currentPoint = 0;
    protected NavMeshAgent _agent;
    protected Animator _animator;
    protected SkinnedMeshRenderer _skinnedMeshRenderer;
    protected bool _isDying;

    [Header("Death")]
    [SerializeField] protected Material _dissolveMaterial;
    [SerializeField] protected float _dissolveDuration = 2f;
    [SerializeField] protected float _dissolveDelay = 1f;
    protected Material _originalMaterial;

    protected virtual void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _waypoints = WayPointManager.Instance.SendWayPoints();
        _animator = GetComponentInChildren<Animator>();
        _skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        _originalMaterial = _skinnedMeshRenderer.material;
    }

    protected virtual void Start()
    {
        if (_waypoints == null || _waypoints.Count == 0)
        {
            Debug.LogError("Waypoints are not set");
            return;
        }

        _agent.destination = _waypoints[_currentPoint].position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WayPoint"))
        {
            _currentPoint++;
            if (_currentPoint >= _waypoints.Count)
            {
                _currentPoint = 0;
                ResetEnemy();
            }

            _agent.destination = _waypoints[_currentPoint].position;
        }
    }

    public void Die()
    {
        if (_isDying) return;

        _isDying = true;
        _agent.speed = 0f;
        _animator.SetTrigger("Death");
        StartCoroutine(DissolveRoutine());
    }

    private IEnumerator DissolveRoutine()
    {
        yield return new WaitForSeconds(_dissolveDelay);

        _skinnedMeshRenderer.material = _dissolveMaterial;

        float elapsed = 0f;
        while (elapsed < _dissolveDuration)
        {
            elapsed += Time.deltaTime;
            float dissolveValue = Mathf.Clamp01(elapsed / _dissolveDuration);
            _skinnedMeshRenderer.material.SetFloat("_Dissolve", dissolveValue);
            yield return null;
        }

        ResetEnemy();
    }

    public virtual void ResetEnemy()
    {
        _isDying = false;
        _skinnedMeshRenderer.material = _originalMaterial;
        _currentPoint = 0;
        GetComponent<HealthHandler>().ResetHealth();
        _enemyPool.Release(gameObject);
        WaveManager.Instance.OnEnemyReturnedToPool();
    }

    public virtual void ActivateEnemy()
    {
        _currentPoint = 0;
        gameObject.SetActive(false);
        transform.position = SpawnManager.Instance._spawnPoint.position;
        gameObject.SetActive(true);
        _agent.SetDestination(_waypoints[_currentPoint].position);
    }

    public void SetPool(ObjectPool<GameObject> pool)
    {
        _enemyPool = pool;
    }
}
