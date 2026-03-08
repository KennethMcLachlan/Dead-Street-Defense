using UnityEngine;

public enum WeaponType
{
    GatlingGun,
    MissileLauncher
}
public class ClickableObject : MonoBehaviour, IClickable
{
    [SerializeField] private WeaponType _weaponType;

    public void OnClick()
    {
        GatlingBehavior gatlingGun = GetComponent<GatlingBehavior>();
        UIManager.Instance.ShowUpgradePopUp(_weaponType, gatlingGun);
    }
}

   

