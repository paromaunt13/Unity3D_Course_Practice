using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;
    private int _enemyCount;
    private void OnEnable()
    {
        Enemy.OnEnemyKilled += EnemyKilled;
    } 

    private void OnDisable()
    {
        Enemy.OnEnemyKilled -= EnemyKilled;
    }

    private void Start()
    {
        CheckForEnemy();
    }
    private void Update()
    {
        CheckForEnemy();
    }
    public void CheckForEnemy()
    {
        List<Enemy> enemies = FindObjectsOfType<Enemy>().ToList();
        _enemyCount = enemies.Count;
    }

    public void EnemyKilled()
    {
        _enemyCount--;
        Debug.Log($"Оставшееся количество врагов: {_enemyCount}");
        if (_enemyCount <= 0)
        {
            LevelManager.Instance.LevelComplete();
        }
    }
}
