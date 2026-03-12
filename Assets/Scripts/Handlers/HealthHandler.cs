using UnityEngine;

public class HealthHandler : MonoBehaviour, IDamageable
{
    [SerializeField] private int _maxHealth = 10;
    [SerializeField] private int _currentHealth;
    [SerializeField] private int _enemyDeathReward = 100;
    public int health
    {
        get => _currentHealth;
        set => _currentHealth = value;
    }

    private void Start()
    {
        _currentHealth = _maxHealth;
    }
    public void Damage(int amount)
    {
        health -= amount;

        if (TryGetComponent(out Enemy enemy))
        {
            if (health <= 0)
            {
                health = _maxHealth;
                WarfundsHandler.Instance.ReceiveWarfunds(_enemyDeathReward);
                enemy.ResetEnemy();
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
