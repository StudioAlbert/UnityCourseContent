using System;
using UnityEngine;
using UnityEngine.AI;

namespace FSM
{
    public class TankContext : MonoBehaviour
    {
        [SerializeField] private GameObject _player;
        
        [Header("Patrol")]
        [SerializeField] private Transform[] _waypoints;
        [SerializeField] private float _patrolDistance = 5;

        [Header("Chase")]
        [SerializeField] private float _sightAngle = 25;
        [SerializeField] private float _sightRange = 15;
        [SerializeField] private float _offSightTime = 5;

        private NavMeshAgent _agent;
        private bool _isPlayerOnSight;
        private float _detectedTime;
        
        public Transform SelfTransform => transform;
        public Transform GetPlayerTransform() => _player.transform;
        public Transform[] Waypoints => _waypoints;
        public float PatrolDistance => _patrolDistance;
        
        
        public void MoveTo(Vector3 position)
        {
            _agent.SetDestination(position);
        }
        public void StopMove()
        {
            _agent.SetDestination(transform.position);
        }

        public bool IsPlayerOnSight() => _isPlayerOnSight;
        
        private void OnEnable()
        {
            //_player = GameObject.FindGameObjectWithTag("Player");
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            if (DetectPlayer())
            {
                _isPlayerOnSight  = true;
                _detectedTime = 0;
            }
            else
            {
                _detectedTime += Time.deltaTime;
                if (_detectedTime > _offSightTime)
                {
                    _isPlayerOnSight = false;
                }
            }
            

        }
        private bool DetectPlayer()
        {

            Vector3 distance = GetPlayerTransform().position - SelfTransform.position;
            float angle = Vector3.Angle(SelfTransform.forward, distance);

            return distance.magnitude <= _sightRange && angle < _sightAngle;
        }
    }
}
