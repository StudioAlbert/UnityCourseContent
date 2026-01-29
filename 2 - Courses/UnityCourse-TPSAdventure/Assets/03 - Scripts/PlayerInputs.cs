using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour
{

    public Vector2 Move;
    public bool Jump;
    
    public void OnMove(InputAction.CallbackContext ctx) => Move = ctx.ReadValue<Vector2>();
    public void OnJump(InputAction.CallbackContext ctx) => Jump = ctx.ReadValueAsButton();
    
}
