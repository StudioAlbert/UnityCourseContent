using System;
using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour
{

    [SerializeField] private float _detectionRange;
    [SerializeField] private float _fireTime;
    [SerializeField] private float _waitTime;
    [SerializeField] private float _reloadTime;
    [SerializeField] private float _dps;
    
    [SerializeField] private float _lerpCompensation;

    [SerializeField] private Transform _barrel;
    [SerializeField] private Transform _laserPoint;

    [SerializeField] private LayerMask _layers;

    private LineRenderer _laser;
    private Transform _playerTransform;
    
    private Coroutine _shootSequence;

    private bool _doShoot = false;

    private bool IsPlayerAtRange => _playerTransform && Vector3.Distance(_playerTransform.position, _barrel.position) <= _detectionRange;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerTransform = GameObject.FindWithTag("Tank").transform;
        _laser = GetComponent<LineRenderer>();
        
        _laser.enabled = false;

    }

    private void OnEnable()
    {

    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPlayerAtRange)
        {
            Vector3 playerDirection = _playerTransform.position - _barrel.position;
            _barrel.rotation = Quaternion.Lerp(_barrel.rotation,
                Quaternion.LookRotation(playerDirection),
                _lerpCompensation * Time.deltaTime);

            TryDetectPlayer();

        }
        else
        {
            _laser.enabled = false;

            _barrel.rotation = Quaternion.Lerp(_barrel.rotation,
                Quaternion.LookRotation(Vector3.forward),
                _lerpCompensation * Time.deltaTime);
        }


        if (_laser.enabled)
        {
            Debug.DrawRay(_barrel.position, _barrel.forward * 100, Color.green, 0.25f);

            _laser.SetPosition(0, _laserPoint.position);
            _laser.SetPosition(1, _playerTransform.position);
        }
        else
        {
            Debug.DrawRay(_barrel.position, _barrel.forward * 100, Color.red, 0.25f);
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_barrel.position, _detectionRange);
    }

    private void TryDetectPlayer()
    {

        if (Physics.Raycast(_barrel.position, _barrel.forward, out RaycastHit hit, Mathf.Infinity, _layers)
            && hit.collider.gameObject.TryGetComponent(out DamageTaker damageTaker))
        {
            Debug.Log("Hit something !!!! " + hit.collider.gameObject.name);
            _shootSequence ??= StartCoroutine(ShootSequence_co(3, damageTaker));
        }
        else
        {
            if(_shootSequence != null) StopCoroutine(_shootSequence);
            _shootSequence = null;
            _laser.enabled = false;
        }

    }

    private IEnumerator ShootSequence_co(int nbShots, DamageTaker damageTaker)
    {
        do
        {
            for (int i = 0; i < nbShots; i++)
            {
                _laser.enabled = true;
                if(damageTaker) damageTaker.TakeDamages(_dps * _fireTime);
                yield return new WaitForSeconds(_fireTime);
                
                _laser.enabled = false;
                yield return new WaitForSeconds(_waitTime);
                
            }

            yield return new WaitForSeconds(_reloadTime);

        } while (true);

    }


}
