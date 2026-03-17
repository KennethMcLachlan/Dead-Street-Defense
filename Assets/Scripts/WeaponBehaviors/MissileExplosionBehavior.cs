using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class MissileExplosionBehavior : MonoBehaviour
{
    [SerializeField] private int _explosionDamage = 5;
    [SerializeField] private float _lifetime = 2f;
    [SerializeField] private SphereCollider _collider;
    [SerializeField] private float _collisionTime = 0.5f;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private AudioSource _explosionSFX;
    private ObjectPool<GameObject> _pool;
       

   public void SetPool(ObjectPool<GameObject> pool)
    {
        _pool = pool;
    }

    private void OnEnable()
    {
        _collider.enabled = true;

        if (_particleSystem != null)
        {
            _particleSystem.Stop();
            _particleSystem.Play();

            if (_explosionSFX != null)
            {
                _explosionSFX.Play();
            }
        }
    
        StartCoroutine(ReturnToPoolRoutine());
    }

    private IEnumerator ReturnToPoolRoutine()
    {
        yield return new WaitForSeconds(_collisionTime);
        _collider.enabled = false;
        yield return new WaitForSeconds(_lifetime);
        _pool.Release(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            HealthHandler health = other.GetComponent<HealthHandler>();
            if (health != null)
            {
                health.Damage(_explosionDamage);
            }
        }
    }
}
