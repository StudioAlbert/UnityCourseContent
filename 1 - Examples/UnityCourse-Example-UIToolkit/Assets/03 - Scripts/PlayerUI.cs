using UnityEngine;
using UnityEngine.UIElements;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private UIDocument _uiDocument;
    [SerializeField] private PlayerData _datas;

    private VisualElement _healthBar;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        _datas.Reset();
        if (_uiDocument == null)
        {
            Debug.Log("No UI Document valid");
        }
        else
        {
            _healthBar = _uiDocument.rootVisualElement.Q<VisualElement>("Bar");
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        if (_healthBar == null) return;
        _healthBar.style.width = new Length(100 * _datas.CurrentHp / _datas.MaxHp, LengthUnit.Percent);
        _healthBar.style.backgroundColor = Color.Lerp(Color.brown, Color.darkOliveGreen, _datas.CurrentHp / _datas.MaxHp);
    }
}

