using UnityEngine;

namespace UtilityAI_
{
    [CreateAssetMenu(fileName = "New Constant Consideration", menuName = "AI Example/Considerations/Constant")]
    public class ConstantConsideration : Consideration
    {
        public float RawScore;
        public override float Evaluate(Context context) => RawScore;
    }
}