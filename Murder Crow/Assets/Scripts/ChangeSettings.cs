using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class ChangeSettings : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void SetMainVolume (float volume)
    {
        audioMixer.SetFloat("MainVolume", volume);
    }
}
