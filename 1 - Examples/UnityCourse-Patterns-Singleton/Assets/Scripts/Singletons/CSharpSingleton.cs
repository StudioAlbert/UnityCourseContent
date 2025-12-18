namespace Singletons
{
    public class ScoreKeeper
    {
        private static ScoreKeeper _instance;

        public static ScoreKeeper Instance()
        {
            if (_instance == null)
            {
                _instance = new ScoreKeeper();
            }
            return _instance;
        }

        private ScoreKeeper() { }

        public int Score;
        
    }
}