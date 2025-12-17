using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomShooter : MonoBehaviour
{
    [SerializeField] private AudioClip _clip;
    [SerializeField] private float _stepRateMin = 0.25f;
    [SerializeField] private float _stepRateMax = 0.75f;

    // private AudioSource _source;
    private SoundShooterManager _soundShooterManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        // _source = GetComponent<AudioSource>();
        _soundShooterManager = GetComponentInParent<SoundShooterManager>();
    }
    
    private void OnEnable() => StartCoroutine(RandomShoot_co());
    private void OnDisable() => StopCoroutine(RandomShoot_co());

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator RandomShoot_co()
    {
        do
        {
            PlayOneShoot();
            yield return new WaitForSeconds(Random.Range(_stepRateMin, _stepRateMax));
        } while (true);
    }
    
    private void PlayOneShoot()
    {
        // _source.PlayOneShot(_clip);
        _soundShooterManager?.PlayShootSound(_clip);
    }
}
