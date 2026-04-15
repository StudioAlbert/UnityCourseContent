using UnityEngine;

namespace FSM
{
    public class FsmIdle : IState
    {
        public void Enter() {}
        public void Tick() {}
        public void Exit() {}
        public TankContext Context { get; set; }
    }
}
