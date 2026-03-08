using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region[Variables & References]
    private static UIManager _instance;

    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("UI Manager is null");
            }
            return _instance;
        }
    }
    
    [Header("HUD Buttons")]
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _fastForwardButton;

    [Header("Upgrades")]
    [SerializeField] private GameObject _upgradeGatling;
    [SerializeField] private Text _gatlingUpgradeCost;
    [SerializeField] private GameObject _upgradeMissile;
    [SerializeField] private GameObject _dismantleWeapon;
    [SerializeField] private Text _dismantleValue;

    [Header("Weapon Buttons")]
    [SerializeField] private Button _gatlingButton;
    [SerializeField] private Button _doubleGatling;
    [SerializeField] private Button _missileTurret;
    [SerializeField] private Button _plasmaTurret;

    [Header("Text & Numbers")]
    [SerializeField] private Text _warfundsNumber;
    [SerializeField] private Text _waveNumber;
    [SerializeField] private Text _livesText;
    [SerializeField] private Text _statusText;

    [Header("Playback")]
    [SerializeField] private GameObject _pause;
    [SerializeField] private GameObject _play;
    [SerializeField] private GameObject _fastForward;

    [Header("Pop-Ups")]
    [SerializeField] private GameObject _waveInfo;
    [SerializeField] private GameObject _prepareText;
    [SerializeField] private GameObject _startText;
    [SerializeField] private GameObject _upgrade;
    [SerializeField] private GameObject _restartWindow;
    [SerializeField] private GameObject _gameOver;

    [Header("Other")]
    [SerializeField] private GameObject _player;

    private GatlingBehavior _selectedGatlingGun;

    #endregion

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        _player.GetComponent<HealthHandler>();
    }

    //Waves
    public void UpdateWaveNumber(int amount)
    {
        _waveNumber.text = amount.ToString() + "/10";
    }

    //Health
    public void UpdateLives(int amount)
    {
        _livesText.text = amount.ToString();
    }

    public void HealthStatusGood()
    {
        _statusText.color = Color.green;
        _statusText.text = "Good";
    }

    public void HealthStatusWarning()
    {
        _statusText.color = Color.yellow;
        _statusText.text = "Warning";
    }

    public void HealthStatusCritical()
    {
        _statusText.color = Color.red;
        _statusText.text = "Critical";
    }

    //Warfunds
    public void UpdateWarfunds(int amount)
    {
        _warfundsNumber.text = amount.ToString();
    }

    public void UpdateDismantleDisplay(int amount)
    {
        _dismantleValue.text = amount.ToString();
    }

    public void UpdateGatlingUpgradeCost(int amount)
    {
        _gatlingUpgradeCost.text = amount.ToString();
    }

    public void UpdateMissileLauncherUpgradeCost(int amount)
    {

    }

    //Playback
    public void UpdateToNaturalSpeed()
    {
        _play.SetActive(true);
        _pause.SetActive(false);
        _fastForward.SetActive(false);
        _fastForwardButton.interactable = true;
        DisengageAllPopUpMenus();
    }

    public void UpdateToDoubleSpeed()
    {
        _fastForward.SetActive(true);
        _play.SetActive(false);
        _pause.SetActive(false);
        _fastForwardButton.interactable = true;
    }

    public void UpdateToPause()
    {
        _pause.SetActive(true);
        _play.SetActive(false);
        _fastForward.SetActive(false);
        _fastForwardButton.interactable = false;
        EngagePauseMenu();
    }

    //Pop-ups

    //Waves
    public void EngageWaveInfo()
    {
        _waveInfo.SetActive(true);
    }

    public void SetWaveStartText()
    {
        _prepareText.SetActive(false);
        _startText.SetActive(true);
    }
    public void DisengageWaveInfo()
    {
        _startText.SetActive(false);
        _prepareText.SetActive(true);
        _waveInfo.SetActive(false);
    }

    //Player Death | Game Over
    public void EngageGameOver()
    {
        _gameOver.SetActive(true);
    }

    //Pause/Restart
    public void EngagePauseMenu()
    {
        _restartWindow.SetActive(true);
    }

    public void ResumeGame()
    {
        DisengageAllPopUpMenus();
    }

    public void DisengageAllPopUpMenus()
    {
        _restartWindow.SetActive(false);
        _upgrade.SetActive(false);
        _restartWindow.SetActive(false);
        _gameOver.SetActive(false);
        Time.timeScale = 1f;
    }

    //Weapon Upgrades
    public void ShowUpgradePopUp(WeaponType weaponType, GatlingBehavior gatlingGun = null)
    {
        _selectedGatlingGun = null;
        _selectedGatlingGun = gatlingGun;
        //SelectedMissileLauncher = missileLauncher;
        _upgrade.SetActive(true);
        switch (weaponType)
        {
            case WeaponType.GatlingGun:
                _upgradeGatling.SetActive(true);
                //_upgradeMissile.SetActive(false);
                _dismantleWeapon.SetActive(true);
                if (gatlingGun != null)
                {
                    gatlingGun.UpdateUICostValues();
                }
                break;
            case WeaponType.MissileLauncher:
                _upgradeGatling.SetActive(false);
                //_upgradeMissile.SetActive(true);
                _dismantleWeapon.SetActive(true);
                //Update Cost values
                break;
            default:
                _upgradeGatling.SetActive(false);
                //_upgradeMissile.SetActive(false);
                _dismantleWeapon.SetActive(false);
                break;
        }
    }

    //private void ReenableAllClickables()
    //{
    //    GatlingBehavior[] allGatlings = FindObjectsByType<GatlingBehavior>(FindObjectsSortMode.None);
    //    foreach (GatlingBehavior g in allGatlings)
    //    {
    //        g.EnableClickable();
    //    }
    //}

    public void OnUpgradeButtonPressed()
    {
        Debug.Log("Upgrading: " + (_selectedGatlingGun != null ? _selectedGatlingGun.gameObject.name : "NULL"));
        if (_selectedGatlingGun != null)
        {
            _selectedGatlingGun.PurchaseUpgrade();
            _selectedGatlingGun = null;
        }
        _upgrade.SetActive(false);
        _dismantleWeapon.SetActive(false);
    }

    public void OnUpgradeAndDismantleButtonCanceled()
    {
        if (_selectedGatlingGun != null)
        {
            //_selectedGatlingGun.ResetCurrentCostValues();
            _selectedGatlingGun = null;
        }
        _upgrade.SetActive(false);
        _dismantleWeapon.SetActive(false);
    }

    //Dismantle
    public void OnDismantleButtonPressed()
    {
        if (_selectedGatlingGun != null)
        {
            WarfundsHandler.Instance.ReceiveWarfunds(_selectedGatlingGun.GetDismantleValue());
            _selectedGatlingGun.DestroyGatling();
            _upgrade.SetActive(false);
            _dismantleWeapon.SetActive(false);
            _selectedGatlingGun = null;
        }
    }

    public bool IsUpgradePopUpOpen()
    {
        return _upgrade.activeSelf;
    }

}
