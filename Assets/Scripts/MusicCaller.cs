using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicCaller : MonoBehaviour
{

    [SerializeField] protected AudioClip music;

    [SerializeField] protected bool play;
    
    // Start is called before the first frame update
    void Awake()
    {
        if (play)
        {
            if(music) MusicController.Instance.PlayMusic(music);
        }
        else MusicController.Instance.StopMusic();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
