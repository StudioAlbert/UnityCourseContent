using System;
using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private LayerMask _targetLayer;
    [SerializeField] private float _radius = 2;
    [SerializeField] private float _distance = 2;

    private bool _targetDetected;
    private Coroutine _attackCoroutine;

    private int combienDeCoroutine = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _targetDetected = Physics.CheckSphere(transform.position + transform.forward * _distance, _radius, _targetLayer);

        if (_targetDetected)
        {
            if(_attackCoroutine == null) _attackCoroutine = StartCoroutine(AttackCO());
        }
        else
        {
            StopCoroutine(_attackCoroutine);
            _attackCoroutine = null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = _targetDetected ? Color.blue : Color.black;
        Gizmos.DrawWireSphere(transform.position + transform.forward * _distance, _radius);
    }


    IEnumerator AttackCO()
    {
        while(true)
        {
            Debug.Log("Attack");
            yield return new WaitForSeconds(1);
        }
    }
}
