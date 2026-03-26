using UnityEngine;

public enum WeaponType
{
    GatlingGun,
    MissileLauncher
}

public class ClickableObject : MonoBehaviour, IClickable
{
    [SerializeField] private WeaponType _weaponType;
    public WeaponType WeaponType => _weaponType;

    public void OnClick()
    {
        Draggable draggable = GetComponent<Draggable>();
        if (draggable != null && draggable.enabled)
        {
            return;
        }

        GatlingBehavior gatlingGun = GetComponent<GatlingBehavior>();
        MissileLauncherBehavior missileLauncher = GetComponent<MissileLauncherBehavior>();
        UIManager.Instance.ShowUpgradePopUp(_weaponType, gatlingGun, missileLauncher);
    }
}

   

