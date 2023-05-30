using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Gem"))
            EventsControl.OnGemPicked?.Invoke(collider);
        if (collider.CompareTag("FinishZone"))
            EventsControl.OnLevelCompleted?.Invoke();       
    }
}
