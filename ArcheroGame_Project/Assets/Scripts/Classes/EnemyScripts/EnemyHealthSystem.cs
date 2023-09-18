using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthSystem : MonoBehaviour, IHealthSystem
{
    private EnemyData _enemyData;
    private GameObject _dieVFX;
    private float _currentHealth;
    private float _currentMaxHealth;
    private bool _doChipEffect;
    public float BaseMaxHealth => _enemyData.StatsData.MaxHealth;
    public float CurrentHealth => _currentHealth;

    public event Action<float, float, bool> OnHealthChanged;
    public static event Action OnEnemyDied;

    private void Awake()
    {
        _enemyData = GetComponent<EnemyData>();
        _dieVFX = _enemyData.DieVFX;
        _currentMaxHealth = BaseMaxHealth;
        _currentHealth = _currentMaxHealth;
        _doChipEffect = true;
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        _enemyData.Animator.SetTrigger("TakingDamage");
        OnHealthChanged?.Invoke(_currentHealth, BaseMaxHealth, _doChipEffect);
        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Vector3 diePosition = transform.position;
        diePosition.y = 1f;
        GameObject dieVFX = Instantiate(_dieVFX, diePosition, Quaternion.identity);
        OnEnemyDied?.Invoke();
        Destroy(gameObject);
    }
}
