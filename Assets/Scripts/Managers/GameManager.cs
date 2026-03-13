using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
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
