using System;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Clicker : MonoBehaviour
{

    [SerializeField] private LayerMask _interactionLayers;
    [SerializeField] private float _detectionDepth = 1000;
    [SerializeField] private Transform _pointer;

    [SerializeField] private UnityEvent<Vector3> _onLeftPressed;
    [SerializeField] private UnityEvent<Vector3> _onLeftReleased;
    [SerializeField] private UnityEvent<Vector3> _onRightPressed;
    [SerializeField] private UnityEvent<Vector3> _onRightReleased;
    [SerializeField] private UnityEvent<Vector3> _onMouseMove;

    [SerializeField] private Material _clickedMaterial;
    [SerializeField] private Material _hoverMaterial;

    private Material _startPointerMaterial;
    private Camera _mainCamera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _startPointerMaterial = _pointer.gameObject.GetComponent<MeshRenderer>().sharedMaterial;
        _mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {

        Mouse mouse = Mouse.current;

        Ray ray = _mainCamera.ScreenPointToRay(mouse.position.value);
        Debug.DrawRay(ray.origin, ray.direction * _detectionDepth, Color.red);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _interactionLayers))
        {
            _pointer.gameObject.SetActive(true);
            
            if (mouse.delta.value.sqrMagnitude >= 0)
            {
                _pointer.position = hit.point;
                _onMouseMove?.Invoke(hit.point);
            }

            if (mouse.leftButton.wasPressedThisFrame)
            {
                SetPointerMaterial(_clickedMaterial);
                _onLeftPressed?.Invoke(hit.point);
                if(hit.collider.TryGetComponent(out IInteractable interactable)) interactable.OnLeftClick(); 
            }
            if (mouse.leftButton.wasReleasedThisFrame)
            {
                SetPointerMaterial(_startPointerMaterial);
                _onLeftReleased?.Invoke(hit.point);
            }
            
            
            if (mouse.rightButton.wasPressedThisFrame)
            {
                SetPointerMaterial(_clickedMaterial);
                _onRightPressed?.Invoke(hit.point);
            }
            if (mouse.rightButton.wasReleasedThisFrame)
            {
                SetPointerMaterial(_startPointerMaterial);
                _onRightReleased?.Invoke(hit.point);
                if(hit.collider.TryGetComponent(out IInteractable interactable)) interactable.OnRightClick(); 
            }

        }
        else
        {
            _pointer.gameObject.SetActive(false);
        }
    }
    private void SetPointerMaterial(Material newMaterial)
    {

        if (_pointer.TryGetComponent(out Renderer pointerRenderer))
        {
            pointerRenderer.sharedMaterial = newMaterial;
        }
    }

    
}
