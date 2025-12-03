
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{

    [SerializeField] private TMP_Text _text;
    [SerializeField] private Image _gaugeFill;

    [SerializeField] private BoxManager _boxManager;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        SetCounter(_boxManager.BoxesCount, _boxManager.MaxBoxes);
    }

    private void SetCounter(int count, int maxCount)
    {
        _text.text = count.ToString("D2") + " / " + maxCount.ToString("D2");
        _gaugeFill.fillAmount = (float)count / maxCount;
    }
}
