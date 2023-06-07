using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsWindowController : MonoBehaviour
{   
    [SerializeField] TMP_Text _headerText;    
    [SerializeField] TMP_Text _resolutionText;
    [SerializeField] TMP_Text _languageText;
    [SerializeField] TMP_Text _fpsValueText;
    [SerializeField] TMP_Text _volumeText;
    [SerializeField] TMP_Text _saveButtonText;

    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _saveSettingsButton;

    [SerializeField] private TMP_Dropdown _resolutionDropdown;
    [SerializeField] private TMP_Dropdown _languageDropdown;

    [SerializeField] private Slider _volumeSlider;

    [SerializeField] private ToggleGroup _fpsToggles;

    private void Awake()
    {
        ResolutionDropdownSetValues();
        LanguageDropdownSetValues();
        FPSToggleAddListener();

        _closeButton.onClick.AddListener(OnCloseButtonClick);
        _saveSettingsButton.onClick.AddListener(OnSaveSettingsButtonClick);

        _resolutionDropdown.onValueChanged.AddListener(OnResolutionDropdowValueChanged);
        _languageDropdown.onValueChanged.AddListener(OnLanguageDropdowValueChanged);

        _volumeSlider.onValueChanged.AddListener(OnVolumeSliderValueChanged);
    }
    
    void OnCloseButtonClick()
    {
        Debug.Log("Окно настроек закрыто!");
        gameObject.SetActive(false);
    }

    void OnSaveSettingsButtonClick() 
    {
        Debug.Log("Настройки успешно сохранены!");
    }
    
    void OnVolumeSliderValueChanged(float value)
    {
        Debug.Log($"Уровень громкости: {value}");
    }

    void OnResolutionDropdowValueChanged(int index)
    {       
        Debug.Log($"Выбранное разрешение: {_resolutionDropdown.options[index].text}");
    }

    void OnLanguageDropdowValueChanged(int index)
    {
        TranslateText(index);
        Debug.Log($"Выбранный язык: {_languageDropdown.options[index].text}");
    }

    void OnFPSToggleValueChanged(Toggle toggle)
    {
        Debug.Log($"Выбранная величина FPS: {toggle.GetComponentInChildren<Text>().text}");
    }

    void ResolutionDropdownSetValues()
    {
        List<string> resolutions = new() 
        {
            "1920×1080",
            "1024×768",
            "800×600"
        };
        _resolutionDropdown.ClearOptions();
        _resolutionDropdown.AddOptions(resolutions);
    }

    void LanguageDropdownSetValues()
    {
        List<string> languages = new()
        {
            "English",
            "Українська",
            "Русский"
        };
        _languageDropdown.ClearOptions();
        _languageDropdown.AddOptions(languages);
    }

    void FPSToggleAddListener()
    {
        var toggles = GetComponentsInChildren<Toggle>();
        foreach (var t in toggles)
        {
            var toggle = t;
            t.onValueChanged.AddListener(on => { if (on) OnFPSToggleValueChanged(toggle); });
        }
    }

    void TranslateText(int languageIndex)
    {
        switch (languageIndex)
        {
            case 0:
                _headerText.text = "Settings";
                _resolutionText.text = "Screen resolution";
                _languageText.text = "Language";
                _fpsValueText.text = "FPS value";
                _volumeText.text = "Volume level";
                _saveButtonText.text = "Save settings";
                break;
            case 1:
                _headerText.text = "Налаштування";
                _resolutionText.text = "Роздільна здатність екрану";
                _languageText.text = "Мова";
                _fpsValueText.text = "Величина FPS";
                _volumeText.text = "Гучність звуку";
                _saveButtonText.text = "Зберегти налаштування";
                break;
            case 2:
                _headerText.text = "Настройки";
                _resolutionText.text = "Разрешение экрана";
                _languageText.text = "Язык";
                _fpsValueText.text = "Величина FPS";
                _volumeText.text = "Громкость звука";
                _saveButtonText.text = "Сохранить настройки";
                break;
        }
    }
}
