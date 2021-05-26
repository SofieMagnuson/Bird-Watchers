using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SFXMixer : MonoBehaviour
{
    public AudioMixer audioMixer;
    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", volume);

    }
}
