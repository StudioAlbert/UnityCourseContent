using PCG;
using UnityEngine;

public class BoundsCentering : MonoBehaviour
{
    [SerializeField] private Vector3Int _size = new Vector3Int(10, 10, 10);
    [SerializeField] private Vector3 _center = new Vector3(0,0,0);
    
    private BoundsInt _bounds;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _bounds = new BoundsInt { size = _size };
        _bounds.position = new Vector3Int(2, 2, 2);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log($"1 Bounds min :  {_bounds.min}  max : {_bounds.max}");
        _bounds.SetCenter(_center);
        Debug.Log($"3 Bounds min :  {_bounds.min}  max : {_bounds.max}");
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(_bounds.center, _bounds.size);
    }
}
