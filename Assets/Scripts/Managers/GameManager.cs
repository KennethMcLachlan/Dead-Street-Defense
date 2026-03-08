using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    //UIManager.Instance.EngagePauseMenu();
        //    PlaybackHandler.Instance.PauseGame();
        //}
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
