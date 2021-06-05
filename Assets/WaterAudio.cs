using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterAudio : MonoBehaviour
{
    private AudioSource _as;

    private void Start()
    {
        _as = GetComponent<AudioSource>();
    }

    public void PlayWater()
    {
        _as.Play();
    }

    public void StopWater()
    {
        _as.Stop();
    }
}
