using UnityEngine;

namespace UtilityAI_
{
    public abstract class Consideration : ScriptableObject
    {
        public abstract float Evaluate(Context context);
    }
}
