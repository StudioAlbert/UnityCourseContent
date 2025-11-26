using UnityEngine;

public class Tree : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.TryGetComponent(out Bullet bullet))
        {
            Destroy(gameObject);
        }
        
    }
}
