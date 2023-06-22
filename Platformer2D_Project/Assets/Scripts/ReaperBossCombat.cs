using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReaperBossCombat : MonoBehaviour
{
    [SerializeField] private GameObject _gameCompleteWindow;
    [SerializeField] private Animator _animator;

    [SerializeField] private AudioSource _hurtSound;
    [SerializeField] private AudioSource _dieSound;

    [SerializeField] private int _maxHealth;
    [SerializeField] private int _currentHealth;

    [SerializeField] private TMP_Text _healthText;
    [SerializeField] private Slider _healthSlider;

    void Start()
    {
        _healthSlider.maxValue = _maxHealth;
        _healthSlider.value = _healthSlider.maxValue;
        _healthText.text = $"{_healthSlider.maxValue}/{_maxHealth}";       
        _currentHealth = _maxHealth;
    } 

    IEnumerator Die()
    {
        _animator.SetTrigger("Die");
        _dieSound.Play();
        yield return new WaitForSeconds(1f);
        _gameCompleteWindow.SetActive(true);
        Time.timeScale = 0;
        gameObject.SetActive(false);
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
