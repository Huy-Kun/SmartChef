using System;
using System.Collections;
using System.Collections.Generic;
using Dacodelaac.Core;
using UnityEngine;

public class StoveCounterSound : BaseMono
{
    [SerializeField] private StoveCounter stoveCounter; 
    
    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        stoveCounter.OnStateChanged += StoveCounterOnOnStateChanged;
    }

    private void StoveCounterOnOnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
    {
        bool playSound = e.state == StoveCounter.State.Fried || e.state == StoveCounter.State.Frying;
        if(playSound)
            audioSource.Play();
        else audioSource.Pause();
    }
}