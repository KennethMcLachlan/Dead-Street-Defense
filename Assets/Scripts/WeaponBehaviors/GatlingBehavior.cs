using UnityEngine;
using UnityEngine.Rendering;

public class GatlingBehavior : WeaponBehavior
{
    [SerializeField] private float _rotationSpeed = 90f;
    [SerializeField] private float _damageRate = 1f;
    private GameObject _muzzleFlash;
    [SerializeField] private AudioSource _gunFireSFX;


    protected override void Start()
    {
        base.Start();
        Transform firstChild = transform.GetChild(0);
        _muzzleFlash = firstChild.GetChild(1).gameObject;
        if (_muzzleFlash != null)
        {
            _muzzleFlash.SetActive(false);
            _gunFireSFX.Stop();
        }
        else
        {
            Debug.LogError("MuzzleFlash is NULL!");
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        if (_muzzleFlash != null)
        {
            _muzzleFlash.SetActive(false);
            _gunFireSFX.Stop();
        }
    }

    protected override void Update()
    {
        base.Update();
        if (_enemiesInRange <= 0 && _muzzleFlash.activeSelf)
        {
            _muzzleFlash.SetActive(false);
            _gunFireSFX.Stop();
        }
    }

    protected override void OnFire()
    {
        if (!_gunFireSFX.isPlaying)
        {
            _gunFireSFX.Play();
        }
        _muzzleFlash.SetActive(true);

        RotateTowardsTarget(transform.GetChild(0), _rotationSpeed);
    }

    protected override void OnTargetLost()
    {
        _muzzleFlash.SetActive(false);
        _gunFireSFX.Stop();
    }

    protected override float GetFireRate()
    {
        return _damageRate;
    }

    protected override void OnDamageDealt(Collider target)
    {

    }

    public override void UpdateUICostValues()
    {
        base.UpdateUICostValues();
        UIManager.Instance.UpdateGatlingUpgradeCost(_upgradeCost);
    }

    public void DestroyGatling()
    {
        DestroyWeapon();
    }
}
