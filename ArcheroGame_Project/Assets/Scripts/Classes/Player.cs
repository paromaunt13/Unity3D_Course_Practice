using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, ICharacter
{
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private Transform _projectileSpawnPoint;
    [SerializeField] private float _attackCooldown;
    [SerializeField] private StatsData _statsData;
    [SerializeField] private ExpData _expData;
    [SerializeField] private SoundData _soundData;

    private Button _attackButton;
    private Animator _animator;

    private bool _allowToAttack;
    private bool _canRecieveExp;

    private float _currentHealth;
    private int _currentExp;
    private int _currentLevel = 1;
    private int _currentDamage;
    private int _expForEnemy = 20;
    private float _timeToDestroy = 0.8f;


    public event Action<float, float> OnHealthChanged;
    public static event Action OnLevelUp;
    public static event Action OnMaxLevelUp;
    public static event Action<int, bool> OnExpGained;
    public static Action OnPlayerDied;
    public GameObject ProjectilePrefab => _projectilePrefab;
    public StatsData StatsData => _statsData;
    public ExpData ExpData => _expData;
    public SoundData SoundData => _soundData;
    public float MaxHealth { get => StatsData.MaxHealth; }
    public float CurrentHealth { get => _currentHealth; set => _currentHealth = value; }
    public int Damage => StatsData.Damage;
    public int CurrentExp { get => _currentExp; set => _currentExp = value; } 
    public int CurrentLevel { get => _currentLevel; set => _currentLevel = value; }
    public int LevelExp { get => ExpData.LevelExp; }
    public int MaxLevel => ExpData.MaxLevel;
    public int CurrentDamage { get => _currentDamage; set => _currentDamage = value; }
    public float AttackSpeed { get => _attackCooldown; set => _attackCooldown = value; }
  
    private void Start()
    {
        _currentHealth = MaxHealth;
        _currentDamage = Damage;

        _currentExp = 0;
        _allowToAttack = true;
        _canRecieveExp = true;

        _attackButton = FindObjectOfType<AttackButton>().GetComponent<Button>();
        _attackButton.onClick.AddListener(Attack);

        _animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GainExp();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            TakeDamage(30);
        }
    }
    private void OnEnable()
    {
        Enemy.OnEnemyKilled += GainExp;
    }

    private void OnDisable()
    {
        Enemy.OnEnemyKilled -= GainExp;
    }

    private void GainExp()
    {
        if (_canRecieveExp)
        {
            _currentExp += _expForEnemy;
            Debug.Log($"Current exp {_currentExp}");
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
        AudioManager.Instance.PlaySound(_soundData.LevelUpSound);
        _currentLevel++;
        Debug.Log($"Текущий уровен персонажа: {_currentLevel}");
        OnLevelUp?.Invoke();
        if (_currentLevel == MaxLevel)
        {
            Debug.Log("Достигнут максимальный уровень");
            _canRecieveExp = false;
            OnMaxLevelUp?.Invoke();
        }
    }
    public void Attack()
    {
        AudioManager.Instance.PlaySound(_soundData.AttackSound);
        _animator.SetTrigger("Attack");
        StartCoroutine(Attacking());
        Debug.Log($"Текущий урон: {CurrentDamage}");
        Debug.Log($"Текущая скорость атаки: {AttackSpeed}");
    }

    public void Die()
    {
        OnPlayerDied?.Invoke();
        Debug.Log("Персонаж уничтожен");
        Destroy(gameObject);
        StopAllCoroutines();
    }  
    
    public void TakeDamage(int damage)
    {
        AudioManager.Instance.PlaySound(_soundData.HurtSound);
        _animator.SetTrigger("TakingDamage");
        _currentHealth -= damage;
        OnHealthChanged?.Invoke(_currentHealth, MaxHealth);
        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            Die();
        }
    }

    public void TakeOverTimeDamage(int damage, float duration)
    {
        StartCoroutine(TakeDamageOverTime(damage, duration));
    }

    public IEnumerator TakeDamageOverTime(int damage, float duration)
    {
        float damagePerTick = damage / duration;
        float tickInterval = duration / 3;

        while (duration > 0)
        {
            _currentHealth -= damagePerTick;
            OnHealthChanged?.Invoke(_currentHealth, MaxHealth);
            _animator.SetTrigger("TakingDamage");
            Debug.Log("Player takes " + damagePerTick + " damage.");
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

    public void Heal(int healAmount)
    {
        AudioManager.Instance.PlaySound(_soundData.HealSound);
        _currentHealth += healAmount;        
        if (_currentHealth > MaxHealth)
        {
            _currentHealth = MaxHealth;
        }
        OnHealthChanged?.Invoke(_currentHealth, MaxHealth);
    }

    private IEnumerator Attacking()
    {
        if (_allowToAttack)
        {
            GameObject projectile = Instantiate(ProjectilePrefab, _projectileSpawnPoint.position, _projectileSpawnPoint.rotation);
            PlayerProjectile playerProjectile = projectile.GetComponent<PlayerProjectile>();

            playerProjectile.SetDamage(_currentDamage);
            projectile.GetComponent<Rigidbody>().velocity = _projectileSpawnPoint.forward * 20f;

            Destroy(projectile, (_timeToDestroy - AttackSpeed));

            _allowToAttack = false;
            yield return new WaitForSeconds(_attackCooldown);
            _allowToAttack = true;
        }
    }
}
