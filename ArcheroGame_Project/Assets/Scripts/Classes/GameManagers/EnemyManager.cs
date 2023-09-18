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
        EnemyHealthSystem.OnEnemyDied += EnemyKilled;
    } 

    private void OnDisable()
    {
        EnemyHealthSystem.OnEnemyDied -= EnemyKilled;
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
        List<EnemyData> enemies = FindObjectsOfType<EnemyData>().ToList();
        _enemyCount = enemies.Count;
    }

    public void EnemyKilled()
    {
        _enemyCount--;
        if (_enemyCount <= 0)
        {
            LevelManager.Instance.LevelComplete();
        }
    }
}
