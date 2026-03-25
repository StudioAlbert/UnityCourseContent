using UnityEngine;

namespace UtilityAIByAdamMhyre {
    [CreateAssetMenu(menuName = "UtilityAI/Considerations/InRangeConsideration")]
    public class InRangeConsideration : Consideration {
        public float maxDistance = 10f;
        public float maxAngle = 360f;
        public string targetTag = "Target";
        public AnimationCurve curve;

        public override float Evaluate(Context context) {
            if (!context.sensor.targetTags.Contains(targetTag)) {
                context.sensor.targetTags.Add(targetTag);
            }
            
            Transform targetTransform = context.sensor.GetClosestTarget(targetTag);
            if (targetTransform == null) return 0f;
            
            Transform agentTransform = context.agent.transform;
            
            Vector3 directionToTarget = (targetTransform.position - agentTransform.position);
            directionToTarget.y = 0f;
            bool isInRange = directionToTarget.magnitude <= maxDistance && Vector3.Angle(agentTransform.forward, directionToTarget) <= maxAngle / 2;
            if (!isInRange) return 0f;
            
            float distanceToTarget = directionToTarget.magnitude;
            
            float normalizedDistance = Mathf.Clamp01(distanceToTarget / maxDistance);
            
            float utility = curve.Evaluate(normalizedDistance);
            return Mathf.Clamp01(utility);
        }
        
        void Reset() {
            curve = new AnimationCurve(
                new Keyframe(0f, 1f),
                new Keyframe(1f, 0f)
            );
        }
    }
}