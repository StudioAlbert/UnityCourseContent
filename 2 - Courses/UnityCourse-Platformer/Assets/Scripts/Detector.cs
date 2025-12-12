using System;
using TMPro;
using UnityEngine;

public class Detector : MonoBehaviour
{
    
    public bool Touched = false;
    
    [SerializeField] private Vector2 _size = new Vector2(0.5f, 0.5f);
    [SerializeField] private LayerMask _mask;

    private void Update()
    {
        Touched = Physics2D.OverlapBox(transform.position, _size, 0, _mask);
    }

    private void OnDrawGizmos()
    {
        if(Touched)
            Gizmos.color = Color.green;
        else
            Gizmos.color = Color.yellow;

        Gizmos.DrawWireCube(transform.position,new Vector3(_size.x, _size.y, 0));
        
    }

}
