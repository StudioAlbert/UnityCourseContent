using UnityEngine;

public class Impact : MonoBehaviour
{

    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private float _time = 3f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() => Destroy(gameObject, (_particleSystem.main.duration - 0.25f));

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("System ? : " + _particleSystem.time + ":" + _particleSystem.totalTime);
        // if (_particleSystem.totalTime > _time)
        // {
        //     Destroy(gameObject);
        // }
    }
}
