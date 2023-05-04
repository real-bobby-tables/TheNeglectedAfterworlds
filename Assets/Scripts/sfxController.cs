using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SFX {
    UseItem, Die, PlayerHurt, EnemyHurt
};
public class sfxController : MonoBehaviour
{
 
    public AudioClip useItemSfx;
    public AudioClip deathSFX;
    public AudioClip hitHurtEnemy;
    public AudioClip hitHurtPlayer;

    AudioSource source;
    
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        VolumeChanged();
    }

    public void PlaySFX(SFX s)
    {
        switch(s)
        {
            case SFX.UseItem: {
                source.PlayOneShot(useItemSfx);
            } break;
            case SFX.Die: {
                source.PlayOneShot(deathSFX);
            } break;
            case SFX.PlayerHurt: {
                source.PlayOneShot(hitHurtEnemy);
            } break;
            case SFX.EnemyHurt: {
                source.PlayOneShot(hitHurtPlayer);
            } break;
        }
    }


    public void VolumeChanged()
    {
        float loud = PlayerPrefs.GetFloat("SFXVolume", 0.5f);
        source.volume = loud;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
