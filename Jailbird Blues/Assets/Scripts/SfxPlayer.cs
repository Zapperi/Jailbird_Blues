using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SfxPlayer : MonoBehaviour
{

    public AudioSource sfxSource;
    public AudioClip[] buttonSounds;
    public AudioClip itemReceived;
    public AudioSource timedSfxSource;

    public AudioSource musicSource1;                                                 //2 sources for cross-fade transitions
    public AudioSource musicSource2;
    public AudioSource ambientSource;                                                //gameobject of ambient audio source
    public float musicFadeSpeed;
    private float musicVolume;                                                       //target volume to fade-in
    private bool music1FadingOut;
    private bool music2FadingOut;
    private bool music1FadingIn;
    private bool music2FadingIn;

    public bool timedIsWaiting;
    public bool timedSfxPlaying;
    public bool timedSfxIsFadingOut;
    public bool timedIsAfterWaiting;
    public bool timedSfxHasFadeOut;
    public bool timedHasAfterWait;
    public float timeUntilStart;
    public float timeUntilFadeOut;
    public float afterWaitTime;
    public float fadeOutAmount;

    public float masterVolume;
    public float sfxModifier;
    public float musicModifier;
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

    public float currentMusicVolume1;
    public float currentMusicVolume2;
    public float currentSfxVolume;
    public float currentAmbientVolume;
    public float currentTimedVolume;


    private void Awake()
    {
        music1FadingOut = false;
        music2FadingOut = false;
        music1FadingIn = false;
        music2FadingIn = false;
        timedSfxPlaying = false;
        timedIsWaiting = false;
        timedIsAfterWaiting = false;

        currentSfxVolume = PersistentData.persistentValues.sfxVolume;
        currentTimedVolume = PersistentData.persistentValues.sfxVolume;
    }

    private void Start()
    {
        SetVolumesAndSliders();
    }

    private void Update()
    {
        if (music1FadingIn)
        {
            currentMusicVolume1 += Time.deltaTime * musicFadeSpeed;
            if (currentMusicVolume1 >= musicVolume)
            {
                currentMusicVolume1 = musicVolume;
                music1FadingIn = false;
            }
        }
        if (music2FadingIn)
        {
            currentMusicVolume2 += Time.deltaTime * musicFadeSpeed;
            if (currentMusicVolume2 >= musicVolume)
            {
                currentMusicVolume2 = musicVolume;
                music2FadingIn = false;
            }
        }
        if (music1FadingOut)
        {
            currentMusicVolume1 -= Time.deltaTime * musicFadeSpeed;
            if (currentMusicVolume1 <= 0f)
            {
                currentMusicVolume1 = 0f;
                musicSource1.Stop();
                music1FadingOut = false;
            }
        }
        if (music2FadingOut)
        {
            currentMusicVolume2 -= Time.deltaTime * musicFadeSpeed;
            if (currentMusicVolume2 <= 0f)
            {
                currentMusicVolume2 = 0f;
                musicSource2.Stop();
                music2FadingOut = false;
            }
        }
        //********LINEBREAK***********
        if (timedIsAfterWaiting)
        {
            afterWaitTime -= Time.deltaTime;
            if (afterWaitTime <= 0)
            {
                afterWaitTime = 0f;
                timedIsAfterWaiting = false;
                GameController.gameController.SFXFadesDone();
            }
        }
        if (timedSfxIsFadingOut)
        {
            currentTimedVolume -= fadeOutAmount;
            if (currentTimedVolume <= 0f)
            {
                currentTimedVolume = 0f;
                timedSfxIsFadingOut = false;
                if (timedHasAfterWait)
                {
                    timedIsAfterWaiting = true;
                }
                else
                {
                    GameController.gameController.SFXFadesDone();
                }
            }
        }

        if (timedSfxPlaying)
        {
            if (!timedSfxSource.isPlaying)
            {
                timedSfxPlaying = false;

                if (timedHasAfterWait)
                {
                    timedIsAfterWaiting = true;
                }
                else
                {
                    GameController.gameController.SFXFadesDone();
                }
            }
            else if (timeUntilFadeOut < 0f)
            {
                timedSfxPlaying = false;
                timedSfxIsFadingOut = true;
            }
        }

        if (timedIsWaiting)
        {
            timeUntilStart -= Time.deltaTime;
            if (timeUntilStart <= 0)
            {
                timeUntilStart = 0f;
                timedIsWaiting = false;
                timedSfxPlaying = true;
                timedSfxSource.Play();

            }
        }
        if (timedSfxHasFadeOut)
        {
            if (!timedSfxIsFadingOut && !timedIsAfterWaiting)
            {
                timeUntilFadeOut -= Time.deltaTime;
                if (timeUntilFadeOut <= 0f)
                {
                    timedSfxIsFadingOut = true;
                    timedSfxPlaying = false;
                    timedIsWaiting = false;
                    timedIsAfterWaiting = false;
                    timeUntilFadeOut = 0f;
                }
            }
        }

        sfxSource.volume = currentSfxVolume * sfxModifier * masterVolume;
        timedSfxSource.volume = currentTimedVolume * sfxModifier * masterVolume;
        ambientSource.volume = currentAmbientVolume * sfxModifier * masterVolume;
        musicSource1.volume = currentMusicVolume1 * musicModifier * masterVolume;
        musicSource2.volume = currentMusicVolume2 * musicModifier * masterVolume;

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
                currentMusicVolume2 = mVol;

            }
            else //set the next music to the 1st slot and set the 2nd slot to fade out
            {
                musicSource1.clip = nextMusic;
                musicSource1.Play();
                currentMusicVolume1 = 0f;
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
                currentMusicVolume1 = mVol;
                music2FadingOut = true;
                music1FadingIn = false;
                music1FadingOut = false;

            }
            else if (musicSource2.GetComponent<AudioClip>() == nextMusic)
            {
                currentMusicVolume2 = mVol;
                music1FadingOut = true;
                music2FadingIn = false;
                music1FadingOut = false;
            }
            else
            {
                musicSource1.clip = nextMusic;
                currentMusicVolume1 = 0;
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
        currentAmbientVolume = aVol;

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
        }
        else
        {
            musicVolume = mVol;
            if (musicSource1.isPlaying && !music1FadingOut)
            {
                currentMusicVolume1 = mVol;
                if (musicSource2.isPlaying)
                {
                    music2FadingOut = true;
                }
            }
            else if (musicSource2.isPlaying && !music2FadingOut)
            {
                currentMusicVolume2 = mVol;
            }
        }
        if (ambientSource.clip == null || ambientSource.clip != nextAmbient)
        {
            ambientSource.clip = nextAmbient;
            ambientSource.Play();
        }
        currentAmbientVolume = aVol;
    }

    public void SetActiveAudios(AudioClip nextMusic, bool ambientOff, float mVol, float aVol)
    {
        musicVolume = mVol;
        //set the next music
        //check if the 1st audio isn't playing
        if (!musicSource1.isPlaying)
        {
            //if the 2nd source is already playing the same music, just adjust the volume if needed

            if (musicSource2.GetComponent<AudioClip>() == nextMusic)
            {
                currentMusicVolume2 = mVol;

            }
            else //set the next music to the 1st slot and set the 2nd slot to fade out
            {
                musicSource1.clip = nextMusic;
                musicSource1.Play();
                currentMusicVolume1 = 0f;
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
                currentMusicVolume1 = mVol;
            }
            else
            {   //set the next music to the 2nd slot and set the 1nd slot to fade out
                musicSource2.clip = nextMusic;
                musicSource2.Play();
                currentMusicVolume2 = 0f;
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
                currentMusicVolume1 = mVol;
                music2FadingOut = true;
                music1FadingIn = false;
                music1FadingOut = false;

            }
            else if (musicSource2.GetComponent<AudioClip>() == nextMusic)
            {
                currentMusicVolume2 = mVol;
                music1FadingOut = true;
                music2FadingIn = false;
                music1FadingOut = false;
            }
            else
            {
                musicSource1.clip = nextMusic;
                currentMusicVolume1 = 0;
                music1FadingOut = false;
                music1FadingIn = true;
                music2FadingOut = true;
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
                currentAmbientVolume = aVol;
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
                currentMusicVolume1 = mVol;
                if (musicSource2.isPlaying)
                {
                    music2FadingOut = true;
                }
            }
            else if (musicSource2.isPlaying && !music2FadingOut)
            {
                currentMusicVolume2 = mVol;
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
                currentAmbientVolume = aVol;
            }
        }
    }

    public void SetMasterVolume(float value)
    {
        masterVolume = value;
    }

    public void SetMusicModifier(float value)
    {
        musicModifier = value;
    }

    public void SetSfxModifier(float value)
    {
        sfxModifier = value;
    }

    public void SetVolumesAndSliders()
    {
        masterVolume = PersistentData.persistentValues.masterVolume;
        musicModifier = PersistentData.persistentValues.musicVolume;
        sfxModifier = PersistentData.persistentValues.sfxVolume;
        masterSlider.value = masterVolume;
        musicSlider.value = musicModifier;
        sfxSlider.value = sfxModifier;

    }



    public void PlayTimedSfx(CardValues currentCard)
    {
        if (currentCard.sfx == null)
        {
            GameController.gameController.SFXFadesDone();
        }
        else
        {
            if (timedSfxSource.isPlaying)
            {
                timedSfxSource.Stop();
            }
            timedSfxSource.clip = currentCard.sfx;
            timedSfxSource.volume = 1f;
            currentTimedVolume = 1f;

            if (currentCard.sfxPrewait == 0)
            {
                timedIsWaiting = false;
                timedSfxPlaying = true;
                timedSfxSource.Play();
            }
            else
            {
                timedIsWaiting = true;
                timeUntilStart = currentCard.sfxPrewait;
            }

            if (currentCard.sfxFadeOutAt == 0)
            {
                timedSfxHasFadeOut = false;
            }
            else
            {
                timedSfxHasFadeOut = true;
                timeUntilFadeOut = currentCard.sfxFadeOutAt;
                if (timeUntilFadeOut < timeUntilStart)
                {
                    timeUntilFadeOut += timeUntilStart;
                }
                if (currentCard.fadeOutSpeed == 0)
                {
                    fadeOutAmount = 1f / 30f;
                }
                else
                {
                    fadeOutAmount = currentCard.fadeOutSpeed / 60f;
                }
                if (currentCard.sfxAfterWait == 0)
                {
                    timedHasAfterWait = false;
                }
                else
                {
                    timedHasAfterWait = true;
                    afterWaitTime = currentCard.sfxAfterWait;
                }
            }

        }
    }

    public void SetFadingOutTrue()
    {
        if (timedIsWaiting)
        {
            timedIsWaiting = false;
            timeUntilStart = 0f;
            timeUntilFadeOut = 0f;
            afterWaitTime = 0f;
            GameController.gameController.SFXFadesDone();
            return;
        }
        if (timedSfxPlaying)
        {
            timedSfxPlaying = false;
            timedSfxIsFadingOut = true;
            fadeOutAmount = 1f / 30f;
            timedHasAfterWait = false;
            afterWaitTime = 0f;
        }
        if (timedIsAfterWaiting)
        {
            afterWaitTime = 0f;

        }

    }
}

