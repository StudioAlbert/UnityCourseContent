
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{

    [SerializeField] private TMP_Text _text;
    [SerializeField] private Image _gaugeFill;
    
    [SerializeField] private TMP_Text _healthPoints;
    [SerializeField] private Image _healthBar;
    [SerializeField] private Gradient _healthGradient;

    [SerializeField] private BoxManager _boxManager;
    [SerializeField] private DamageTaker _tankHealth;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(_boxManager) SetCounter(_boxManager.BoxesCount, _boxManager.MaxBoxes);
        if(_tankHealth) SetHealth(_tankHealth.Hp, _tankHealth.HpMax);
    }

    private void SetCounter(int count, int maxCount)
    {
        _text.text = count.ToString("D2") + " / " + maxCount.ToString("D2");
        _gaugeFill.fillAmount = (float)count / maxCount;
    }

    private void SetHealth(float health, float healthMax)
    {
        _healthPoints.text = Mathf.FloorToInt(health).ToString("D3");

        float healthRatio = health / healthMax;
        _healthBar.fillAmount = (float)health / healthMax;
        _healthBar.color = _healthGradient.Evaluate(healthRatio);
        
    }
}
