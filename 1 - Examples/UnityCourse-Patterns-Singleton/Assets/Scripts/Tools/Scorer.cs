using Singletons;
using UnityEngine;

public class Scorer : MonoBehaviour
{

    public int ScoreIfClicked = 1;
    
    public void AddScore()
    {
        Debug.Log($"Score added {@ScoreIfClicked}");
        ScoreKeeperGenerized.Instance.AddScore(ScoreIfClicked);
        // ScoreKeeperAsComponent.Instance.AddScore(ScoreIfClicked);
    }
    
}
