using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeControl : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;

    private void Start()
    {
        _canvasGroup.alpha = 0f;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            _canvasGroup.DOFade(1f, 0.4f).OnComplete(FadeOut);
        }            
    }

    void FadeOut()
    {
        _canvasGroup.DOFade(0f, 0.4f);
    }
}
