using System;
using UnityEngine;

namespace Singletons
{
    public class ScoreKeeperAsComponent : MonoBehaviour
    {

        #region Singleton

        public static ScoreKeeperAsComponent Instance { get; private set; }
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

        #endregion

        #region ClassicComponent

        public int Score { get; private set; }
        public void AddScore(int score)
        {
            Score += score;
        }

        #endregion

    }
}
