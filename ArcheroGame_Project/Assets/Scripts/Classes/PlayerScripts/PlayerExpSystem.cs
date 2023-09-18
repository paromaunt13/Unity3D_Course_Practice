using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExpSystem : MonoBehaviour, IExpSystem
{
    [SerializeField] int _expForEnemy;

    private PlayerData _playerData;

    private bool _canRecieveExp;

    private int _currentExp;
    private int _currentLevel;

    public int LevelExp => _playerData.ExpData.LevelExp;
    public int MaxLevel => _playerData.ExpData.MaxLevel;
    public int CurrentLevel { get => _currentLevel; set => _currentLevel = value; }
    public int CurrentExp => _currentExp;

    public static event Action OnLevelUp;
    public static event Action OnMaxLevelUp;
    public static event Action<int, bool> OnExpGained;

    void Awake()
    {
        _playerData = FindObjectOfType<PlayerData>();
        _canRecieveExp = true;
        _currentExp = 0;
        _currentLevel = 1;
        EnemyHealthSystem.OnEnemyDied += GainExp;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GainExp();
        }
    }

    private void OnDisable()
    {
        EnemyHealthSystem.OnEnemyDied -= GainExp;
    }

    private void GainExp()
    {
        if (_canRecieveExp)
        {
            _currentExp += _expForEnemy;
            OnExpGained?.Invoke(_expForEnemy, _canRecieveExp);
            if (_currentExp == LevelExp)
            {
                LevelUp();
                _currentExp = 0;
            }
        }
    }

    public void LevelUp()
    {
        AudioManager.Instance.PlaySound(_playerData.SoundData.LevelUpSound);
        _currentLevel++;
        OnLevelUp?.Invoke();
        if (_currentLevel == MaxLevel)
        {
            _canRecieveExp = false;
            OnMaxLevelUp?.Invoke();
        }
    }
}
