using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompletionManager : MonoBehaviour
{
    [SerializeField] private GameObject _levelCompletePanel;
    [SerializeField] private GameObject _gameOverPanel;

    private void Start()
    {
        GameCompleteTrigger.OnGameComplete += GameComplete;
        PlayerHealthSystem.OnPlayerDied += OnPlayerDied;
    }

    private void OnDisable()
    {
        GameCompleteTrigger.OnGameComplete -= GameComplete;
        PlayerHealthSystem.OnPlayerDied -= OnPlayerDied;
    }

    public void GameComplete()
    {
        _levelCompletePanel.SetActive(true);
    }
    public void Exit()
    {
        SceneManager.LoadScene(0);
    }

    public void OnPlayerDied()
    {
        _gameOverPanel.SetActive(true);
    }
}
