using UnityEngine;

public class WarfundsHandler : MonoBehaviour
{
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

    private void Awake()
    {
        _instance = this;
    }


    void Start()
    {

        UpdateUI();
    }

    void Update()
    {
        
    }

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
        _warfunds -= amount;
        UpdateUI();
    }

    public void PurchaseGatlingGun()
    {
        if (_warfunds >= _gatlingGunCost)
        {
            _warfunds -= _gatlingGunCost;
            SpawnManager.Instance.SpawnGatling();
            UpdateUI();
        }
        else
        {
            //Cant afford weapon
            UIManager.Instance.DisplayNotEnoughFunds();
            Debug.Log("Can't afford Gatling Gun!");
            //Make Popup or SFX to indicate insufficient funds
        }
    }

    public void UpgradeGatlingGun(int amount)
    {
        //_gatlingGunUpgradeCost = amount;
        //if (_warfunds >= _gatlingGunUpgradeCost)
        //{
        //    _warfunds -= _gatlingGunUpgradeCost;
        //    UpdateUI();
        //}
        //else
        //{
        //    //Cant afford upgrade
        //    Debug.Log("Can't afford Gatling Gun Upgrade!");
        //    //Make Popup or SFX to indicate insufficient funds
        //}
    }

    public void DismantleGatlingGun()
    {
        _warfunds += _gatlingGunCost / 2;
        UpdateUI();
    }

    public void PurchaseMissileLauncher()
    {
        if (_warfunds >= _missileLauncherCost)
        {
            _warfunds -= _missileLauncherCost;
            SpawnManager.Instance.SpawnMissileTurret();
            UpdateUI();
        }
        else
        {
            //Cant afford weapon
            UIManager.Instance.DisplayNotEnoughFunds();
            Debug.Log("Can't afford Missile Launcher!");
            //Make Popup or SFX to indicate insufficient funds
        }
    }

    public void DismantleMissileLauncher()
    {
        _warfunds += _missileLauncherCost / 2;
        UpdateUI();
    }

    public void UpdateUI()
    {
        UIManager.Instance.UpdateWarfunds(_warfunds);
    }

}
