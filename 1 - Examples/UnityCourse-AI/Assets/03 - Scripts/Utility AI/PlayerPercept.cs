using UnityEngine;

public class PlayerPercept : AIPercept
{
    public override Transform Transform => transform;
    public override bool Available => this.gameObject.activeInHierarchy;
}
