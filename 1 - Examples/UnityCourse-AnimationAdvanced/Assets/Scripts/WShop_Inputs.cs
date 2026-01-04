using UnityEngine;
using UnityEngine.InputSystem;

namespace DefaultNamespace
{
    
    // ReSharper disable once InconsistentNaming
    public class WShop_Inputs : MonoBehaviour
    {
        public Vector2 Move;
        public bool Jump;
        
        public void OnMove(InputValue value) => Move = value.Get<Vector2>();
        public void OnJump(InputValue value) => Jump = value.isPressed;

    }
    
}
