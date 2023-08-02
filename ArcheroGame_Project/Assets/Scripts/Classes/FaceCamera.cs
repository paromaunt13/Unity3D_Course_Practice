using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    private Camera _mainCamera;
    private void Start()
    {
        _mainCamera = Camera.main;

        Canvas canvas = GetComponent<Canvas>();
        canvas.sortingOrder = 9999;
    }

    private void Update()
    {
        if (_mainCamera == null)
            return;

        transform.LookAt(transform.position + _mainCamera.transform.rotation * Vector3.forward,
            _mainCamera.transform.rotation * Vector3.up);
    }
}
