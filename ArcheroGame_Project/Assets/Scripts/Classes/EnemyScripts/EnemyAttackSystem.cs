using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackSystem : MonoBehaviour, IAttackSystem, ITargetSystem
{
    [SerializeField] private float _attackCooldown;
    [SerializeField] private float _timeToDestroy;
    [SerializeField] private float _detectionRange;

    private EnemyData _enemyData;
    private Transform _nearestTarget;

    private bool _playerDetected;
    private bool _allowToAttack;

    private float _attackPower = -230f;

    public float BaseDamage => _enemyData.StatsData.Damage;
    public GameObject ProjectilePrefab => _enemyData.ProjectilePrefab;
    public float TimeToAttack { get => _attackCooldown; set => _attackCooldown = value; }
    public float DetectionRange => _detectionRange;
    public Transform NearestTarget => _nearestTarget;

    void Awake()
    {
        _enemyData = GetComponent<EnemyData>();

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
        Collider[] colliders = Physics.OverlapSphere(transform.position, DetectionRange);

        float nearestDistance = DetectionRange;
        foreach (Collider collider in colliders)
        {
            if (collider.GetComponent<PlayerHealthSystem>())
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
            GameObject projectile = Instantiate(ProjectilePrefab, _enemyData.ProjectileSpawnPoint.position, _enemyData.ProjectileSpawnPoint.rotation);
            Projectile enemyProjectile = projectile.GetComponent<Projectile>();

            enemyProjectile.SetDamage(BaseDamage);
            projectile.GetComponent<Rigidbody>().AddForce(_enemyData.ProjectileSpawnPoint.transform.up * _attackPower, ForceMode.Impulse);
            _enemyData.Animator.SetTrigger("Attack");
            AudioManager.Instance.PlaySound(_enemyData.SoundData.AttackSound);

            Destroy(projectile, _timeToDestroy);

            _allowToAttack = false;
            yield return new WaitForSeconds(_attackCooldown);
            _allowToAttack = true;
        }
    }
    public void Attack()
    {
        StartCoroutine(Attacking());
    }
}
