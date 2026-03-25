using UnityEngine;
public class CryptPercept : AIPercept
{
    // Actually on the object
    [SerializeField] private Transform _door;

    // Percept exposition for AIs
    public override Transform Transform => _door.transform;
    public override bool Available => true;
}
