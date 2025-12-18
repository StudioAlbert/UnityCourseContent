using System;
using UnityEngine;

namespace Singletons
{
    public class ScoreKeeperAsComponent : MonoBehaviour
    {
        public static ScoreKeeperAsComponent Instance { get; private set; }
        
        public int Score { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        public void AddScore(int score)
        {
            Score += score;
        }


    }
}
