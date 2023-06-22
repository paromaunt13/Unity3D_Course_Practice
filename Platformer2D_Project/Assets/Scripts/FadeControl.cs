using DG.Tweening;
using UnityEngine;

public class FadeControl : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            _canvasGroup.DOFade(1f, 0.4f);          
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            _canvasGroup.DOFade(0f, 1f);
    }
 
}
