
using UnityEngine;
using UnityEngine.InputSystem;
 
/// <summary>
/// This processor scales the value by the inverse of deltaTime.
/// It's useful for time-normalizing framerate-sensitive inputs such as pointer delta.
/// </summary>
#if UNITY_EDITOR
[UnityEditor.InitializeOnLoad]
#endif
class MyOwnProcessor : InputProcessor<Vector2>
{
    /// <summary>Compensates for varialble deltaTime</summary>
    /// <param name="value"></param>
    /// <param name="control"></param>
    /// <returns></returns>
    public override Vector2 Process(Vector2 value, InputControl control) => value * 1.0f;
 
    #if UNITY_EDITOR
    static MyOwnProcessor() => Initialize();
    #endif
 
    [RuntimeInitializeOnLoadMethod]
    static void Initialize() => InputSystem.RegisterProcessor<MyOwnProcessor>();
}
