using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBarUI : MonoBehaviour
{
    [SerializeField] private Slider _healthBarSlider;
    [SerializeField] private Slider _chipEffectSlider;

    [SerializeField] private TMP_Text _healthBarText;

    [SerializeField] private float _chipEffectDuration;
    private Enemy _enemy;

    private void Start()
    {       
        _enemy = GetComponentInParent<Enemy>();
        _healthBarSlider.value = _enemy.MaxHealth;
        _healthBarText.text = _enemy.MaxHealth.ToString();
        _enemy.OnHealthChanged += UpdateHealthBarUI;
    }

    private void UpdateHealthBarUI(float currentHealth, float maxHealth)
    {
        _healthBarSlider.value = currentHealth / maxHealth;       
        _healthBarText.text = currentHealth.ToString();
        ChipEffect(currentHealth, maxHealth);
    }

    private void ChipEffect(float currentHealth, float maxHealth)
    {
        float targetValue = currentHealth / maxHealth;
        _chipEffectSlider.DOValue(targetValue, _chipEffectDuration, false);
    }
}
