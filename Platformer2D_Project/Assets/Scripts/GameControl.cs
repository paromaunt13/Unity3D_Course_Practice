using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{

    [SerializeField] private GameObject _pauseMenu;

    private bool isPaused;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            Pause();           
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            Continue();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }

    public void Pause()
    {
        _pauseMenu.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
    }

    public void Continue()
    {
        _pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

}
