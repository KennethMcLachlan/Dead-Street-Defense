using UnityEngine;

public class PlaybackHandler : MonoBehaviour
{
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

}
