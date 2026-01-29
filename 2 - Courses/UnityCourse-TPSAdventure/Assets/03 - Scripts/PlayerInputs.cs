using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInputs : MonoBehaviour
{

    public Vector2 InputMove;

    public void OnMove(InputAction.CallbackContext ctx) => InputMove = ctx.ReadValue<Vector2>();

    
}
