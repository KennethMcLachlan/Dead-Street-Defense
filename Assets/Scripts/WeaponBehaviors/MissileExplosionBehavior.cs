using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class MissileExplosionBehavior : MonoBehaviour
{
    [SerializeField] private int _explosionDamage = 5;
    [SerializeField] private float _lifetime = 2f;
    private ObjectPool<GameObject> _pool;
       

   public void SetPool(ObjectPool<GameObject> pool)
    {
        _pool = pool;
    }

    private void OnEnable()
    {
        StartCoroutine(ReturnToPoolRoutine());
    }

    private IEnumerator ReturnToPoolRoutine()
    {
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
