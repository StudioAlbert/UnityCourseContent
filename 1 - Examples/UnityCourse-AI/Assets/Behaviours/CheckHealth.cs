using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "CheckRecovery", story: "[Health]'s health is [Operator] [Threshold]", category: "Conditions", id: "d68f1cdb85241f4d4bfda873f8a42767")]
public partial class CheckHealth : Condition
{
    [SerializeReference] public BlackboardVariable<Health> Health;
    [Comparison(comparisonType: ComparisonType.All)]
    [SerializeReference] public BlackboardVariable<ConditionOperator> Operator;
    [SerializeReference] public BlackboardVariable<float> Threshold;

    public override bool IsTrue()
    {
        // return true;
        float currentHealth = Health.Value.Hp;
        float threshold = Threshold.Value;
        
        switch (Operator.Value)
        {
            case ConditionOperator.Greater:
                return currentHealth > threshold;
            case ConditionOperator.GreaterOrEqual:
                return currentHealth >= threshold;
            case ConditionOperator.Lower:
                return currentHealth < threshold;
            case ConditionOperator.LowerOrEqual:
                return currentHealth <= threshold;
            case ConditionOperator.Equal:
                return Mathf.Approximately(currentHealth, threshold);
            case ConditionOperator.NotEqual:
                return !Mathf.Approximately(currentHealth, threshold);
            default:
                return false;
        }
    }

    public override void OnStart()
    {
    }

    public override void OnEnd()
    {
    }
}
