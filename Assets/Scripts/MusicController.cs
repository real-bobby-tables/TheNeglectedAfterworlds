using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    
    AudioSource audioSource;
    void Start()
    {
         audioSource = GetComponent<AudioSource>();
         audioSource.volume = PlayerPrefs.GetFloat("MainMusicVolume");
         //audioSource.volume = PlayerPrefs.GetFloat("SFXVolume"); //for wherever this needs to go for sfx
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void UpdateVolume()
    {
        audioSource.volume = PlayerPrefs.GetFloat("MainMusicVolume");
    }
}
