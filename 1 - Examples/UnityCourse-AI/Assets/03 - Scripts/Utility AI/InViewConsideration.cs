using UnityEngine;

namespace UtilityAI_
{
    [CreateAssetMenu(fileName = "In view Consideration", menuName = "AI Example/Considerations/In view Consideration")]
    public class InViewConsideration : Consideration
    {
        [SerializeField] private AnimationCurve _correctionCurve;
        [SerializeField] private float _range = 2;
        [SerializeField] private float _angleRange = 40;

        public float Score;
        
        public override float Evaluate(Context context)
        {
            Vector3 distance = context.GetPlayerTransform().position - context.SelfTransform.position;
            float distanceScore = Mathf.Clamp01(1 - (distance.magnitude / _range));

            float angle = Vector3.Angle(context.SelfTransform.forward, distance);
            float angleScore = Mathf.Clamp01(1 - (angle / _angleRange));
            
            Score = _correctionCurve.Evaluate(distanceScore * angleScore);
            
            //Debug.Log($"Distance: {distance}, Angle: {angle}, Score: {score}");
            
            return Score;
            
        }
    }
}
