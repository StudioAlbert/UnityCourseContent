using System.Linq;
using UnityEngine;

public class AISensor : MonoBehaviour
{
    
    [SerializeField] private float _radius;
    [SerializeField] private LayerMask _layerMask;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    bool IsPerceptAndAvailable(Collider collider)
    {
        if (collider.TryGetComponent(out AIPercept percept))
        {
            return percept.Available;
        }
        
        return false;
        
    }
    
    // Update is called once per frame
    void Update()
    {
        Collider[] objects = Physics.OverlapSphere(this.transform.position, this._radius, _layerMask, QueryTriggerInteraction.Ignore);


        foreach (Collider c in objects)
        {
            
        }
        
        var nearestTarget = objects
            .Where(o => IsPerceptAndAvailable(o))
            .OrderBy(o => Vector3.Distance(o.transform.position, this.transform.position))
            .First();
        
        Debug.Log($"nearestTarget = {nearestTarget.gameObject.name}");

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position, this._radius);
    }
    
}
