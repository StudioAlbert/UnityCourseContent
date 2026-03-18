using UnityEngine;

public class Brain : MonoBehaviour
{
    
    [SerializeField] private AnimationCurve _animationCurve;
    
    [SerializeField][Range(0,1)] private float _input;
    [SerializeField] private float _score;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _score = _animationCurve.Evaluate(_input);
    }
}
