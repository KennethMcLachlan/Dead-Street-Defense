using UnityEngine;

public class MissileLauncherBehavior : WeaponBehavior
{
    [SerializeField] private float _rotationSpeed = 90f;
    [SerializeField] private float _damageRate = 3f;
    [SerializeField] private ParticleSystem _muzzleFlash;

    protected override void Start()
    {
        base.Start();
        if (_muzzleFlash == null)
        {
            Debug.LogError("MissileLauncher MuzzleFlash is NULL!");
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void OnFire()
    {
        RotateTowardsTarget(transform.GetChild(0).GetChild(0), _rotationSpeed);
    }

    protected override float GetFireRate()
    {
        return _damageRate;
    }

    protected override void OnTargetLost()
    {
    }

    protected override void OnDamageDealt(Collider target)
    {
        _muzzleFlash.Play();
        SpawnManager.Instance.SpawnExplosion(target.transform.position);
    }

    public override void UpdateUICostValues()
    {
        base.UpdateUICostValues();
        UIManager.Instance.UpdateMissileLauncherUpgradeCost(_upgradeCost);
    }

    public void DestroyMissileLauncher()
    {
        DestroyWeapon();
    }
}
