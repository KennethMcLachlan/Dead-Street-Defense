using UnityEngine;

public abstract class WeaponBehavior : MonoBehaviour
{
    [Header("Ammunition")]
    [SerializeField] protected int _currentAmmo;
    [SerializeField] protected int _maxAmmo;
    [SerializeField] protected int _ammoDepletionCount = 10;
    [SerializeField] protected float _lowAmmoIndicator = 0.25f;
    [SerializeField] protected ParticleSystem _smokePFX;
    private bool _isSmokePlaying;

    [Header("Warfund Costs")]
    [SerializeField] protected int _selfDestructCost = 200;
    [SerializeField] protected int _upgradeCost = 500;
    [SerializeField] protected int _dismantleValue;
    public int GetDismantleValue() => _dismantleValue;

    [Header("Damage")]
    [SerializeField] protected int _damagePerSecond = 1;
    protected float _damageTimer = 0f;
    protected Collider _currentTarget;
    protected int _enemiesInRange = 0;

    public bool isActive;

    protected virtual void Start()
    {
        _currentAmmo = _maxAmmo;
    }

    protected virtual void OnEnable()
    {
        _enemiesInRange = 0;
        _currentTarget = null;
        _isSmokePlaying = false;
        if (_smokePFX != null)
        {
            _smokePFX.Stop();
        }
    }

    protected virtual void Update()
    {
        AmmoTracking();

        if (isActive)
        {
            TrackEnemiesInRange();
        }
    }

    private void AmmoTracking()
    {
        if (_currentAmmo <= 0)
        {
            isActive = false;
            SendSelfDestructCost(_selfDestructCost);
            DestroyWeapon();
        }

        if (!_isSmokePlaying && _currentAmmo <= _maxAmmo * _lowAmmoIndicator)
        {
            _isSmokePlaying = true;
            if (_smokePFX != null)
            {
                _smokePFX.Play();
            }
        }

        if (_isSmokePlaying && _currentAmmo > _maxAmmo * _lowAmmoIndicator)
        {
            _isSmokePlaying = false;
            if (_smokePFX != null)
            {
                _smokePFX.Stop();
            }
        }
    }

    #region [Enemy Tracking]
    private void TrackEnemiesInRange()
    {
        SphereCollider sphereCollider = GetComponent<SphereCollider>();
        Collider[] hits = Physics.OverlapSphere(transform.position, sphereCollider.radius);
        _enemiesInRange = 0;
        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                _enemiesInRange++;
            }
        }

        if (_enemiesInRange <= 0)
        {
            _currentTarget = null;
            OnTargetLost();
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && isActive)
        {
            if (_currentTarget == null)
            {
                _currentTarget = other;
            }
        }
    }

    protected virtual void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy") && isActive)
        {
            if (_currentTarget == null || !_currentTarget.gameObject.activeInHierarchy)
            {
                _currentTarget = other;
                //_damageTimer = 0f;
                OnTargetLost();
            }

            if (other == _currentTarget)
            {
                _damageTimer += Time.deltaTime;
                OnFire();
                if (_damageTimer > GetFireRate())
                {
                    _damageTimer = 0f;
                    HealthHandler health = other.GetComponent<HealthHandler>();
                    if (health != null)
                    {
                        health.Damage(_damagePerSecond);
                    }
                    _currentAmmo -= _ammoDepletionCount;
                    OnDamageDealt(other);
                }
            }
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy") && isActive)
        {
            if (other == _currentTarget)
            {
                _currentTarget = null;
                //_damageTimer = 0f;
            }
        }
    }

    protected void RotateTowardsTarget(Transform rotateGun, float rotationSpeed)
    {
        Quaternion targetRotation = Quaternion.LookRotation(_currentTarget.transform.position - rotateGun.position);
        rotateGun.rotation = Quaternion.RotateTowards(rotateGun.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
    #endregion

    #region [Damage Communication]
    protected virtual void OnFire()
    {

    }

    protected virtual void OnDamageDealt(Collider target)
    {

    }

    protected virtual void OnTargetLost()
    {

    }

    protected virtual float GetFireRate()
    {
        return 1f;
    }
    #endregion

    #region[Destroy Weapons]
    public virtual void DestroyWeapon()
    {
        if (transform.parent != null)
        {
            transform.parent.GetComponent<PlaceableZone>().ResetZone();
            transform.SetParent(null);
            Destroy(gameObject);
        }
    }

    public virtual void SendSelfDestructCost (int amount)
    {
        _selfDestructCost = amount;
        WarfundsHandler.Instance.WarfundPenalty(_selfDestructCost);
    }
    #endregion

    #region [Upgrade Weapons]
    public virtual void UpgradeWeapon()
    {
        _damagePerSecond *= 2;
        _ammoDepletionCount = Mathf.RoundToInt(_ammoDepletionCount * 1.5f);
        _selfDestructCost *= 2;
        _upgradeCost *= 2;
        _maxAmmo *= 2;
        _currentAmmo = _maxAmmo;
    }

    public virtual void UpdateUICostValues()
    {
        _dismantleValue = _selfDestructCost / 2;
        UIManager.Instance.UpdateDismantleDisplay(_dismantleValue);
    }

    public virtual void PurchaseUpgrade()
    {
        if (WarfundsHandler.Instance.Warfunds >= _upgradeCost)
        {
            WarfundsHandler.Instance.SpendWarfunds(_upgradeCost);
            UpgradeWeapon();
            UpdateUICostValues();
        }
        else
        {
            UIManager.Instance.DisplayNotEnoughFunds();
        }
    }
    #endregion
}
