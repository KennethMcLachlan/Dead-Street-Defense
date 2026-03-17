using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;

    public static AudioManager Instance
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

    [SerializeField] private AudioSource _explosionSFX;
    [SerializeField] private AudioSource _yesSFX;
    [SerializeField] private AudioSource _noSFX;
    [SerializeField] private AudioSource _gameOverSFX;
    [SerializeField] private AudioSource _waveStartSFX;
    [SerializeField] private AudioSource _youWinSFX;

    private void Awake()
    {
        _instance = this;
    }

    public void PlayExplosionSFX()
    {
        _explosionSFX.Play();
    }

    public void PlayPositiveSFX()
    {
        _yesSFX.Play();
    }

    public void PlayNegativeSFX()
    {
        _noSFX.Play();
    }

    public void LoseGameSFX()
    {
        _gameOverSFX.Play();
    }

    public void WaveStartSFX()
    {
        _waveStartSFX.Play();
    }

    public void YouWinSFX()
    {
        _youWinSFX.Play();
    }
}
