using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;

    private float _currentMoveSpeed;
    private float _baseMoveSpeed;

    private float _horizontalInput;
    private float _verticalInput;

    private Joystick _joystick;
    private Animator _animator;
    private Rigidbody _rigidBody;

    private Quaternion _currentRotation;

    private bool _isMoving;

    public bool IsMoving => _isMoving;

    public Quaternion CurrentRotation => _currentRotation;

    private void Start()
    {
        _currentMoveSpeed = _moveSpeed;
        _baseMoveSpeed = _moveSpeed;
        _joystick = FindObjectOfType<Joystick>();       
        _rigidBody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        _horizontalInput = _joystick.Horizontal;
        _verticalInput = _joystick.Vertical;

        Vector3 movementDirection = new Vector3(_horizontalInput, 0f, _verticalInput).normalized;

        if (_horizontalInput != 0 && _verticalInput != 0)
        {
            _isMoving = true;
            _animator.SetBool("IsRunning", _isMoving);          
        }
        else
        {
            _isMoving = false;
            _animator.SetBool("IsRunning", _isMoving);
        }
        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, _rotationSpeed * Time.deltaTime);
            _currentRotation = transform.rotation;
        }

        Vector3 movement = _currentMoveSpeed * Time.deltaTime * movementDirection;
        _rigidBody.MovePosition(_rigidBody.position + movement);
    }

    private IEnumerator ApplySlowing(float slowValue, float duration)
    {
        Debug.Log($"Current speed {_currentMoveSpeed}");
        _currentMoveSpeed *= slowValue;
        Debug.Log($"Slowed speed {_currentMoveSpeed}");
        yield return new WaitForSeconds(duration);
        _currentMoveSpeed = _moveSpeed;
        Debug.Log($"Current speed {_currentMoveSpeed}");
    }

    public void ApplySlow(float slowValue, float duration)
    {
        StartCoroutine(ApplySlowing(slowValue, duration));
    }
}
