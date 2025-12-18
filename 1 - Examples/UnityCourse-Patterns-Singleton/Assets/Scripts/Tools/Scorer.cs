using Singletons;
using UnityEngine;

public class Scorer : MonoBehaviour
{

    public int ScoreIfClicked = 1;
    
    public void AddScore()
    {
        Debug.Log($"Score added {@ScoreIfClicked}");
        //ScoreKeeper.Instance().Score += ScoreIfClicked;
        //ScoreKeeperAsComponent.Instance.AddScore(ScoreIfClicked);
        ScoreKeeperGenerized.Instance.AddScore(ScoreIfClicked);
        }
    
}
