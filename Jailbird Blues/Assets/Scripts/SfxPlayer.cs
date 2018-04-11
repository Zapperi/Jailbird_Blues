using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxPlayer : MonoBehaviour {

    public AudioSource sfxSource;

    public AudioClip[] buttonSounds;

    public void ButtonAudioPlay()
    {
        int i = Random.Range(0, buttonSounds.Length);
        sfxSource.clip = buttonSounds[i];
        sfxSource.Play();
    }


	

}

