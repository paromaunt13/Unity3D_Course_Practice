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
    private EnemyHealthSystem _enemyHealth;

    private void Start()
    {
        _enemyHealth = GetComponentInParent<EnemyHealthSystem>();
        _healthBarSlider.value = _enemyHealth.BaseMaxHealth;
        _healthBarText.text = _enemyHealth.BaseMaxHealth.ToString();
        _enemyHealth.OnHealthChanged += UpdateHealthBarUI;
    }

    private void UpdateHealthBarUI(float currentHealth, float maxHealth, bool doChipEffect)
    {
        _healthBarSlider.value = currentHealth / maxHealth;       
        _healthBarText.text = currentHealth.ToString();
        if (doChipEffect)
        {
            ChipEffect(currentHealth, maxHealth);
        }
    }

    private void ChipEffect(float currentHealth, float maxHealth)
    {
        float targetValue = currentHealth / maxHealth;
        _chipEffectSlider.DOValue(targetValue, _chipEffectDuration, false);
    }
}
