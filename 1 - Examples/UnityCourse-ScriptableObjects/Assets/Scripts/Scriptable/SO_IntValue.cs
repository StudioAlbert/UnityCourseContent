using UnityEngine;

[CreateAssetMenu(fileName = "IntValue", menuName = "Dungeon Datas/Values/Int Value")]
public class SO_IntValue : ScriptableObject
{
    [SerializeField] private int _value;
    public int Value
    {
        get => _value;
        set => _value = value;
    }
}
