using UnityEngine;

[CreateAssetMenu(menuName = "Dungeon/Loot", fileName = "loot")]
public class LootData : ScriptableObject
{
    [SerializeField] private int _atk;
    [SerializeField] private float _weight;
}
