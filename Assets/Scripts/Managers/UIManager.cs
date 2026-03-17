using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

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

    [Header("Upgrades")]
    [SerializeField] private GameObject _upgradeGatling;
    [SerializeField] private Text _gatlingUpgradeCost;
    [SerializeField] private GameObject _upgradeMissile;
    [SerializeField] private Text _upgradeMissileCost;
    [SerializeField] private GameObject _dismantleWeapon;
    [SerializeField] private Text _dismantleValue;

    [Header("Text & Numbers")]
    [SerializeField] private Text _warfundsNumber;
    [SerializeField] private Text _waveNumber;
    [SerializeField] private Text _livesText;
    [SerializeField] private Text _statusText;

    [Header("Playback")]
    [SerializeField] private GameObject _pause;
    [SerializeField] private GameObject _play;
    [SerializeField] private GameObject _fastForward;
    [SerializeField] private Button _fastForwardButton;

    [Header("Pop-Ups")]
    [SerializeField] private GameObject _waveInfo;
    [SerializeField] private GameObject _prepareText;
    [SerializeField] private GameObject _startText;
    [SerializeField] private GameObject _upgrade;
    [SerializeField] private GameObject _restartWindow;
    [SerializeField] private GameObject _gameOver;
    [SerializeField] private GameObject _notEnoughFunds;
    [SerializeField] private GameObject _gameWon;

    [Header("Other")]
    [SerializeField] private GameObject _player;

    private GatlingBehavior _selectedGatlingGun;
    private MissileLauncherBehavior _selectedMissileLauncher;
    private WeaponBehavior _selectedWeapon;
    private List<GameObject> _disabledWeapons = new List<GameObject>();
    #endregion

    #region [Awake & Start]
    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        _player.GetComponent<HealthHandler>();
    }
    #endregion

    #region [Health]
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
    #endregion

    #region [Warfunds]
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
        _upgradeMissileCost.text = amount.ToString();
    }

    public void DisplayNotEnoughFunds()
    {
        StartCoroutine(NotEnoughFundsRoutine());
    }

    private IEnumerator NotEnoughFundsRoutine()
    {
        AudioManager.Instance.PlayNegativeSFX();
        _notEnoughFunds.SetActive(true);
        yield return new WaitForSeconds(2f);
        _notEnoughFunds.SetActive(false);
    }
    #endregion

    #region [Playback]
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
    #endregion

    #region [Waves]
    public void UpdateWaveNumber(int amount)
    {
        _waveNumber.text = amount.ToString() + "/10";
    }

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
    #endregion

    #region [Player Death & Game Over]
    public void EngageGameOver()
    {
        _gameOver.SetActive(true);
    }

    public void EngageWinGame()
    {
        _gameWon.SetActive(true);
    }
    #endregion

    #region [Pause & Restart]
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
        _gameOver.SetActive(false);
        CloseUpgradeWindows();
        _fastForward.SetActive(false);
        _play.SetActive(true);
        Time.timeScale = 1f;
    }
    #endregion

    #region [Weapon Upgrades]
    public void ShowUpgradePopUp(WeaponType weaponType, GatlingBehavior gatlingGun = null, MissileLauncherBehavior missileLauncher = null)
    {
        _selectedGatlingGun = gatlingGun;
        _selectedMissileLauncher = missileLauncher;

        if (gatlingGun != null)
        {
            _selectedWeapon = gatlingGun;
        }
        else
        {
            _selectedWeapon = missileLauncher;
        }

        DisableAllWeaponInteraction();
        _upgrade.SetActive(true);

        switch (weaponType)
        {
            case WeaponType.GatlingGun:
                _upgradeGatling.SetActive(true);
                _upgradeMissile.SetActive(false);
                _dismantleWeapon.SetActive(true);
                if (gatlingGun != null)
                {
                    gatlingGun.UpdateUICostValues();
                }
                break;
            case WeaponType.MissileLauncher:
                _upgradeGatling.SetActive(false);
                _upgradeMissile.SetActive(true);
                _dismantleWeapon.SetActive(true);
                if (missileLauncher != null)
                {
                    missileLauncher.UpdateUICostValues();
                }
                break;
            default:
                _upgradeGatling.SetActive(false);
                _upgradeMissile.SetActive(false);
                _dismantleWeapon.SetActive(false);
                break;
        }
    }

    private void DisableAllWeaponInteraction()
    {
        _disabledWeapons.Clear();
        GameObject[] weapons = GameObject.FindGameObjectsWithTag("Weapon");
        foreach (GameObject weapon in weapons)
        {
            if (_selectedWeapon != null && weapon == _selectedWeapon.gameObject)
            {
                continue;
            }
            _disabledWeapons.Add(weapon);
            weapon.SetActive(false);
        }
        Time.timeScale = 0f;
    }

    private void EnableAllWeaponInteraction()
    {
        WeaponBehavior[] allWeapons = FindObjectsByType<WeaponBehavior>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (WeaponBehavior weapon in allWeapons)
        {
            weapon.gameObject.SetActive(true);
        }
        _disabledWeapons.Clear();
        Time.timeScale = 1f;
    }

    public void OnUpgradeButtonPressed()
    {
        if (_selectedWeapon != null)
        {
            _selectedWeapon.PurchaseUpgrade();
            _selectedWeapon = null;
            _selectedGatlingGun = null;
            _selectedMissileLauncher = null;
        }
        CloseUpgradeWindows();
        EnableAllWeaponInteraction();
    }

    public void OnUpgradeAndDismantleButtonCanceled()
    {
        _selectedWeapon = null;
        _selectedGatlingGun = null;
        _selectedMissileLauncher = null;
        CloseUpgradeWindows();
        EnableAllWeaponInteraction();
    }
    #endregion

    #region [Dismantle]
    public void OnDismantleButtonPressed()
    {
        if (_selectedWeapon != null)
        {
            WarfundsHandler.Instance.ReceiveWarfunds(_selectedWeapon.GetDismantleValue());
            _selectedWeapon.DestroyWeapon();
            CloseUpgradeWindows();
            _selectedWeapon = null;
            _selectedGatlingGun = null;
            _selectedMissileLauncher = null;
            EnableAllWeaponInteraction();
        }
    }

    private void CloseUpgradeWindows()
    {
        _upgrade.SetActive(false);
        _upgradeGatling.SetActive(false);
        _upgradeMissile.SetActive(false);
        _dismantleWeapon.SetActive(false);
        _fastForward.SetActive(false);
        _play.SetActive(true);
    }
    #endregion

    public bool IsUpgradePopUpOpen()
    {
        return _upgrade.activeSelf;
    }
}
