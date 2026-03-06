using UnityEngine;

public class HealthHandler : MonoBehaviour, IDamageable
{
    [SerializeField] private int _maxHealth = 10;
    [SerializeField] private int _currentHealth;
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
        if (health <= 0)
        {
            Enemy enemy = GetComponent<Enemy>();
            if (enemy != null)
            {
                health = _maxHealth;
                enemy.ResetEnemy();
            }
            else
            {
                //Get Player Component and kill em
            }
        }
    }

}
