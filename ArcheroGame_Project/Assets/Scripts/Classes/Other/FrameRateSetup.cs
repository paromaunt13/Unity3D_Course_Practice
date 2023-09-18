using TMPro;
using UnityEngine;

public class FrameRateSetup : MonoBehaviour
{
    [SerializeField] private TMP_Text _fpsText;
    private float deltaTime = 0.0f;

#if PLATFORM_ANDROID
    void Awake()
    {
        Application.targetFrameRate = 90;
        QualitySettings.vSyncCount = 0;
    }
#endif

    private void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        string text = string.Format("{0:0.} fps", fps);
        _fpsText.text = text;
    }
}
