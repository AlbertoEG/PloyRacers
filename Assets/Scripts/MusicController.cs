using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    private AudioSource audioSource;
    
    private static MusicController instance = null;
    public static MusicController Instance
    {
        get { return instance; }
    }
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
            return;
        } else {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
     
    public void PlayMusic(AudioClip _clip)
    { 
        if (audioSource.isPlaying && audioSource.clip == _clip) return;
        audioSource.clip = _clip;
        audioSource.Play();
    }
     
    public void StopMusic()
    {
        audioSource.Stop();
        
    }
}
