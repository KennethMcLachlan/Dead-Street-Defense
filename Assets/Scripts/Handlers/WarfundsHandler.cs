using NUnit.Framework.Constraints;
using UnityEngine;

public class WarfundsHandler : MonoBehaviour
{
    #region [Instance & Variables]
    private static WarfundsHandler _instance;
    public static WarfundsHandler Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("Warfunds Handler is NULL!");
            }
            return _instance;
        }
    }

    [SerializeField] private int _warfunds = 500;
     public int Warfunds
    {
        get => _warfunds;
        set => _warfunds = value;
    }

    [SerializeField] private int _gatlingGunCost = 200;
    private int _gatlingGunUpgradeCost;
    [SerializeField] private int _missileLauncherCost = 500;
    #endregion

    #region [Awake & Start]
    private void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        UpdateUI();
    }
    #endregion

    public void UpdateUI()
    {
        UIManager.Instance.UpdateWarfunds(_warfunds);
    }

    #region [Warfund Spendatures]
    public void ReceiveWarfunds(int amount)
    {
        _warfunds += amount;
        UpdateUI();
    }

    public void SpendWarfunds(int amount)
    {
        _warfunds -= amount;
        UpdateUI();
    }

    public void WarfundPenalty(int amount)
    {
        AudioManager.Instance.PlayNegativeSFX();
        _warfunds -= amount;
        UpdateUI();
    }

    public void RefundWeapon(WeaponType weaponType)
    {
        switch (weaponType)
        {
            case WeaponType.GatlingGun:
                ReceiveWarfunds(_gatlingGunCost);
                break;
            case WeaponType.MissileLauncher:
                ReceiveWarfunds(_missileLauncherCost);
                break;
        }
    }
    #endregion

    #region [Purchase & Dismantle Weapons]
    public void PurchaseGatlingGun()
    {
        if (_warfunds >= _gatlingGunCost)
        {
            AudioManager.Instance.PlayPositiveSFX();
            _warfunds -= _gatlingGunCost;
            SpawnManager.Instance.SpawnGatling();
            UpdateUI();
        }
        else
        {
            UIManager.Instance.DisplayNotEnoughFunds();
        }
    }

    public void DismantleGatlingGun()
    {
        AudioManager.Instance.PlayPositiveSFX();
        _warfunds += _gatlingGunCost / 2;
        UpdateUI();
    }

    public void PurchaseMissileLauncher()
    {
        if (_warfunds >= _missileLauncherCost)
        {
            AudioManager.Instance.PlayPositiveSFX();
            _warfunds -= _missileLauncherCost;
            SpawnManager.Instance.SpawnMissileTurret();
            UpdateUI();
        }
        else
        {
            UIManager.Instance.DisplayNotEnoughFunds();
        }
    }

    public void DismantleMissileLauncher()
    {
        AudioManager.Instance.PlayPositiveSFX();
        _warfunds += _missileLauncherCost / 2;
        UpdateUI();
    }
    #endregion


}
