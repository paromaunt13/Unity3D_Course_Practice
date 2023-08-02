using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExpManagerUI : MonoBehaviour
{
    [SerializeField] private Slider _expSlider;
    [SerializeField] private TMP_Text _levelText;
    private int _currentLevel = 1;

    private void Start()
    {
        _expSlider.value = FindObjectOfType<Player>().CurrentExp;
        _expSlider.maxValue = FindObjectOfType<Player>().LevelExp;
        _levelText.text = $"Lv. {_currentLevel}";
        Player.OnExpGained += ExpBarUpdate;
        Player.OnMaxLevelUp += MaxLevelUp;
    }
    private void OnDisable()
    {
        Player.OnExpGained -= ExpBarUpdate;
        Player.OnMaxLevelUp -= MaxLevelUp;
    }
    void ExpBarUpdate(int enemyExp, bool canRecieveExp)
    {
        if (canRecieveExp)
        {
            _expSlider.value += enemyExp;
            if (_expSlider.value >= _expSlider.maxValue)
            {
                LevelUp();
            }
        }
        else 
        {
            return;
        }
    }
    void LevelUp()
    {
        _currentLevel++;
        _expSlider.value = _expSlider.minValue;
        _levelText.text = $"Lv. {_currentLevel}";
    }
    void MaxLevelUp()
    {
        _expSlider.gameObject.SetActive(false);
    }
}
