using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    [SerializeField] private Slider _loadingSlider;

    public void LoadSceneAsync()
    {
        _loadingSlider.gameObject.SetActive(true);
        LoadScene();
    }

    private void LoadScene()
    {       
        StartCoroutine(LoadSceneAsyncWithProgress());
    }

    private IEnumerator LoadSceneAsyncWithProgress()
    {        
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(1);       
        while (!asyncOperation.isDone)
        {
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            _loadingSlider.value = progress;
            yield return null;
        }
    }
}
