using Unity.VisualScripting;
using UnityEngine;

public class MissileLauncherBehavior : WeaponBehavior
{
    [SerializeField] private float _rotationSpeed = 90f;
    [SerializeField] private float _damageRate = 3f;
    private GameObject _muzzleFlash;
    private ParticleSystem _muzzleParticle;
    [SerializeField] private GameObject _explosionPrefab;

    protected override void Start()
    {
        base.Start();
        Transform muzzleChild = transform.GetChild(1);
        _muzzleFlash = muzzleChild.GetChild(0).gameObject;
        _muzzleParticle = _muzzleFlash.GetComponent<ParticleSystem>();
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
        ParticleSystem muzzleFlash = _muzzleFlash.GetComponent<ParticleSystem>();
        muzzleFlash.Stop();
    }

    protected override void OnDamageDealt(Collider target)
    {
        if (_muzzleParticle != null)
        {
            _muzzleParticle.Play();
        }

        //AOE Explosion
        if (_explosionPrefab != null)
        {
            Instantiate(_explosionPrefab, target.transform.position, Quaternion.identity);
        }
    }

    public override void UpdateUICostValues()
    {
        base.UpdateUICostValues();
        UIManager.Instance.UpdateGatlingUpgradeCost(_upgradeCost);
    }

    public void DestroyMissileLauncher()
    {
        DestroyWeapon();
    }
}
