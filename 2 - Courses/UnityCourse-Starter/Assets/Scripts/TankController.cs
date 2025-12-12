using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class TankController : MonoBehaviour
{
    [SerializeField] private float _fwdSpeed = 10f;
    [SerializeField] private float _rotateSpeed = 10f;
    
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _bulletSpawnPoint;

    [SerializeField] private ParticleSystem _flash;

    [SerializeField] private AnimationCurve _smokeRatio;
    [SerializeField] private List<ParticleSystem> _smokes;
    
    private float _moveInput = 0;
    private float _rotateInput = 0;
    
    private Rigidbody _rb;
    private Animator _animator;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector3.forward * (_moveInput * _fwdSpeed * Time.deltaTime));
        //transform.Rotate(Vector3.up * (_rotateInput * _rotateSpeed * Time.deltaTime));
        //
        
        Vector3 velocity = _moveInput * _fwdSpeed * transform.forward; 
        _rb.linearVelocity = new Vector3(velocity.x, _rb.linearVelocity.y, velocity.z);
        _rb.angularVelocity = _rotateInput * Mathf.Deg2Rad * _rotateSpeed * transform.up;
        
        _animator.SetFloat("Velocity", _rb.linearVelocity.magnitude);
        _animator.SetFloat("ForwardBackward", _rb.linearVelocity.z);

        foreach (ParticleSystem smoke in _smokes)
        {
            ParticleSystem.EmissionModule emission = smoke.emission;
            emission.rateOverTime = _smokeRatio.Evaluate(_rb.linearVelocity.magnitude);
        }
        
    }

    public void OnMoveForward(InputAction.CallbackContext ctx)
    {
        Debug.Log("I'm moving forward : " + ctx.ReadValue<float>());
        _moveInput = ctx.ReadValue<float>();
    }
    public void OnRotate(InputAction.CallbackContext ctx)
    {
        Debug.Log("I'm rotating : " + ctx.ReadValue<float>());
        _rotateInput = ctx.ReadValue<float>();
    }

    public void DoShooting(InputAction.CallbackContext ctx)
    {
        Debug.Log("Do shooting");
        if(ctx.performed)
        {
            Instantiate(_bulletPrefab, _bulletSpawnPoint.position, _bulletSpawnPoint.rotation);
            _flash.Play();
        }
        
    }
}
