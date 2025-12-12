using UnityEngine;
using UnityEngine.Events;

public class DamageTaker : MonoBehaviour
{

    [SerializeField] private float _hpMax;
    [SerializeField] private UnityEvent _onDeath;
    [SerializeField] private bool _destroyable = true;
    
    public float _hp;
    
    public float Hp => _hp;
    public float HpMax => _hpMax;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _hp = _hpMax;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamages(float damages)
    {
        
        _hp -= damages;
        if (_hp <= 0)
        {
            _onDeath.Invoke();
            if (_destroyable)
            {
                Destroy(gameObject);
            }
            else
            {
                enabled = false;
            }
        }
    }
    
}
