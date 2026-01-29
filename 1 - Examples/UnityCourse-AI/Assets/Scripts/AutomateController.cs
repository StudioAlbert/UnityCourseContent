using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AutomateController : MonoBehaviour
{

    private NavMeshAgent _agent;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDestination(Vector3 destination)
    {
        if (NavMesh.SamplePosition(destination, out NavMeshHit hit, 1f, NavMesh.AllAreas))
        {
            _agent.SetDestination(hit.position);    
        }
    }
}
