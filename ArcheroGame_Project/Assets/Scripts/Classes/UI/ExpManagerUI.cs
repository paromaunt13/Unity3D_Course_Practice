using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExpManagerUI : MonoBehaviour
{
    [SerializeField] private Slider _expSlider;
    [SerializeField] private TMP_Text _levelText;

    private PlayerExpSystem _playerExp;

    private void Start()
    {
        _playerExp = FindObjectOfType<PlayerExpSystem>();
        _expSlider.value = _playerExp.CurrentExp;
        _expSlider.maxValue = _playerExp.LevelExp;
        _levelText.text = $"{_playerExp.CurrentLevel}";
        PlayerExpSystem.OnExpGained += ExpBarUpdate;
        PlayerExpSystem.OnLevelUp += LevelUp;
        PlayerExpSystem.OnMaxLevelUp += MaxLevelUp;
    }

    private void LevelUp()
    {
        _levelText.text = $"{_playerExp.CurrentLevel}";
    }

    private void OnDisable()
    {
        PlayerExpSystem.OnExpGained -= ExpBarUpdate;
        PlayerExpSystem.OnMaxLevelUp -= MaxLevelUp;
    }

    private void ExpBarUpdate(int enemyExp, bool canRecieveExp)
    {
        if (canRecieveExp)
        {
            _expSlider.value += enemyExp;
            if (_expSlider.value >= _expSlider.maxValue)
            {
                _expSlider.value = _expSlider.minValue;
                LevelUp();
            }
        }
        else 
        {
            return;
        }
    }

    private void MaxLevelUp()
    {
        _expSlider.gameObject.SetActive(false);
    }
}
