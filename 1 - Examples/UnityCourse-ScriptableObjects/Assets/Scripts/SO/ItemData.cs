using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dungeon/Item", fileName = "item")]
public class ItemData : ScriptableObject
{
    
    [SerializeField] private float _moneyMin;
    [SerializeField] private float _moneyMax;
    [SerializeField] private float _manaGain;
    [SerializeField] private float _healthGain;

    public float ManaGain => _manaGain;
    public float HealthGain => _healthGain;
    public float MoneyGain()
    {
        return Random.Range(_moneyMin, _moneyMax);
    }

}
