using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

public class SoundShooterManager : MonoBehaviour
{
    private AudioSource _source;

    [SerializeField] private int _poolSize = 5;

    [SerializeField] private AudioSource _sourceOrigin;
    
    private List<AudioSource> _sourcePool = new List<AudioSource>();
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        for (int i = 0; (i) < _poolSize; (i)++)
        {
            AudioSource newSource = Instantiate(_sourceOrigin, this.transform);
            _sourcePool.Add(newSource);
        }
    }

    public void PlayShootSound(AudioClip clip)
    {
        AudioSource playableSource = GetAvailableSource();
        if(playableSource != null)
        {
            playableSource.PlayOneShot(clip);
        }
    }
    private AudioSource GetAvailableSource()
    {
        AudioSource goodSource = _sourcePool.FirstOrDefault(s => s.isPlaying == false);
        return goodSource;
    }
}
