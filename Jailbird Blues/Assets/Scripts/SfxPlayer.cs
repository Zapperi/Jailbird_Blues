using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxPlayer : MonoBehaviour {

    public AudioSource sfxSource;

    public AudioClip[] buttonSounds;
    public AudioClip itemReceived;

    public void ButtonAudioPlay()
    {
        int i = Random.Range(0, buttonSounds.Length);
        sfxSource.clip = buttonSounds[i];
        sfxSource.Play();
    }

    public void ItemAudioPlay()
    {
        sfxSource.clip = itemReceived;
        sfxSource.Play();
    }	

}

