using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealthBarUI : MonoBehaviour
{
    [SerializeField] private Slider _healthBarSlider;
    [SerializeField] private Slider _chipEffectSlider;

    [SerializeField] private TMP_Text _healthBarText;

    [SerializeField] private Gradient _gradient;

    [SerializeField] private float _chipEffectDuration;

    private Image _fillImage;

    private PlayerHealthSystem _playerHealth;

    private void Start()
    {
        _playerHealth = FindObjectOfType<PlayerHealthSystem>();

        _healthBarSlider.maxValue = _playerHealth.BaseMaxHealth;
        _healthBarSlider.value = _playerHealth.BaseMaxHealth;
        _healthBarText.text = _healthBarSlider.value.ToString();

        _chipEffectSlider.maxValue = _healthBarSlider.maxValue;
        _chipEffectSlider.value = _healthBarSlider.value;
        
        _playerHealth.OnHealthChanged += UpdateHealthBarUI;

        _fillImage = GetComponentInChildren<PlayerHPBarFill>().GetComponent<Image>();
    }
    private void OnDisable()
    {
        _playerHealth.OnHealthChanged -= UpdateHealthBarUI;
    }

    private void UpdateHealthBarUI(float currentValue, float maxValue, bool doChipEffect)
    {
        _healthBarSlider.value = currentValue;
        _healthBarSlider.maxValue = maxValue;
        _healthBarText.text = currentValue.ToString();

        float noramalizedValue = currentValue / maxValue;
        _fillImage.color = ColorFromGradient(noramalizedValue);

        if (doChipEffect)
        {
            ChipEffect(_healthBarSlider.value);
        }
        else
        {
            _chipEffectSlider.value = currentValue;
            _chipEffectSlider.maxValue = maxValue;
        }
        //_healthBarSlider.value = currentValue / maxValue;
        //_fillImage.color = ColorFromGradient(_healthBarSlider.value);
        //_healthBarText.text = currentValue.ToString();
        //_chipEffectSlider.value = _healthBarSlider.value;
        //if (doChipEffect)
        //{
        //    ChipEffect(_healthBarSlider.value);
        //}          
    }

    private void ChipEffect(float targetValue)
    {
        _chipEffectSlider.DOValue(targetValue, _chipEffectDuration, false);
    }

    private Color ColorFromGradient(float value)
    {
        return _gradient.Evaluate(value);
    }
}
