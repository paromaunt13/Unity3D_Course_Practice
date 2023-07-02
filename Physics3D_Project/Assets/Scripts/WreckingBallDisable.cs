using UnityEngine;

public class WreckingBallDisable : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    private Camera _cam;
    private RaycastHit _raycastHit;

    private void Start()
    {
        _cam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray _ray = _cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(_ray, out _raycastHit, 100))
            {
                if (_raycastHit.collider.CompareTag("WreckingBall"))
                {
                    _raycastHit.rigidbody.isKinematic = false;
                }               
            }
        }
    }
}
