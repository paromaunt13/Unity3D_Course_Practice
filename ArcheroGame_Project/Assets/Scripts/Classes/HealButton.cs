using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealButton : MonoBehaviour
{
    [SerializeField] private float cooldownTime;
    [SerializeField] private Image cooldownFillImage;

    private Button _healButton;

    private bool isOnCooldown = false;  
    
    private float cooldownTimer = 0f;
    private int _healAmount = 150;

    private void Start()
    {
        cooldownFillImage.fillAmount = 1f;
        _healButton = GetComponent<Button>();
    }

    private void Update()
    {
        if (isOnCooldown)
        {
            cooldownTimer -= Time.deltaTime;
            float fillAmount = 1f - (cooldownTimer / cooldownTime);
            cooldownFillImage.fillAmount = Mathf.Clamp01(fillAmount);
            _healButton.interactable = false;
            if (cooldownTimer <= 0f)
            {
                isOnCooldown = false;
                _healButton.interactable = true;
                cooldownFillImage.fillAmount = 1f;
            }
        }
    }

    public void Heal()
    {
        FindObjectOfType<Player>().Heal(_healAmount);
        cooldownFillImage.fillAmount = 0f;
        if (!isOnCooldown)
        {
            isOnCooldown = true;            
            cooldownTimer = cooldownTime;
        }
    }
}
