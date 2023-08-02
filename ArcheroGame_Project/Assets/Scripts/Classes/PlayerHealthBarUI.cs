using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealthBarUI : MonoBehaviour
{
    [SerializeField] private Slider _healthBarSlider;
    [SerializeField] private Slider _chipEffectSlider;

    [SerializeField] private TMP_Text _healthBarText;

    //[SerializeField] private Gradient _gradient;
    [SerializeField] private float _chipEffectDuration;

    private Player _player;

    private void Start()
    {
        _player = GetComponentInParent<Player>();
        _healthBarSlider.value = _player.MaxHealth;
        _healthBarText.text = _player.MaxHealth.ToString();
        _player.OnHealthChanged += UpdateHealthBarUI;
    }

    private void UpdateHealthBarUI(float currentHealth, float maxHealth)
    {
        _healthBarSlider.value = currentHealth / maxHealth;
        //_healthBarSlider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = ColorFromGradient(_healthBarSlider.value);
        _healthBarText.text = currentHealth.ToString();
        ChipEffect(currentHealth, maxHealth);
    }

    private void ChipEffect(float currentHealth, float maxHealth)
    {
        float targetValue = currentHealth / maxHealth;
        _chipEffectSlider.DOValue(targetValue, _chipEffectDuration, false);
    }
    //private Color ColorFromGradient(float value)
    //{
    //    return _gradient.Evaluate(value);
    //}
}
