using System.Collections;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [SerializeField] private Animator _animator;   
    [SerializeField] private AudioSource _hurtSound;
    [SerializeField] private AudioSource _dieSound;
    [SerializeField] private GameObject _wraithSoul;

    [SerializeField] private int _maxHealth;
    [SerializeField] private int _currentHealth;

    void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        Hurt(damage);
        if (_currentHealth <= 0)
            StartCoroutine(Die());
    }

    IEnumerator Die()
    {
        _animator.SetTrigger("Die");
        _dieSound.Play();
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
        if (gameObject.CompareTag("Wraith"))
            _wraithSoul.SetActive(true);
    }

    void Hurt(int damage)
    {
        _hurtSound.Play();
        _currentHealth -= damage;
        _animator.SetTrigger("Hurt");
    }
}
