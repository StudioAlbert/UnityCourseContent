using UnityEngine;
using UnityEngine.InputSystem;

public class TankController : MonoBehaviour
{
    [SerializeField] private float _fwdSpeed = 10f;
    [SerializeField] private float _rotateSpeed = 10f;
    
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _bulletSpawnPoint;
    
    private float _moveInput = 0;
    private float _rotateInput = 0;
    
    private Rigidbody _rb;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //transform.Translate(Vector3.forward * (_moveInput * _fwdSpeed * Time.deltaTime));
        //transform.Rotate(Vector3.up * (_rotateInput * _rotateSpeed * Time.deltaTime));
        //
        
        Vector3 velocity = _moveInput * _fwdSpeed * transform.forward; 
        _rb.linearVelocity = new Vector3(velocity.x, _rb.linearVelocity.y, velocity.z);
        _rb.angularVelocity = _rotateInput * Mathf.Deg2Rad * _rotateSpeed * transform.up;
        
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

    public void DoShooting()
    {
        Instantiate(_bulletPrefab, _bulletSpawnPoint.position, _bulletSpawnPoint.rotation);
    }
}
