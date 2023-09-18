using DG.Tweening;
using UnityEngine;

public class SpikePlatformController : MonoBehaviour
{
    [SerializeField] private Vector3 moveDirection = Vector3.up;
    [SerializeField] private float moveDistance;
    [SerializeField] private float moveDuration;
    [SerializeField] private Ease moveEase = Ease.OutQuad;
    [SerializeField] private int damageAmount = 10;

    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;
        MovePlatform();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerHealthSystem>(out var player))
        {
            player.TakeDamage(damageAmount);
        }
    }

    private void MovePlatform()
    {
        Vector3 targetPosition = initialPosition + moveDirection * moveDistance;

        transform.DOMove(targetPosition, moveDuration).SetEase(moveEase).OnComplete(OnMoveComplete);
    }

    private void OnMoveComplete()
    {
        moveDirection = -moveDirection;
        MovePlatform();
    }   
}
