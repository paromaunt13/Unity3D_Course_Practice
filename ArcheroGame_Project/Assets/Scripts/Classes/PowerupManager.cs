using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    public static PowerupManager Instance;
    [SerializeField] private GameObject _powerUpPanel;
    [SerializeField] private List<GameObject> _powerUps;

    private int _damageIncrease = 15;
    private float _attackSpeed = 0.05f;
    private int _healAmount = 150;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            Heal();
        }
    }
    private void OnEnable()
    {
        Player.OnLevelUp += ShowPowerUp;
    }
    private void OnDisable()
    {
        Player.OnLevelUp -= ShowPowerUp;
    }
    void ShowPowerUp()
    {
        _powerUpPanel.SetActive(true);
    }
    public void IncreaseDamage()
    {
        FindObjectOfType<Player>().CurrentDamage += _damageIncrease;
    }
    public void IncreaseAttackSpeed()
    {
        FindObjectOfType<Player>().AttackSpeed -= _attackSpeed;
    }
    public void Heal()
    {
        FindObjectOfType<Player>().Heal(_healAmount);
    }
}
