using UnityEngine;

namespace UtilityAI_
{
    [CreateAssetMenu(fileName = "new Chase Action", menuName = "AI Example/Actions/Chase")]
    public class ChaseUtilityAction : UtilityAction
    {
        public override void Initialize(Context context)
        {
        }
        public override void Execute(Context context)
        {
            context.MoveTo(context.GetPlayerTransform().position);
        }
    }
}
