using System;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class SatyrCharacter : MonoBehaviour
{

    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    private Animator _animator;

    private float _moveInput;
    private bool _jumpInput;
    private bool _dashInput;
    
    [SerializeField] private float _xSpeed = 10;
    [SerializeField] private float _xForce = 20;
    [SerializeField] private float _jumpForce = 10;

    [SerializeField] private Detector _groundDetector;
    [SerializeField] private Detector _leftDetector;
    [SerializeField] private Detector _rightDetector;

    [SerializeField] private float _hitForce;
    [SerializeField] private float _hitTime = 2.0f;

    public bool _isHit = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!_isHit)
        {
            float targetSpeed = _moveInput * _xSpeed;
            float diffSpeed = (targetSpeed - _rb.linearVelocityX) / _xSpeed;
            
            Vector2 force = Vector2.right * (_xForce * diffSpeed);
            Debug.DrawRay(transform.position, force, Color.yellow);
            
            _rb.AddForce(force);
            
            if(_groundDetector.Touched && _jumpInput)
            {
                _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Force);
            }
            
        }
        
        _rb.linearVelocityX = Mathf.Min(_rb.linearVelocityX, _xSpeed);
        
    }

    private void Update()
    {
        
        if (_rb.linearVelocityX < 0) _sr.flipX = true;
        if (_rb.linearVelocityX > 0) _sr.flipX = false;

        _animator.SetBool("IsHit", _isHit);
        _animator.SetBool("IsRunning", Mathf.Abs(_rb.linearVelocityX) >= 0.1f);
        _animator.SetBool("IsJumping", _rb.linearVelocityY >= 0.01f);
        _animator.SetBool("IsFalling", _rb.linearVelocityY <= -0.01f);
        
        Debug.DrawRay(transform.position, _rb.linearVelocity, Color.blue);
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit what ?!" + other.gameObject.name);
        if (other.gameObject.CompareTag("Hazard"))
        {
            if(!_isHit){
                _isHit = true;
                StopCoroutine("ResetHit_co");
                StartCoroutine("ResetHit_co");
            }
        }
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        _moveInput = ctx.ReadValue<float>();
    }
    
    public void OnJump(InputAction.CallbackContext ctx)
    {
        _jumpInput = ctx.performed;
    }

    IEnumerator ResetHit_co()
    {
        Vector2 hitForce = _rb.linearVelocity.normalized * (-1.0f * _hitForce);
        Debug.DrawRay(transform.position, hitForce, Color.magenta, 0.5f);

        _rb.linearVelocity = Vector2.zero;
        _rb.AddForce(hitForce, ForceMode2D.Impulse);
        _rb.linearDamping = 2.0f;
        
        yield return new WaitForSeconds(_hitTime);
        _isHit = false;
        _rb.linearDamping = 0.13f;
        
    }
}
