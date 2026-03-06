using UnityEngine;
using UnityEngine.Rendering;

public class GatlingBehavior : MonoBehaviour
{
    private GameObject _muzzleFlash;
    private int _enemiesInRange = 0;
    [SerializeField] private int _currentAmmo;
    [SerializeField] private int _maxAmmo;
    public bool isActive;

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
    }

    private void Update()
    {
        //if ammo supply is >0
        // ammo -1 * TIme.deltaTime
        // else destroy this object
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && isActive)
        {
            _enemiesInRange++;
        }
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy") && isActive)
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
            }

            // IF enemy is within range, damage enemy
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy") && isActive)
        {
            _enemiesInRange--;
            if (_enemiesInRange <= 0)
            {
                _enemiesInRange = 0;
                _muzzleFlash.SetActive(false);
            }
        }
    }
}
