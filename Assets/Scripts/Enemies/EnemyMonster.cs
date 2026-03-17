using UnityEngine;

public class EnemyMonster : EnemyBase
{
    [Header("Navigation")]
    [SerializeField] private float _speed = 2f;

    protected override void Start()
    {
        base.Start();
        _agent.speed = _speed;
    }

    public override void ResetEnemy()
    {
        _agent.speed = _speed;
        base.ResetEnemy();
    }

    public override void ActivateEnemy()
    {
        base.ActivateEnemy();
        _agent.speed = _speed;
    }
}
