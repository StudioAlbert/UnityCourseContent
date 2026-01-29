using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInputs))]
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{

    [SerializeField] private float _horizontalSpeed = 5;
    [SerializeField] private float _jumpHeight = 10;
    [SerializeField] private float _fallFactor = 2;
    [SerializeField] private float _maxVerticalVelocity = 42f;
    [SerializeField] private Detector _gndDetector;

    [SerializeField] private float _rotationDelay = 0.05f;
    [SerializeField] private float _fallingThreshold = 0.1f;

    private PlayerInputs _inputs;
    private CharacterController _controller;
    private Camera _mainCamera;
    private Animator _animator;

    private Vector3 _verticalVelocity;
    private bool _landingDone = true;

    public Vector3 VerticalVelocity => _verticalVelocity;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _inputs = GetComponent<PlayerInputs>();
        _controller = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        float moveMagnitude = _inputs.Move.magnitude;
        Vector3 horizontalVelocity;

        if (_landingDone)
        {
            //horizontalVelocity = transform.forward * (moveMagnitude * _horizontalSpeed);
            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, Mathf.Infinity))
            {
                Debug.Log("Touched surface : " + hit.collider.gameObject.name);
                Debug.DrawRay(hit.point, hit.normal, Color.red);

                Vector3 desiredForward = Vector3.Cross(hit.normal, -1 * transform.right);
                Debug.DrawRay(hit.point, desiredForward, Color.blue);

                horizontalVelocity = desiredForward.normalized * (moveMagnitude * _horizontalSpeed);
            }
            else
            {
                horizontalVelocity = Vector3.zero;
            }

        }
        else
            horizontalVelocity = Vector3.zero;

        if (_gndDetector.Detected)
        {
            _verticalVelocity = Physics.gravity * (_fallFactor * Time.deltaTime);
            if (_inputs.Jump && _landingDone)
            {
                _verticalVelocity = Vector3.up * Mathf.Sqrt(_jumpHeight * -2f * Physics.gravity.y);
            }
        }
        else
        {
            _inputs.Jump = false;
        }

        // Is Not Grounded : Jumping, Falling
        if (_verticalVelocity.magnitude < _maxVerticalVelocity)
        {
            if (_controller.velocity.y > 0)
                _verticalVelocity += Physics.gravity * Time.deltaTime;
            else
                _verticalVelocity += Physics.gravity * (_fallFactor * Time.deltaTime);
        }


        Quaternion inputRotation = Quaternion.LookRotation(new Vector3(_inputs.Move.x, 0, _inputs.Move.y), Vector3.up);
        Quaternion cameraRotation = _mainCamera.transform.rotation;
        Quaternion rotation = Quaternion.Euler(0, cameraRotation.eulerAngles.y, 0) * inputRotation;

        _controller.Move(horizontalVelocity * Time.deltaTime + _verticalVelocity * Time.deltaTime);

        if (horizontalVelocity.sqrMagnitude > 0.001f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, _rotationDelay);
        }

        _animator.SetFloat("velocity", moveMagnitude);
        _animator.SetBool("IsFalling", !_gndDetector.Detected && _verticalVelocity.y < (-1.0f * _fallingThreshold));
        _animator.SetBool("IsJumping", !_gndDetector.Detected);

    }

    private void OnLandingBegin() => _landingDone = false;
    private void OnLandingEnd() => _landingDone = true;

}
