using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private float _healthMax;
    [SerializeField] private FloatValue _health;
    [SerializeField] private FloatValue _mana;
    [SerializeField] private FloatValue _gold;

    [SerializeField] private SORuntimeSet _inventory;
    
    // Start is called before the first frame update
    void Start()
    {
        _health.Value = _healthMax;
        _inventory.Clear();
    }

    // Update is called once per frame
    public void GetDamage(float damage)
    {
        _health.Value -= damage;
    }
    public void Heal(float healPower)
    {
        _health.Value += healPower;
    }

    public bool Pickup(LootData lootData)
    {
        return _inventory.Add(lootData);
    }

}
