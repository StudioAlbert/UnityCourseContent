using System;
using System.Linq;
using UnityEngine;

public class HealingPoint : MonoBehaviour
{

    [SerializeField] private float _radius = 5;
    [SerializeField] private float _healPerSeconds = 1;

    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Collider[] zombies = Physics.OverlapSphere(transform.position, _radius);

        foreach (Collider zombie in zombies.Where(z => z.CompareTag("Zombie")))
        {
            if (zombie.TryGetComponent(out Health zombieHealth))
            {
                zombieHealth.Heal(_healPerSeconds * Time.deltaTime);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
