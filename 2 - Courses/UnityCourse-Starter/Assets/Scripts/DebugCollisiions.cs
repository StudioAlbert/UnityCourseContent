using System;
using UnityEngine;

public class DebugCollisions : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("TRIGGER : " + other.gameObject.name + " est entré en collision avec " + gameObject.name);
    }
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("COLLISION : " + other.gameObject.name + " est entré en collision avec " + gameObject.name);
    }
}
