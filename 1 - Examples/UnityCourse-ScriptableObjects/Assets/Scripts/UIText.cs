using TMPro;
using UnityEngine;

public class UIText : MonoBehaviour
{

    [SerializeField] private SO_IntValue _value;
    private TMP_Text _text;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _text = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        _text.text = _value.Value.ToString("0000");
    }
}
