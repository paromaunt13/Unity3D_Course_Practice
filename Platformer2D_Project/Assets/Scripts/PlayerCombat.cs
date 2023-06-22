using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverWindow;

    [SerializeField] private Animator _animator;

    [SerializeField] private Transform _attackPoint;
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private float _attackRange;  
    
    [SerializeField] public AudioSource _attackSound;
    [SerializeField] private AudioSource _hurtSound;
    [SerializeField] private AudioSource _dieSound;

    [SerializeField] public int _damage;
    [SerializeField] private int _projectileDamage;
    [SerializeField] private float _cooldown;

    [SerializeField] private int _maxHealth;
    [SerializeField] private int _currentHealth;

    [SerializeField] private TMP_Text _healthText;
    [SerializeField] private Slider _healthSlider;

    private bool _isAttacking;
    private void Start()
    {
        _healthSlider.maxValue = _maxHealth;
        _healthSlider.value = _healthSlider.maxValue;
        _healthText.text = $"{_healthSlider.maxValue}/{_maxHealth}";
        _currentHealth = _maxHealth;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !_isAttacking)
            StartCoroutine(Attack());
        _projectileDamage = Random.Range(10, 25);
    }
    IEnumerator Attack()
    {
        _isAttacking = true;
        _animator.SetTrigger("Attack");
        _attackSound.Play();

        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _enemyLayer);
        
        foreach (var enemy in enemiesHit)
        {
            if (enemy.GetComponent<EnemyCombat>())
                enemy.GetComponent<EnemyCombat>().TakeDamage(_damage);
            if (enemy.GetComponent<ReaperBossCombat>())
                enemy.GetComponent<ReaperBossCombat>().TakeDamage(_damage);
        }
            
        yield return new WaitForSeconds(_cooldown);
        _isAttacking = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Projectile"))
        {
            TakeDamage(_projectileDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
    }   

    IEnumerator Die()
    {
        _animator.SetTrigger("Die");
        _dieSound.Play();
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0;
        _gameOverWindow.SetActive(true);
    }

    void Hurt(int damage)
    {
        _hurtSound.Play();
        _currentHealth -= damage;
        _healthSlider.value = _currentHealth;
        if (_healthSlider.value <= 0)
            _healthSlider.value = 0;
        _healthText.text = $"{_healthSlider.value}/{_maxHealth}";
        _animator.SetTrigger("Hurt");
    }

    public void TakeDamage(int damage)
    {
        Hurt(damage);
        if (_currentHealth <= 0)
            StartCoroutine(Die());
    }
}
