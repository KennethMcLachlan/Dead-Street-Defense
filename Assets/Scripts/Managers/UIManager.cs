using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    
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

    #region Variables & References

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
    [SerializeField] private TextMeshPro _warfundsNumber;
    [SerializeField] private TextMeshPro _waveNumber;
    [SerializeField] private TextMeshPro _livesText;
    [SerializeField] private TextMeshPro _statusText;

    #endregion

    private void Awake()
    {
        _instance = this;
    }



}
