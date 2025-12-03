using System;
using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour
{

    [SerializeField] private float _detectionRange;
    [SerializeField] private float _fireTime;
    [SerializeField] private float _waitTime;
    [SerializeField] private float _dps;
    [SerializeField] private Transform _playerPosition;
    [SerializeField] private Transform _barrel;
    [SerializeField] private float _lerpCompensation;

    [SerializeField] private LayerMask _layers;

    private bool _playerDetected = false;
    private bool _doShoot = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(!_playerPosition)
        {
            _playerPosition = GameObject.FindWithTag("Tank").transform;
        }
    }

    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //_barrel.LookAt(_playerPosition);
        
        
        if(_playerPosition != null)
        {
            Vector3 playerDirection = _playerPosition.position - _barrel.position;
            if (playerDirection.magnitude < _detectionRange)
            {
                if (_playerDetected == false)
                {
                    StartCoroutine(ShootSequence_co());
                    _playerDetected = true;
                }
                _barrel.rotation = Quaternion.Lerp(_barrel.rotation,
                    Quaternion.LookRotation(playerDirection),
                    _lerpCompensation * Time.deltaTime);
            }
            else
            {
                StopAllCoroutines();
                _playerDetected = false;
                _barrel.rotation = Quaternion.Lerp(_barrel.rotation,
                    Quaternion.LookRotation(Vector3.forward),
                    _lerpCompensation * Time.deltaTime);
            }

        }
        else
        {
            StopAllCoroutines();
        }
        
        if (_doShoot) DoLaserShoot();
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_barrel.position, _detectionRange);
    }

    private void DoLaserShoot()
    {
        if (Physics.Raycast(_barrel.position, _barrel.forward, out RaycastHit hit, Mathf.Infinity, _layers))
        {
            Debug.Log("Hit something !!!! " + hit.collider.gameObject.name);
            // if(hit.collider.CompareTag("Tank"))
            if(hit.collider.gameObject.TryGetComponent(out DamageTaker damageTaker))
            {
                damageTaker.TakeDamages(_dps * Time.deltaTime);
                Debug.DrawRay(_barrel.position, _barrel.forward * 100, Color.green, 0.25f);
            }
                
        }
        else
        {
            Debug.DrawRay(_barrel.position, _barrel.forward * 100, Color.red, 0.25f);
        }

    }

    private IEnumerator ShootSequence_co()
    {
        do
        {
            _doShoot = true;
            yield return new WaitForSeconds(_fireTime);
            
            _doShoot = false;
            yield return new WaitForSeconds(_waitTime);
            
        } while (true);

    }
    
    
}
