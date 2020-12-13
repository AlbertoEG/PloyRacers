using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MusicVol : MonoBehaviour
{
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider soundSlider;
    
    public AudioMixer mixer;

    private void Awake()
    {
        float vol;
        mixer.GetFloat("MasterVol", out vol);

        masterSlider.value = Mathf.Pow(10f,vol / 20f);
        
        mixer.GetFloat("SoundVol", out vol);

        soundSlider.value = Mathf.Pow(10f,vol / 20f);
    }

    public void setMusicVol(float _sliderValue)
    {
        mixer.SetFloat("MasterVol", Mathf.Log10(_sliderValue) * 20f);
    }
    
    public void setSoundVol(float _sliderValue)
    {
        mixer.SetFloat("SoundVol", Mathf.Log10(_sliderValue) * 20f);
    }
}
