using UnityEngine;

public class PlaybackHandler : MonoBehaviour
{
    private static PlaybackHandler _instance;

    public static PlaybackHandler Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("PlaybackHandler is NULL");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        NaturalSpeed();
    }
    public void NaturalSpeed()
    {
        Time.timeScale = 1.0f;
        UIManager.Instance.UpdateToNaturalSpeed();
    }

    public void DoubleSpeed()
    {
        Time.timeScale = 2.0f;
        UIManager.Instance.UpdateToDoubleSpeed();
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        UIManager.Instance.UpdateToPause();
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        UIManager.Instance.EngageGameOver();
    }
}
