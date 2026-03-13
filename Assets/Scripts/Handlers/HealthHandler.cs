using System.Runtime.CompilerServices;
using UnityEngine;

public class HealthHandler : MonoBehaviour, IDamageable
{
    [SerializeField] private int _maxHealth = 12;
    [SerializeField] private int _minHealth = 3;
    [SerializeField] private int _currentHealth;
    [SerializeField] private int _enemyDeathReward = 100;
    [SerializeField] private bool _canRandomizeHealth;
    public int health
    {
        get => _currentHealth;
        set => _currentHealth = value;
    }

    private void Start()
    {
        if (_canRandomizeHealth)
        {
            _currentHealth = Random.Range(_minHealth, _maxHealth);
        }
        else { _currentHealth = _maxHealth; }
    }
    public void Damage(int amount)
    {
        health -= amount;

        if (TryGetComponent(out Enemy enemy))
        {
            if (health <= 0)
            {
                //health = _maxHealth;
                WarfundsHandler.Instance.ReceiveWarfunds(_enemyDeathReward);
                enemy.Die();
                //enemy.ResetEnemy();
            }
        }
        else
        {
            UIManager.Instance.UpdateLives(health);
            if (health <= 0)
            {
                PlaybackHandler.Instance.GameOver();
            }
            else if (health <= 5)
            {
                UIManager.Instance.HealthStatusCritical();
            }
            else if (health < 10)
            {
                UIManager.Instance.HealthStatusWarning();
            }
            else
            {
                UIManager.Instance.HealthStatusGood();
            }
        }
    }

    public void ResetHealth()
    {
        _currentHealth = _maxHealth;
    }
}
