using UnityEngine;

namespace FSM
{
    public class FsmPatrol : IState
    {

        private Transform _currentWayPoint;
        private int _idxWaypoint = 0;
        
        public void Enter()
        {
            if(Context.Waypoints.Length > 0)
            {
                //_idxWaypoint = 0;
                _currentWayPoint = Context.Waypoints[_idxWaypoint];
            }
        }
        
        public void Tick()
        {

            if (Vector3.Distance(_currentWayPoint.position, Context.SelfTransform.position) <= Context.PatrolDistance)
            {
                _idxWaypoint++;
                if (_idxWaypoint >= Context.Waypoints.Length) _idxWaypoint = 0;
                _currentWayPoint = Context.Waypoints[_idxWaypoint];
            }
            
            Context.MoveTo(_currentWayPoint.position);
            
        }

        public void Exit()
        {
            Context.StopMove();
        }
        
        public TankContext Context { get; set; }
        
    }
}
