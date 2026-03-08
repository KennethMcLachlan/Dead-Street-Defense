using UnityEngine;
using UnityEngine.Rendering;

public class GatlingBehavior : MonoBehaviour
{
    private GameObject _muzzleFlash;
    private int _enemiesInRange = 0;
    [SerializeField] private int _currentAmmo;
    [SerializeField] private int _maxAmmo;
    [SerializeField] private float _ammoDepletionRate;
    [SerializeField] private int _ammoDepletionCount = 10;
    [SerializeField] private int _selfDestructCost = 200;
    public bool isActive;
    private Collider _currentTarget;

    private float _damageTimer = 0f;
    [SerializeField] private int _damagePerSecond = 1;

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

    private void DestroyGatling()
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
        _damagePerSecond += Mathf.RoundToInt(_damagePerSecond * 2);
        _ammoDepletionCount += Mathf.RoundToInt(_ammoDepletionCount / 2);
        _selfDestructCost += Mathf.RoundToInt(_selfDestructCost * 2);
        _maxAmmo += Mathf.RoundToInt(_maxAmmo * 2);
        _currentAmmo = _maxAmmo;
    }
}
