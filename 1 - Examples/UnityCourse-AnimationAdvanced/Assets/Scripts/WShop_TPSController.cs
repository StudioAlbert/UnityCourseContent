using DefaultNamespace;
using UnityEngine;

// ReSharper disable once InconsistentNaming
public class WShop_TPSController : MonoBehaviour
{
    [SerializeField] private bool _isStrafing = false;
    [SerializeField] private float _horizontalSpeed = 15;
    [SerializeField] private float _rotationSpeed = 15;
    [SerializeField] private float _jumpHeight = 2;

    [SerializeField] private WShop_GroundDetector _ground;

    private Rigidbody _rb;
    private Animator _animator;
    private WShop_Inputs _inputs;

    private float _gravityMagnitude = Physics.gravity.magnitude;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _inputs = GetComponent<WShop_Inputs>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        // MOVE ---------------------------------
        Vector3 horizontalSpeed;
        Vector3 rotationSpeed = Vector3.zero;

        if (_isStrafing)
        {
            horizontalSpeed = _horizontalSpeed * new Vector3(_inputs.Move.x, 0, _inputs.Move.y);
        }
        else
        {
            horizontalSpeed = (_horizontalSpeed * _inputs.Move.y) * transform.forward;
            rotationSpeed = (_rotationSpeed * _inputs.Move.x) * Vector3.up;
        }

        // JUMP ---------------------------------
        Vector3 verticalSpeed = new Vector3(0, _rb.linearVelocity.y, 0);
        if (_ground.Touched && _inputs.Jump)
        {
            // verticalSpeed.y = _jumpForce;
            verticalSpeed.y = Mathf.Sqrt(_jumpHeight * 2f * _gravityMagnitude);
            _animator.SetBool("IsJumping", true);
        }
        else
        {
            _animator.SetBool("IsJumping", false);
        }

        // ACTION ON RB ---------------------------
        
        // No root motion
        _rb.linearVelocity = horizontalSpeed + verticalSpeed;
        _rb.angularVelocity = rotationSpeed;
        _animator.SetBool("IsFalling", _rb.linearVelocity.y < 0 && !_ground.Touched);
        _animator.SetFloat("Turn", _inputs.Move.x);
        _animator.SetFloat("Speed", _inputs.Move.y);
        

    }
}
