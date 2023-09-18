using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PowerupManager : MonoBehaviour
{
    [SerializeField] private GameObject _powerUpPanel;
    [SerializeField] private List<PowerUp> _powerUps;
    private List<PowerUpButton> _powerUpButtons;

    private List<PowerUp> _randomPowerUps;

    private void Awake()
    {
        _powerUpPanel.SetActive(true);
        _powerUpButtons = FindObjectsOfType<PowerUpButton>().ToList();
        _powerUpPanel.SetActive(false);
    }

    private void OnEnable()
    {
        PlayerExpSystem.OnLevelUp += OnLevelUp;
    }

    private void OnDisable()
    {
        PlayerExpSystem.OnLevelUp -= OnLevelUp;
    }

    private void OnLevelUp()
    {
        SetPowerUpsButtons();
        _powerUpPanel.SetActive(true);       
    }

    private void SetPowerUpsButtons()
    {
        var randomPowerUps = GetRandomPowerUps();
        for (int i = 0; i < _powerUpButtons.Count; i++)
        {
            _powerUpButtons[i].GetComponentInChildren<PowerUpImage>().GetComponent<Image>().sprite = randomPowerUps[i].Icon;
            _powerUpButtons[i].GetComponent<Button>().GetComponentInChildren<TMP_Text>().text = randomPowerUps[i].Description + $" (+{randomPowerUps[i].Value})";
            if (_powerUpButtons[i].GetComponent<Button>().onClick.GetPersistentEventCount() == 0)
            {
                _powerUpButtons[i].GetComponent<Button>().onClick.AddListener(randomPowerUps[i].ApplyPowerUp);
                _powerUpButtons[i].GetComponent<Button>().onClick.AddListener(ClosePowerUpPanel);
            }
        }
    }

    private List<PowerUp> GetRandomPowerUps()
    {
        _randomPowerUps = new List<PowerUp>();

        while (_randomPowerUps.Count < _powerUpButtons.Count)
        {
            int randomIndex = Random.Range(0, _powerUps.Count);
            var randomPowerUp = _powerUps[randomIndex];
            if (_randomPowerUps.Contains(randomPowerUp))
            {
                continue;
            }
            else
            {
                _randomPowerUps.Add(randomPowerUp);
            }
        }
        return _randomPowerUps;
    }

    private void ClosePowerUpPanel()
    {
        foreach (var item in _powerUpButtons)
        {
            item.GetComponent<Button>().onClick.RemoveAllListeners();
        }
        _powerUpPanel.SetActive(false);
    }
}
