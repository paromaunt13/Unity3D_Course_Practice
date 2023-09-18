using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField] private GameObject[] _levelPrefabs;
    [SerializeField] private GameObject _playerPrefab;

    private GameObject _door;
    private GameObject _currentLevelInstance;

    private Transform _playerSpawnPosition;

    private int _currentLevelIndex;

    private void Awake()
    {
        Instance = this;
        _currentLevelIndex = 0;
        LoadLevel(_currentLevelIndex);
    }

    private void LoadLevel(int levelIndex)
    {
        if (_currentLevelInstance != null)
            Destroy(_currentLevelInstance);

        _currentLevelInstance = Instantiate(_levelPrefabs[levelIndex]);

        _playerSpawnPosition = _currentLevelInstance.GetComponentInChildren<PlayerSpawnPoint>().transform;
        _playerPrefab.transform.position = _playerSpawnPosition.position;
        
        _door = _currentLevelInstance.GetComponentInChildren<Door>().gameObject;

        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        if (_playerPrefab != null)
        {
            _playerPrefab.transform.position = _playerSpawnPosition.position;
            _playerPrefab.transform.rotation = Quaternion.identity;
        }
    }

    public void LevelComplete()
    {
        _door.gameObject.SetActive(false);
    }

    public void LoadNextLevel()
    {
        _currentLevelIndex++;

        if (_currentLevelIndex >= _levelPrefabs.Length)
        {
            return;
        }

        LoadLevel(_currentLevelIndex);
    }
}
