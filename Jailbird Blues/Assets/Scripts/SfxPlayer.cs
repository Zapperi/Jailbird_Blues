using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxPlayer : MonoBehaviour {

    public AudioSource sfxSource;
    public AudioClip[] buttonSounds;
    public AudioClip itemReceived;

    public AudioSource musicSource1;                                                 //2 sources for cross-fade transitions
    public AudioSource musicSource2;
    public AudioSource ambientSource;                                                //gameobject of ambient audio source
    public float musicFadeSpeed;
    private float musicVolume;                                                       //target volume to fade-in
    private bool music1FadingOut;
    private bool music2FadingOut;
    private bool music1FadingIn;
    private bool music2FadingIn;

    private void Awake()
    {
        music1FadingOut = false;
        music2FadingOut = false;
        music1FadingIn = false;
        music2FadingIn = false;
    }

    private void Update()
    {
        if (music1FadingIn)
        {
            musicSource1.volume += Time.deltaTime * musicFadeSpeed;
            if (musicSource1.volume >= musicVolume)
            {
                musicSource1.volume = musicVolume;
                music1FadingIn = false;
            }
        }
        if (music2FadingIn)
        {
            musicSource2.volume += Time.deltaTime * musicFadeSpeed;
            if (musicSource2.volume >= musicVolume)
            {
                musicSource2.volume = musicVolume;
                music2FadingIn = false;
            }
        }
        if (music1FadingOut)
        {
            musicSource1.volume -= Time.deltaTime * musicFadeSpeed;
            if (musicSource1.volume <= 0f)
            {
                musicSource1.Stop();
                music1FadingOut = false;
            }
        }
        if (music2FadingOut)
        {
            musicSource2.volume -= Time.deltaTime * musicFadeSpeed;
            if (musicSource2.volume <= 0f)
            {
                musicSource2.Stop();
                music2FadingOut = false;
            }
        }
    }

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

    public void SetActiveAudios(AudioClip nextMusic, AudioClip nextAmbient, float mVol, float aVol)
    {
        musicVolume = mVol;
        //set the next music
        //if the 1st audio isn't playing
        if (!musicSource1.isPlaying)
        {
            //if the 2nd source is already playing the same music, just adjust the volume if needed

            if (musicSource2.GetComponent<AudioClip>() == nextMusic)
            {
                musicSource2.volume = mVol;

            }
            else //set the next music to the 1st slot and set the 2nd slot to fade out
            {
                musicSource1.clip = nextMusic;
                musicSource1.Play();
                musicSource1.volume = 0f;
                music1FadingIn = true;
                if (musicSource2.isPlaying)
                {
                    music2FadingOut = true;
                }
            }
        }
        //if 1st slot is playing and the 2nd isn't
        else if (!musicSource2.isPlaying)
        {
            //if the next clip is already playing
            if (musicSource1.GetComponent<AudioClip>() == nextMusic)
            {
                musicSource1.volume = mVol;
            } else
            {   //set the next music to the 2nd slot and set the 1nd slot to fade out
                musicSource2.clip = nextMusic;
                musicSource2.Play();
                musicSource2.volume = 0f;
                music2FadingIn = true;
                if (musicSource1.isPlaying)
                {
                    music1FadingOut = true;
                }
            }
        }
        else //just in case for some weird situation if both audios are still playing
        {
            if (musicSource1.GetComponent<AudioClip>() == nextMusic)
            {
                musicSource1.volume = mVol;
                music2FadingOut = true;
                music1FadingIn = false;
                music1FadingOut = false;

            } else if (musicSource2.GetComponent<AudioClip>() == nextMusic)
            {
                musicSource2.volume = mVol;
                music1FadingOut = true;
                music2FadingIn = false;
                music1FadingOut = false;
            } else
            {
                musicSource1.clip = nextMusic;
                musicSource1.volume = 0;
                music1FadingOut = false;
                music1FadingIn = true;
                music2FadingOut = true;
            }
        }
        //set the ambient audio
        if (!ambientSource.clip || ambientSource.clip != nextAmbient)
        {
            ambientSource.clip = nextAmbient;
            ambientSource.Play();
        }
        ambientSource.volume = aVol;

    }

    public void SetActiveAudios(bool musicOff, AudioClip nextAmbient, float mVol, float aVol)
    {
        if (musicOff == true)
        {
            if (musicSource1.isPlaying)
            {
                music1FadingOut = true;
            }
            if (musicSource2.isPlaying)
            {
                music2FadingOut = true;
            }
        } else
        {
            musicVolume = mVol;
            if (musicSource1.isPlaying && !music1FadingOut)
            {
                musicSource1.volume = mVol;
                if (musicSource2.isPlaying)
                {
                    music2FadingOut = true;
                }
            } else if (musicSource2.isPlaying && !music2FadingOut)
            {
                musicSource2.volume = mVol;
            }
        }
        if (ambientSource.clip == null || ambientSource.clip != nextAmbient)
        {
            ambientSource.clip = nextAmbient;
            ambientSource.Play();
        }
        ambientSource.volume = aVol;
    }

    public void SetActiveAudios(AudioClip nextMusic, bool ambientOff, float mVol, float aVol)
    {
        musicVolume = mVol;
        //set the next music
        //if the 1st audio isn't playing
        if (!musicSource1.isPlaying)
        {
            //if the 2nd source is already playing the same music, just adjust the volume if needed

            if (musicSource2.GetComponent<AudioClip>() == nextMusic)
            {
                musicSource2.volume = mVol;

            }
            else //set the next music to the 1st slot and set the 2nd slot to fade out
            {
                musicSource1.clip = nextMusic;
                musicSource1.Play();
                musicSource1.volume = 0f;
                music1FadingIn = true;
                if (musicSource2.isPlaying)
                {
                    music2FadingOut = true;
                }
            }
        }
        //if 1st slot is playing and the 2nd isn't
        else if (!musicSource2.isPlaying)
        {
            //if the next clip is already playing
            if (musicSource1.GetComponent<AudioClip>() == nextMusic)
            {
                musicSource1.volume = mVol;
            }
            else
            {   //set the next music to the 2nd slot and set the 1nd slot to fade out
                musicSource2.clip = nextMusic;
                musicSource2.Play();
                musicSource2.volume = 0f;
                music2FadingIn = true;
                if (musicSource1.isPlaying)
                {
                    music1FadingOut = true;
                }
            }
        }
        else //just in case for some weird situation if both audios are still playing
        {
            if (musicSource1.GetComponent<AudioClip>() == nextMusic)
            {
                musicSource1.volume = mVol;
                music2FadingOut = true;
                music1FadingIn = false;
                music1FadingOut = false;

            }
            else if (musicSource2.GetComponent<AudioClip>() == nextMusic)
            {
                musicSource2.volume = mVol;
                music1FadingOut = true;
                music2FadingIn = false;
                music1FadingOut = false;
            }
            else
            {
                musicSource1.clip = nextMusic;
                musicSource1.volume = 0;
                music1FadingOut = false;
                music1FadingIn = true;
                music2FadingOut = true;
            }
        }

        if (ambientOff)
        {
            if (ambientSource.clip !=null && ambientSource.isPlaying)
            {
                ambientSource.Stop();
            }
        } else
        {
            if (ambientSource.clip != null)
            {
                ambientSource.volume = aVol;
            }
        }
    }

    public void SetActiveAudios(bool musicOff, bool ambientOff, float mVol, float aVol)
    {
        if (musicOff == true)
        {
            if (musicSource1.isPlaying)
            {
                music1FadingOut = true;
            }
            if (musicSource2.isPlaying)
            {
                music2FadingOut = true;
            }
        }
        else
        {
            musicVolume = mVol;
            if (musicSource1.isPlaying && !music1FadingOut)
            {
                musicSource1.volume = mVol;
                if (musicSource2.isPlaying)
                {
                    music2FadingOut = true;
                }
            }
            else if (musicSource2.isPlaying && !music2FadingOut)
            {
                musicSource2.volume = mVol;
            }
        }
        if (ambientOff)
        {
            if (ambientSource.clip != null && ambientSource.isPlaying)
            {
                ambientSource.Stop();
            }
        }
        else
        {
            if (ambientSource.clip != null)
            {
                ambientSource.volume = aVol;
            }
        }


    }
}

