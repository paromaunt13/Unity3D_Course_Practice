using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationSwitch : MonoBehaviour
{
    [SerializeField] private GameObject _activeLocation;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            _activeLocation.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            _activeLocation.SetActive(false);
    }
}
