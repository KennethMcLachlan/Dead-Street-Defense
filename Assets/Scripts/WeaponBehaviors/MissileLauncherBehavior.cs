using Unity.VisualScripting;
using UnityEngine;

public class MissileLauncherBehavior : WeaponBehavior
{
    [SerializeField] private float _rotationSpeed = 90f;
    private GameObject _muzzleFlash;

    protected override void Start()
    {
        base.Start();
    }

}
