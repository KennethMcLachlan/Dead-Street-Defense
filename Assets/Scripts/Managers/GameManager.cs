using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Text _versionText;

    private void Start()
    {
        _versionText.text = $"v{Application.version}";
    }

    public void RestartGame()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Start_Level");
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
