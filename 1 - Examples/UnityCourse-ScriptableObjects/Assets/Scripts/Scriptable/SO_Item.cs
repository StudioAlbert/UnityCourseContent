using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Dungeon Datas/Item")]
public class SO_Item : ScriptableObject
{
    [SerializeField] private int _mana;
    [SerializeField] private int _health;
    [SerializeField] private int _goldMax;
    [SerializeField] private int _goldMin;

    [SerializeField] private Sprite _lootedSprite;

    public int Mana => _mana;
    public int Health => _health;
    public Sprite LootedSprite => _lootedSprite;

    // Random gold loot
    public int Gold => Random.Range(_goldMin, _goldMax);

}
