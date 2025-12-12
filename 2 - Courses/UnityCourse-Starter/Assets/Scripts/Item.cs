using System;
using UnityEngine;

public class Item : MonoBehaviour
{

    [SerializeField] private int _goldValue = 1;
    
    private void OnCollisionEnter(Collision other)
    {
        if(other.collider.CompareTag("Tank"))
        {
            Destroy(gameObject);
        }
    }


}
