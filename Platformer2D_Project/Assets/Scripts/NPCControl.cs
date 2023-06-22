using UnityEngine;

public class NPCControl : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            _animator.SetTrigger("Greeting");
    } 
}

