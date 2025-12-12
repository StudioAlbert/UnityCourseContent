using UnityEngine;

public class Tree : MonoBehaviour
{
    public void IsDown()
    {
        if (TryGetComponent(out Rigidbody rb))
        {
            rb.isKinematic = false;
            rb.AddForce(new Vector3(Random.value, 0, Random.value), ForceMode.Impulse);
        }
    }
}
