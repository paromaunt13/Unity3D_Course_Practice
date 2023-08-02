using UnityEngine;
using UnityEngine.UIElements;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _rotationSpeed = 10f;

    private Joystick _joystick;
    private Animator _animator;

    private Rigidbody _rigidBody;

    private void Start()
    {
        _joystick = FindObjectOfType<Joystick>();       
        _rigidBody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        float horizontalInput = _joystick.Horizontal;
        float verticalInput = _joystick.Vertical;

        Vector3 movementDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        if (horizontalInput != 0 && verticalInput!= 0)
        {
            _animator.SetBool("IsRunning", true);
        }
        else
        {
            _animator.SetBool("IsRunning", false);
        }
        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, _rotationSpeed * Time.deltaTime);
        }

        Vector3 movement = movementDirection * _moveSpeed * Time.deltaTime;
        _rigidBody.MovePosition(_rigidBody.position + movement);
    }
}
