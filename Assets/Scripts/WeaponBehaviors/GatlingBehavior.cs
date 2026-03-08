using UnityEngine;
using UnityEngine.Rendering;

public class GatlingBehavior : MonoBehaviour
{
    private GameObject _muzzleFlash;

    [Header("Ammunition")]
    [SerializeField] private int _currentAmmo;
    [SerializeField] private int _maxAmmo;
    [SerializeField] private float _ammoDepletionRate;
    [SerializeField] private int _ammoDepletionCount = 10;

    [Header("Warfund Costs")]
    [SerializeField] private int _selfDestructCost = 200;
    [SerializeField] private int _upgradeCost = 500;
    [SerializeField] private int _dismantleValue;
    public int GetDismantleValue() => _dismantleValue;

    [Header("Damage")]
    [SerializeField] private int _damagePerSecond = 1;
    private float _damageTimer = 0f;
    private Collider _currentTarget;
    private int _enemiesInRange = 0;

    public bool isActive;
    private void Start()
    {
        Transform firstChild = transform.GetChild(0);
        _muzzleFlash = firstChild.GetChild(1).gameObject;
        if (_muzzleFlash != null)
        {
            _muzzleFlash.SetActive(false);
        }
        else
        {
            Debug.LogError("Muzzle Flash is NULL");
        }

        _currentAmmo = _maxAmmo;
    }

    private void Update()
    {
        if (_currentAmmo <= 0)
        {
            Debug.Log("Ammo Depleted");
            isActive = false;
            SendSelfDestructCost(_selfDestructCost);
            DestroyGatling();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && isActive)
        {
            _enemiesInRange++;
            if (_currentTarget == null)
            {
                _currentTarget = other;
            }
        }
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy") && isActive && other == _currentTarget)
        {
            _muzzleFlash.SetActive(true);

            Transform rotateGun = gameObject.transform.GetChild(0);
            rotateGun.LookAt(other.transform.position);

            _damageTimer += Time.deltaTime;
            if (_damageTimer > 1f)
            {
                _damageTimer = 0f;
                HealthHandler health = other.GetComponent<HealthHandler>();
                if (health != null)
                {
                    health.Damage(_damagePerSecond);
                }

                _currentAmmo -= _ammoDepletionCount;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy") && isActive)
        {
            if (other == _currentTarget)
            {
                _currentTarget = null;
            }

            _enemiesInRange--;
            if (_enemiesInRange <= 0)
            {
                _enemiesInRange = 0;
                _muzzleFlash.SetActive(false);
            }
        }
    }

    public void DestroyGatling()
    {
        if (transform.parent != null)
        {
            Debug.Log("Gatling Gun Destroyed");
            transform.parent.GetComponent<PlaceableZone>().ResetZone();
            transform.SetParent(null);
            Destroy(gameObject);
        }
    }

    private void SendSelfDestructCost(int amount)
    {
        _selfDestructCost = amount;
         WarfundsHandler.Instance.WarfundPenalty(_selfDestructCost);
    }

    public void UpgradeWeapon()
    {
        _damagePerSecond *= 2;
        _ammoDepletionCount = Mathf.RoundToInt(_ammoDepletionCount * 1.5f);
        _selfDestructCost *= 2;
        _upgradeCost *= 2;
        _maxAmmo *= 2;
        _currentAmmo = _maxAmmo;
    }

    public void UpdateUICostValues() // Display cost before purchase
    {
        UIManager.Instance.UpdateGatlingUpgradeCost(_upgradeCost);
        _dismantleValue = _selfDestructCost * 2;
        UIManager.Instance.UpdateDismantleDisplay(_dismantleValue);
    }

    public void PurchaseUpgrade()
    {
        if (WarfundsHandler.Instance.Warfunds >= _upgradeCost)
        {
            WarfundsHandler.Instance.SpendWarfunds(_upgradeCost);
            UpgradeWeapon();
            UpdateUICostValues();
        }
        else
        {
            Debug.Log("Not enough Warfunds to upgrade Gatling Gun");
        }
    }

    //public void DisableClickable()
    //{
    //    GetComponent<ClickableObject>().enabled = false;
    //}

    //public void EnableClickable()
    //{
    //    GetComponent<ClickableObject>().enabled = true;
    //}
}
