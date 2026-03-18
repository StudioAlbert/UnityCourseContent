using UnityEngine;

public abstract class AIPercept : MonoBehaviour
{
    
    public abstract Transform Position { get; }
    public abstract bool Available { get; }
    
}

public class CryptPercept : AIPercept
{
    // Actually on the object
    [SerializeField] private Transform _door;

    // Percept exposition for AIs
    public override Transform Position => _door.transform;
    public override bool Available => true;
}