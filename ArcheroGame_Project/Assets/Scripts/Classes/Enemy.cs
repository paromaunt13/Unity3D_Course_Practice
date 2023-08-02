using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IEnemy
{
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private Transform _projectileSpawnPoint;
    [SerializeField] private StatsData _statsData;
    [SerializeField] private float _attackCooldown;   
    [SerializeField] private float _detectionRange;
    [SerializeField] private SoundData _soundData;

    private Transform _nearestTarget;

    private bool _playerDetected;
    private bool _allowToAttack;

    private float _currentHealth;
    private float _attackPower = -230f;

    public event Action<float, float> OnHealthChanged;
    public static Action OnEnemyKilled;
    public StatsData StatsData => _statsData;
    public SoundData SoundData => _soundData;
    public float MaxHealth => StatsData.MaxHealth;
    public float CurrentHealth { get => _currentHealth; set => _currentHealth = value; }
    public GameObject ProjectilePrefab => _projectilePrefab;
    public int Damage => StatsData.Damage;
    public float DetectionRange => _detectionRange;
    public Transform NearestTarget => _nearestTarget;

    private void Start()
    {
        _currentHealth = MaxHealth;

        _allowToAttack = true;
    }

    private void Update()
    {
        CheckForPlayer();
        if (_playerDetected)
        {
            Attack();
        }
    }

    private void CheckForPlayer()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _detectionRange);

        float nearestDistance = _detectionRange;
        foreach (Collider collider in colliders)
        {
            if (collider.GetComponent<Player>())
            {
                float distanceToTarget = Vector3.Distance(transform.position, collider.transform.position);
                if (distanceToTarget < nearestDistance)
                {
                    nearestDistance = distanceToTarget;
                    _nearestTarget = collider.transform;
                    _playerDetected = true;
                }
            }
        }

        if (_nearestTarget != null)
        {
            Vector3 directionToEnemy = _nearestTarget.transform.position - transform.position;
            transform.rotation = Quaternion.LookRotation(directionToEnemy);
        }
    }

    private IEnumerator Attacking()
    {
        if (_allowToAttack)
        {
            AudioManager.Instance.PlaySound(_soundData.AttackSound);
            GameObject projectile = Instantiate(ProjectilePrefab, _projectileSpawnPoint.position, _projectileSpawnPoint.rotation);
            EnemyProjectile enemyProjectile = projectile.GetComponent<EnemyProjectile>();

            enemyProjectile.SetDamage(Damage);
            projectile.GetComponent<Rigidbody>().AddForce(_projectileSpawnPoint.transform.up  * _attackPower, ForceMode.Impulse);

            Destroy(projectile, 2f);

            _allowToAttack = false;
            yield return new WaitForSeconds(_attackCooldown);
            _allowToAttack = true;
        }
    }
    public void Attack()
    {        
        StartCoroutine(Attacking());
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        OnHealthChanged?.Invoke(_currentHealth, MaxHealth);
        if (_currentHealth <= 0)
        {
            Die();
        }
    }
    
    public void Die()
    {
        OnEnemyKilled?.Invoke();
        Destroy(gameObject);
    }
}
