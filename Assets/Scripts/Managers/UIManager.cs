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

    [Header("Pop-Ups")]
    [SerializeField] private GameObject _upgradeGatling;
    [SerializeField] private GameObject _upgradeMissile;
    [SerializeField] private GameObject _levelStatus;
    [SerializeField] private GameObject _dismantleWeapon;

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
        //DisengageAllPopUpMenus();
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
        //PlaybackHandler.Instance.NaturalSpeed();
    }

}
