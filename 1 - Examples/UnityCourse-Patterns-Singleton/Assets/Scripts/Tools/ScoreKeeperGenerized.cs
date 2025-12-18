using UnityEngine;

namespace Singletons
{
    public class ScoreKeeperGenerized : GenericSingleton<ScoreKeeperGenerized>
    {
        public int Score { get; private set; }
        
        public void AddScore(int score)
        {
            Score += score;
        }
    }
}
