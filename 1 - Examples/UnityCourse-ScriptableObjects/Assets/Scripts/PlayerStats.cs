using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    // [SerializeField] private int _health = 50;
    [SerializeField] private SO_IntValue _health;
    [SerializeField] private int _gold = 0;
    [SerializeField] private int _mana = 0;
    
    public void Heal(int value) => _health.Value += value;
    public void MakeMoney(int value) => _gold += value;
    public void GainMana(int value) => _mana += value;
    
}
