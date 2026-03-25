using UnityEngine;

namespace UtilityAIByAdamMhyre {
    public abstract class Consideration : ScriptableObject {
        public abstract float Evaluate(Context context);
    }
}