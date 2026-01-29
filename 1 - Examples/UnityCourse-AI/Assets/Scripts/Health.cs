using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _hpMax = 100;
    [SerializeField] private FullLife _fullLife;

    public float _hp;
    public float Hp => _hp;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _hp = _hpMax;
    }

    public void Heal(float healAmount)
    {
        _hp += healAmount;
        if (_hp > _hpMax)
        {
            _hp = _hpMax;
            // Event Full Life
            CustomEvent.Trigger(gameObject, _fullLife.name, 100);
        }
        
    }
}
