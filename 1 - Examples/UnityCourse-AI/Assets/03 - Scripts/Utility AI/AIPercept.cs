using UnityEngine;

public abstract class AIPercept : MonoBehaviour
{
    public abstract Transform Transform { get; }
    public abstract bool Available { get; }
}