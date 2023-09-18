using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCompleteTrigger : MonoBehaviour
{
    public static Action OnGameComplete;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerData>())
        {
            OnGameComplete?.Invoke();
        }
    }
}
