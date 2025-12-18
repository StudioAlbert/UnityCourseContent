using UnityEngine;

namespace Singletons
{
    public class ScoreKeeperGenerized : PersistentSingleton<ScoreKeeperGenerized>
    {
        public int Score;
        
        public void AddScore(int score)
        {
            Score += score;
        }
    }
}
