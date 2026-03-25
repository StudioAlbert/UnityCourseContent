using System.Collections.Generic;
using UnityEngine;

namespace UtilityAI_
{
    [CreateAssetMenu(fileName = "PatrolAction", menuName = "AI Example/Actions/Patrol")]
    public class PatrolUtilityAction : UtilityAction
    {

        [SerializeField] private float _patrolDistance;
        private List<Transform> _patrolPoints;
        private int _idxPatrolPoint = 0;

        public override void Initialize(Context context)
        {
            _patrolPoints = context.GetCrypts();
            _idxPatrolPoint = 0;
            context.MoveTo(_patrolPoints[_idxPatrolPoint].position);
        }
        public override void Execute(Context context)
        {
            if (Vector3.Distance(_patrolPoints[_idxPatrolPoint].position, context.SelfTransform.position) < _patrolDistance)
            {
                _idxPatrolPoint++;
                if (_idxPatrolPoint >= _patrolPoints.Count)
                {
                    _idxPatrolPoint = 0;
                }
            }

            context.MoveTo(_patrolPoints[_idxPatrolPoint].position);
            
        }
    }
}
