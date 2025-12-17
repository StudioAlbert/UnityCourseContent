using UnityEngine;
using UnityEngine.InputSystem;

public class AudioShot : MonoBehaviour
{
    [SerializeField] private AudioClip _clip;

    private AudioSource _source;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlaySound(InputAction.CallbackContext ctx)
    {
        if(ctx.performed)
        {
            _source.clip = _clip;
            _source.pitch = Random.Range(0.8f, 1.2f);
            _source.Play();
        }
    }
}
