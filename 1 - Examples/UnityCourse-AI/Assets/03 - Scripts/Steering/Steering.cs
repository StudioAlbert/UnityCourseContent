using System;
using UnityEngine;
using UtilityAI_;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
public class Steering : MonoBehaviour
{
    [SerializeField] private float _maxSpeed = 0f;

    [SerializeField][Range(0,1)] private float _seekWeight = 0;
    [SerializeField][Range(0,1)] private float _fleeWeight = 0;
    [SerializeField][Range(0,1)] private float _wanderWeight = 0;
    [SerializeField][Range(0,1)] private float _queueWeight = 0;
    
    [Header("Wander")]
    [SerializeField] private float _wanderDistance = 0;
    [SerializeField] private float _wanderRadius = 0;
    [SerializeField] private float _wanderAngleSpeed = 0;
    [SerializeField] [Range(0, 2 * Mathf.PI)] private float _wanderAngle = 0; // as radians

    [Header("Queue")]
    [SerializeField] private float _aheadDistance = 0;
    [SerializeField] private float _aheadRadius = 0;

    private Rigidbody _rb;
    private Context _context;

    private Vector3 _wanderCenter = Vector3.zero;
    private Vector3 _wanderPoint = Vector3.zero;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _context = GetComponent<Context>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 desiredVelocity = Vector3.zero;

        desiredVelocity += _seekWeight * Seek(_context.GetPlayerTransform());
        desiredVelocity += _fleeWeight * Flee(_context.GetPlayerTransform());
        desiredVelocity += _wanderWeight * Wander();
        desiredVelocity += _queueWeight * Queue();

        Vector3 currentVelocity = _rb.linearVelocity;
        Vector3 steeringForce = desiredVelocity - currentVelocity;

        Vector3 result = currentVelocity + steeringForce * Time.deltaTime;
        Debug.DrawRay(transform.position, currentVelocity, Color.red);
        Debug.DrawRay(transform.position, desiredVelocity, Color.hotPink);
        Debug.DrawRay(transform.position + currentVelocity, steeringForce, Color.springGreen);

        _rb.linearVelocity = Vector3.ClampMagnitude(result, _maxSpeed);
        
    }

    private Vector3 Seek(Transform seekTarget) => seekTarget.position - _context.SelfTransform.position;
    private Vector3 Flee(Transform seekTarget) => _context.SelfTransform.position - seekTarget.position;
    private Vector3 Wander()
    {
        _wanderCenter = _context.SelfTransform.position + _wanderDistance * _context.SelfTransform.forward;
        _wanderAngle += Mathf.Deg2Rad * _wanderAngleSpeed * Random.Range(-1, 1) * Time.deltaTime;
        _wanderPoint = _wanderCenter + _wanderRadius * new Vector3(Mathf.Cos(_wanderAngle), 0, Mathf.Sin(_wanderAngle));
        return _wanderPoint - _context.SelfTransform.position;
    }
    private Vector3 Queue()
    {
        Transform toFollow = GetBestFriend();
        if (toFollow == null)
        {
            return Vector3.zero;
        }
        else
        {
            return toFollow.position - _context.SelfTransform.position;
        }
    }
    
    private Transform GetBestFriend()
    {
        var friends = _context.GetFriends();
        Vector3 aheadCenter = _context.SelfTransform.position + _aheadDistance * _context.SelfTransform.forward;

        float friendBestDistance = float.MaxValue;
        Transform bestFriend = null;
        
        foreach (Transform friend in friends)
        {
            if (Vector3.Distance(friend.position, aheadCenter) < _aheadRadius)
            {
                if (Vector3.Distance(friend.position, _context.SelfTransform.position) < friendBestDistance)
                {
                    friendBestDistance = Vector3.Distance(friend.position, _context.SelfTransform.position);
                    bestFriend = friend;
                }
            }
        }

        return bestFriend;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.burlywood;
        Gizmos.DrawWireSphere(_wanderCenter, _wanderRadius);
        Gizmos.DrawSphere(_wanderPoint, 0.2f);
    }
}
