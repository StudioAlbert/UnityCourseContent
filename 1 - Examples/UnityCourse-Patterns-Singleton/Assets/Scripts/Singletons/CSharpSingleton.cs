using UnityEngine;

namespace Singletons
{
    public class ScoreKeeper
    {
        private static ScoreKeeper _instance;
        public static ScoreKeeper Instance => _instance ??= new ScoreKeeper();

        private ScoreKeeper() { }

        public int Score;
    }
}