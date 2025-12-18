using Singletons;
using TMPro;
using UnityEngine;

public class ScoreReader : MonoBehaviour
{

    [SerializeField] private TMP_Text _text;
    
    // Update is called once per frame
    void Update()
    {
        //if (_text) _text.text = ScoreKeeper.Instance().Score.ToString();
        //if (_text) _text.text = ScoreKeeperAsComponent.Instance.Score.ToString();
        if (_text) _text.text = ScoreKeeperGenerized.Instance.Score.ToString();
    }
}
