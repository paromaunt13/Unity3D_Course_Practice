using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerAttackSystem : MonoBehaviour, IAttackSystem
{
    [SerializeField] private float _attackCooldown;
    [SerializeField] private float _timeToDestroy;

    private bool _allowToAttack;
    private float _currentDamage;

    private PlayerData _playerData;
    private PlayerMovement _playerMovement;
    private Button _attackButton;

    public float BaseDamage => _playerData.StatsData.Damage;
    public GameObject ProjectilePrefab => _playerData.ProjectilePrefab;
    public float CurrentDamage { get => _currentDamage; set => _currentDamage = value; }
    public float TimeToAttack { get => _attackCooldown; set => _attackCooldown = value; }

    void Start()
    {
        _playerData = GetComponent<PlayerData>();
        _playerMovement = GetComponent<PlayerMovement>();
        _attackButton = FindObjectOfType<AttackButton>().GetComponent<Button>();
        _attackButton.onClick.AddListener(Attack);

        _allowToAttack = true;
        _currentDamage = BaseDamage;
    }

    private IEnumerator Attacking()
    {
        if ( _allowToAttack && !_playerMovement.IsMoving)
        {
            GameObject projectile = Instantiate(ProjectilePrefab, _playerData.ProjectileSpawnPoint.position, _playerData.ProjectileSpawnPoint.rotation);
            PlayerProjectile playerProjectile = projectile.GetComponent<PlayerProjectile>();

            playerProjectile.SetDamage(_currentDamage);
            projectile.GetComponent<Rigidbody>().velocity = _playerData.ProjectileSpawnPoint.forward * 20f;
            _playerData.Animator.SetTrigger("Attack");
            AudioManager.Instance.PlaySound(_playerData.SoundData.AttackSound);

            Destroy(projectile, (_timeToDestroy - TimeToAttack));

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
