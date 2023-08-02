using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PoisonBossHealthBarUI : MonoBehaviour
{
    [SerializeField] private Slider _healthBarSlider;
    [SerializeField] private Slider _chipEffectSlider;

    [SerializeField] private TMP_Text _healthBarText;

    [SerializeField] private float _chipEffectDuration;
    private PoisonBoss _poisonBoss;

    private void Start()
    {
        _poisonBoss = GetComponentInParent<PoisonBoss>();
        _healthBarSlider.value = _poisonBoss.MaxHealth;
        _healthBarText.text = _poisonBoss.MaxHealth.ToString();
        _poisonBoss.OnHealthChanged += UpdateHealthBarUI;
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
