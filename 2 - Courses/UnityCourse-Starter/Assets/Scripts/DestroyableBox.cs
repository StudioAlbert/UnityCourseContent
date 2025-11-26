using System;
using UnityEngine;

public class DestroyableBox : MonoBehaviour
{

    [SerializeField] private float _capForce;
    [SerializeField] private float _boxForce;

    [SerializeField] private Rigidbody _capRb;
    [SerializeField] private Rigidbody _boxRb;

   private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Bullet") || other.gameObject.CompareTag("Tank"))
        {
            Debug.Log("Box touched !");
            _capRb.AddForce(Vector3.up * _capForce);
            _boxRb.AddForce((other.transform.position - transform.position) * _boxForce);

            Collider myCollider = GetComponent<Collider>();
            Destroy(myCollider);
        }
    }
}
