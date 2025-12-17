using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FootSteps : MonoBehaviour
{
    [SerializeField] private List<AudioClip> _footSteps;
    [SerializeField] private float _stepRate = 0.25f;

    private AudioSource _source;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _source = GetComponent<AudioSource>();
    }
    private void OnEnable() => StartCoroutine(FootSteps_co());
    private void OnDisable() => StopCoroutine(FootSteps_co());

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator FootSteps_co()
    {
        do
        {
            PlayOneStep();
            yield return new WaitForSeconds(_stepRate);
        } while (true);
    }
    private void PlayOneStep()
    {
        _source.PlayOneShot(_footSteps[Random.Range(0, _footSteps.Count)]);
    }
}
