using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public AudioClip lightning;
    public AudioClip water;
    public AudioClip air;
    public AudioClip fire;
    public AudioClip shatter;
    private AudioSource _ap;
    private float _volume;

    private void Start()
    {
        _ap = GetComponent<AudioSource>();
        if (!PlayerPrefs.HasKey("volume"))
        {
            _volume = 1f;
        }
        else
        {
            // Volume control might not be implemented yet, here for futureproofing
            _volume = PlayerPrefs.GetFloat("volume");
        }
       
    }

    public void PlayLightningSFX()
    {
        _ap.PlayOneShot(lightning, _volume);
    }
    
    public void PlayWaterSFX()
    {
        _ap.PlayOneShot(water, _volume);
    }
    
    public void PlayAirSFX()
    {
        _ap.PlayOneShot(air, _volume);
    }
    
    public void PlayFireSFX()
    {
        _ap.PlayOneShot(fire, _volume);
    }

    public void PlayShatterSFX()
    {
        _ap.PlayOneShot(shatter, _volume);
    }
}
