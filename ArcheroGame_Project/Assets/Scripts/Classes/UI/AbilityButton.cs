using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class AbilityButton : MonoBehaviour
{
    [SerializeField] private TMP_Text _cooldownText;
    [SerializeField] private Slider _cooldownSlider;
    [SerializeField] private Ability _ability;

    private Button _button;
    private Image _buttonImage;
    private float _cooldownTime;
    private bool _isOnCooldown;  

    private void Start()
    {
        _cooldownTime = _ability.Cooldown;
        _cooldownSlider.maxValue = _cooldownTime;
        _isOnCooldown = false;       
        _button = GetComponentInChildren<Button>();
        _buttonImage = _button.GetComponent<Image>();
        _buttonImage.sprite = _ability.Icon;
        
        _button.onClick.AddListener(OnButtonPressed);
    }

    private void StartCooldown()
    {
        OnCooldownStart();      
        _cooldownSlider.DOValue(0f, _cooldownTime).SetEase(Ease.Linear).OnComplete(OnCooldownEnd);
        _cooldownSlider.onValueChanged.AddListener(UpdateCooldownText);
    }

    private void OnCooldownStart()
    {
        _cooldownSlider.value = _cooldownSlider.maxValue;
        _cooldownText.text = _cooldownSlider.maxValue.ToString();
        _cooldownSlider.gameObject.SetActive(true);
        _cooldownText.gameObject.SetActive(true);
        _button.interactable = false;
        _isOnCooldown = true;
    }

    private void OnCooldownEnd()
    {       
        _cooldownSlider.gameObject.SetActive(false);
        _cooldownText.gameObject.SetActive(false);      
        _button.interactable = true;
        _isOnCooldown = false;
    }

    private void UpdateCooldownText(float currenttime)
    {
        currenttime = Mathf.CeilToInt(_cooldownSlider.value);
        _cooldownText.text = currenttime.ToString();
    }

    private void OnButtonPressed()
    {
        _ability.OnAbilityActivated();

        if (!_isOnCooldown)
        {
            StartCooldown();
        }
    }
}
