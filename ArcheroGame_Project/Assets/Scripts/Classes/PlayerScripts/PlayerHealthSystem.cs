using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour, IHealthSystem
{
    private PlayerData _playerData;
    private float _currentHealth;
    private float _currentMaxHealth;
    private bool _doChipEffect;
    public float BaseMaxHealth => _playerData.StatsData.MaxHealth;
    public float CurrentHealth => _currentHealth;

    public event Action<float, float, bool> OnHealthChanged;
    public static event Action OnPlayerDied;

    private void Awake()
    {      
        _playerData = FindObjectOfType<PlayerData>();
        _currentMaxHealth = BaseMaxHealth;
        _currentHealth = _currentMaxHealth;       
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            AddMaxHealth(200);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            Heal(200);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            TakeDamage(200);
        }
    }

    private IEnumerator TakeDamageOverTime(float damage, float duration, int tickCount)
    {
        float damagePerTick = damage / duration;
        float tickInterval = duration / tickCount;

        while (duration > 0)
        {
            TakeDamage(damagePerTick);
            duration -= tickInterval;
            yield return new WaitForSeconds(tickInterval);
            if (duration == 0)
            {
                yield return null;
            }
            if (_currentHealth <= 0)
            {
                Die();
            }
        }
    }

    public void TakeDamage(float damage)
    {
        _doChipEffect = true;
        AudioManager.Instance.PlaySound(_playerData.SoundData.HurtSound);
        _playerData.Animator.SetTrigger("TakingDamage");
        _currentHealth -= damage;
        OnHealthChanged?.Invoke(_currentHealth, _currentMaxHealth, _doChipEffect);
        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            OnHealthChanged?.Invoke(_currentHealth, _currentMaxHealth, _doChipEffect);
            Die();
        }
    }
    public void Die()
    {
        OnPlayerDied?.Invoke();
        Destroy(gameObject);
        StopAllCoroutines();
    }

    public void TakeOverTimeDamage(float damage, float duration, int tickCount)
    {
        StartCoroutine(TakeDamageOverTime(damage, duration, tickCount));
    }

    public void Heal(float healAmount)
    {
        _doChipEffect = true;
        _currentHealth += healAmount;
        if (_currentHealth > _currentMaxHealth)
        {
            _currentHealth = _currentMaxHealth;
        }
        OnHealthChanged?.Invoke(_currentHealth, _currentMaxHealth, _doChipEffect);
    }

    public void AddMaxHealth(float healthAmount)
    {
        _currentMaxHealth += healthAmount;
        _doChipEffect = false;
        OnHealthChanged?.Invoke(_currentHealth, _currentMaxHealth, _doChipEffect);
        _doChipEffect = true;
    }
}
