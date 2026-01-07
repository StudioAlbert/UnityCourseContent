using System;
using TMPro;
using UnityEngine;

public class ScoreReader : MonoBehaviour
{

    [SerializeField] private TMP_Text _text;

    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = FindAnyObjectByType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_text && _gameManager) _text.text = _gameManager.Score.ToString();
    }
}
