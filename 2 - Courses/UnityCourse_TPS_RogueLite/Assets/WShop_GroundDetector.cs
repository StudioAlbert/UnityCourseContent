using UnityEngine;

public class WShop_GroundDetector : MonoBehaviour
{
    public bool Touched = false;

    [SerializeField] private float _radius = 0.5f;
    [SerializeField] private LayerMask _mask;

    private void FixedUpdate()
    {
        Touched = Physics.CheckSphere(transform.position, _radius, _mask);
    }

    private void OnDrawGizmos()
    {
        if (Touched)
            Gizmos.color = Color.green;
        else
            Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(transform.position, _radius);

    }

}
