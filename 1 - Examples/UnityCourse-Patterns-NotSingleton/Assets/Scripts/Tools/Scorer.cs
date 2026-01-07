
using UnityEngine;

public class Scorer : MonoBehaviour
{

    public int ScoreIfClicked = 1;
    
    private GameManager _gameManager;
    
    private void Start()
    {
        _gameManager = FindAnyObjectByType<GameManager>();
    }
    
    public void AddScore()
    {
        Debug.Log($"Score added {@ScoreIfClicked}");
        _gameManager.Score += ScoreIfClicked;
    }
    
}
