using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableValues/float", fileName = "floatValueName")]
public class FloatValue : ScriptableObject
{
    private float _floatValue;
    public float Value { get => _floatValue; set => _floatValue = value; }
}
