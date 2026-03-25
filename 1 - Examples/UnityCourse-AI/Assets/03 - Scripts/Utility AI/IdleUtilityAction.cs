using UnityEngine;

namespace UtilityAI_
{

    [CreateAssetMenu(fileName = "IdleAction", menuName = "AI Example/Actions/Idle")]
    public class IdleUtilityAction : UtilityAction
    {

        public override void Initialize(Context context) { }
        public override void Execute(Context context) => Debug.Log("Idling...............");

    }
}