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
}
