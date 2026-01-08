using UnityEngine;

[CreateAssetMenu(fileName = "NoNameWeapon", menuName = "Dungeon Datas/Weapon", order = 0)]
public class SO_Weapon : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private int _atkStats;
    [SerializeField] private float _weightKg;

    [SerializeField] private Sprite _icon;

    public Sprite Icon => _icon;

}
