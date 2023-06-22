using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private float _speed = 3f;
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private AudioSource _grassSound;

    private float _checkRadius = 0.5f;
    private bool onGround;
       
    private float _horizontal;   
    private bool _isFacingRight = true;

    void Update()
    {
        _horizontal = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            _animator.SetBool("isRunning", true);
        else _animator.SetBool("isRunning", false);

        if (Input.GetKeyDown(KeyCode.Space) && onGround)
            Jump();
        CheckGround();
        Flip();       
    }

    private void FixedUpdate()
    {
        _rigidbody2D.velocity = new Vector2(_horizontal * _speed, _rigidbody2D.velocity.y);
    }

    void Jump()
    {
        _animator.SetTrigger("Jump");
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpForce);       
    }

    void Flip()
    {
        if (_isFacingRight && _horizontal < 0f || !_isFacingRight && _horizontal > 0f)
        {
            _isFacingRight = !_isFacingRight;
            Vector3 localscale = transform.localScale;
            localscale.x *= -1f;
            transform.localScale = localscale;
        }
    }

    void CheckGround()
    {
        onGround = Physics2D.OverlapCircle(_groundCheck.position, _checkRadius, _groundLayer);
    }
}
