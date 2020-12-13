using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class CarAudio : MonoBehaviour
{
    
    private Car car;
    private AudioSource source;
    [SerializeField] private float modifier;

    private void OnEnable()
    {
        GameManager.OnFinishRaceHandler += StopSound;
    }
    
    private void OnDisable()
    {
        GameManager.OnFinishRaceHandler -= StopSound;
    }

    private void Awake()
    {
        car = GetComponent<Car>();
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {
        float soundPitchDiff = 1f;

        if (car.Speed > 16f) soundPitchDiff = 1.5f;
        if (car.Speed > 22f) soundPitchDiff = 1.8f;
        if (car.Speed > 28f) soundPitchDiff = 2f;
        if (car.Speed > 32f) soundPitchDiff = 2.5f;

        source.pitch = (car.Speed * 0.0875f / soundPitchDiff) * modifier + 0.6f;
        
        if(Time.timeScale == 0f && source.isPlaying) source.Stop();
        else if(Time.timeScale > 0f && !source.isPlaying) source.Play();
    }

    private void StopSound(int _id)
    {
        Destroy(source);
        Destroy(this);
    }
}
