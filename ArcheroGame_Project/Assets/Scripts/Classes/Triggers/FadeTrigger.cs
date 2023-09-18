using DG.Tweening;
using UnityEngine;

public class FadeTrigger : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;

    private void Start()
    {
        _canvasGroup.alpha = 1f;
        _canvasGroup.DOFade(0f, 1.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerData>())
        {
            _canvasGroup.DOFade(1f, 0.4f).OnComplete(FadeOut);
        }            
    }

    private void FadeOut()
    {
        _canvasGroup.DOFade(0f, 0.4f);
    }
}
