using UnityEngine;

public class FinishZoneManager : MonoBehaviour
{
    [SerializeField] private AudioSource _levelCompleteSound;
    [SerializeField] private AudioSource _backgroundMusic;

    [SerializeField] private GameObject _levelCompleteInfo;
    [SerializeField] private GameObject _gemsCollectInfo;
    [SerializeField] private GameObject _player;

    private void OnEnable()
    {
        EventsControl.OnLevelCompleted += LevelCompleteAction;
    }

    private void OnDisable()
    {
        EventsControl.OnLevelCompleted -= LevelCompleteAction;
    }

    private void LevelCompleteAction()
    {
        _levelCompleteSound.Play();
        _backgroundMusic.Stop();
        _levelCompleteInfo.SetActive(true);
        _gemsCollectInfo.SetActive(false);
        _player.SetActive(false);
    }
}
