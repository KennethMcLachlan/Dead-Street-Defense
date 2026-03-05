using UnityEngine;

public class GatlingBehavior : MonoBehaviour
{
    private GameObject _muzzleFlash;
    private int _enemiesInRange = 0;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            _enemiesInRange++;
        }
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            _muzzleFlash.SetActive(true);

            var target = other.transform.position;
            Transform rotateGun = gameObject.transform.GetChild(0);
            rotateGun.LookAt(target);

            // IF enemy is within range, damage enemy
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
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
