using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBarUI : MonoBehaviour
{
    [SerializeField] private Slider _healthBarSlider;
    [SerializeField] private Slider _chipEffectSlider;

    [SerializeField] private TMP_Text _healthBarText;

    [SerializeField] private float _chipEffectDuration;
    private EnemyHealthSystem _bossHealth;

    private void Start()
    {
        _bossHealth = GetComponentInParent<EnemyHealthSystem>();
        _healthBarSlider.value = _bossHealth.BaseMaxHealth;
        _healthBarText.text = _bossHealth.BaseMaxHealth.ToString();
        _bossHealth.OnHealthChanged += UpdateHealthBarUI;
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
