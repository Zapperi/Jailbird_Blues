using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PPSManager : MonoBehaviour
{

    public PostProcessVolume m_Volume;
    public Vignette m_Vignette;

    public bool fadingIn;
    public bool fadingOut;
    public bool showingCard;
    public bool leftToRight;
    public bool endFadeAfterPan;

    public float fadeInAmount;
    public float showTime;
    public float fadeOutAmount;
    public bool blackScreenOn;
    public float blackScreenTime;
    public float vignetteX;
    public bool timedCard;

    // Use this for initialization
    void Awake()
    {
        fadingIn = false;
        fadingOut = false;
        showingCard = false;
        leftToRight = false;
        endFadeAfterPan = false;
        fadeInAmount = 0;
        fadeOutAmount = 0;
        blackScreenOn = false;
        blackScreenTime = 0f;
        vignetteX = 0.5f;
        timedCard = false;


        m_Vignette = ScriptableObject.CreateInstance<Vignette>();
        m_Vignette.enabled.Override(true);
        m_Vignette.roundness.Override(1f);
        m_Vignette.center.Override(new Vector2(0.5f, 0.5f));
        m_Vignette.intensity.Override(0f);
        m_Vignette.smoothness.Override(0f);

        m_Volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, m_Vignette);
        m_Volume.isGlobal = true;
    }

    void Update()
    {
        if (fadingIn && leftToRight)
        {
            vignetteX += fadeInAmount / 2f;
            m_Vignette.center.Override(new Vector2(vignetteX, 0.5f));
            if (vignetteX >= 0.5f)
            {
                vignetteX = 0.5f;
                m_Vignette.center.Override(new Vector2(vignetteX, 0.5f));
            }
            if (m_Vignette.smoothness > 0f)
            {
                m_Vignette.smoothness.value -= fadeInAmount;
                if (m_Vignette.smoothness.value < 0f)
                {
                    m_Vignette.smoothness.value = 0f;
                }
            }
            if (m_Vignette.intensity.value > 0f)
            {
                m_Vignette.intensity.value -= fadeInAmount;
                if (m_Vignette.intensity.value < 0f)
                {
                    m_Vignette.intensity.value = 0f;
                }
            }
            if (vignetteX==0.5f && m_Vignette.smoothness.value == 0f && m_Vignette.intensity.value == 0f)
            {
                fadingIn = false;
                if (timedCard)
                {
                    showingCard = true;
                }
                else
                {
                    FadesDone();
                }
            }

        }
        if (fadingIn && !leftToRight)
        {
            m_Vignette.intensity.value -= fadeInAmount;
            m_Vignette.smoothness.value -= fadeInAmount;
            if (m_Vignette.intensity.value <= 0.1f)
            {
                m_Vignette.intensity.value = 0f;
                m_Vignette.smoothness.value = 0f;
                fadingIn = false;
                fadeInAmount = 0f;
                if (timedCard)
                {
                    showingCard = true;
                }
                else
                {
                    FadesDone();
                }

            }
        }
        if (showingCard)
        {
            showTime -= Time.deltaTime;
            if (showTime <= 0f)
            {
                showTime = 0f;
                if (fadeOutAmount != 0f)
                {
                    showingCard = false;
                    fadingOut = true;
                }
                else
                {
                    showingCard = false;
                    ZeroVignette();
                    FadesDone();
                }
            }
        }

        if (fadingOut && leftToRight)
        {
            vignetteX += fadeOutAmount;
            m_Vignette.center.Override(new Vector2(vignetteX, 0.5f));
            if (vignetteX >= 1f)
            {
                vignetteX = 1f;
                m_Vignette.center.Override(new Vector2(vignetteX, 0.5f));
                fadingOut = false;
                leftToRight = false;
                endFadeAfterPan = true;
            }
        }
        if (fadingOut && !leftToRight)
        {
            m_Vignette.intensity.value += fadeOutAmount;
            m_Vignette.smoothness.value += fadeOutAmount;
            if (m_Vignette.smoothness.value >= 0.99f)
            {
                m_Vignette.intensity.value = 1f;
                m_Vignette.smoothness.value = 1f;
                fadingOut = false;
                fadeOutAmount = 0f;
                FadesDone();

            }
        }
        if (endFadeAfterPan)
        {
            m_Vignette.intensity.value += 2f * fadeOutAmount;
            m_Vignette.smoothness.value += 2f * fadeOutAmount;
            if (m_Vignette.intensity.value >= 0.99f)
            {
                m_Vignette.intensity.value = 1f;
                m_Vignette.smoothness.value = 1f;
                endFadeAfterPan = false;
                fadeOutAmount = 0f;
                FadesDone();

            }
        }

        if (blackScreenOn)
        {

            blackScreenTime -= Time.deltaTime;
            if (blackScreenTime <= 0f)
            {
                blackScreenTime = 0f;
                blackScreenOn = false;
                if (fadeInAmount != 0f)
                {
                    fadingIn = true;
                    if (leftToRight)
                    {
                        SetLeftVignette();
                    }
                    else
                    {
                        SetMiddleVignette();
                    }

                }
                else
                {
                    showingCard = true;
                }
            }
        }

    }

    public void SetFades(CardValues card)
    {
        ResetVignette();
        timedCard = card.isTimeBasedCard;
        if (card.ppsFadeMode == 0)
        {
            leftToRight = false;
        }
        else
        {
            leftToRight = true;
        }

        if (card.blackScreenStart)
        {
            blackScreenOn = true;
            blackScreenTime = card.blackScreenTime;
            BlackScreenVignette();
            //m_Vignette.intensity.value = 1f;
            //m_Vignette.smoothness.value = 0.6f;
        }
        else
        {
            blackScreenOn = false;
        }
        if (card.ppsHasFadeIn)
        {
            if (card.ppsFadeInSpeed == 0)
            {
                fadeInAmount = 1f / (60f);
            }
            else
            {
                fadeInAmount = 1f / (60f * card.ppsFadeInSpeed);
            }
            if (!card.blackScreenStart)
            {
                fadingIn = true;
                if (leftToRight)
                {
                    SetLeftVignette();
                }
                else
                {
                    SetMiddleVignette();
                }
            }
            else
            {
                fadingIn = false;
            }
        }
        if (timedCard)
        {
            if (card.ppsShowCard == 0)
            {
                showTime = 5f;
            }
            else
            {
                showTime = card.ppsShowCard;
            }
            if (!card.blackScreenStart && !card.ppsHasFadeIn)
            {
                showingCard = true;
            }
            else
            {
                showingCard = false;
            }
            fadingOut = false;
            endFadeAfterPan = false;
            if (card.ppsHasFadeOut)
            {
                if (card.fadeOutSpeed == 0)
                {
                    fadeOutAmount = 1f / 60f;
                }
                else
                {
                    fadeOutAmount = 1f / (60f * card.fadeOutSpeed);
                }
            }
        }
    }

    public void ResetVignette()
    {
        vignetteX = 0.5f;
        m_Vignette.center.Override(new Vector2(vignetteX, 0.5f));
        m_Vignette.intensity.value = 0f;
        m_Vignette.smoothness.value = 0f;
        fadingIn = false;
        fadingOut = false;
        leftToRight = false;
        showingCard = false;
        endFadeAfterPan = false;

    }

    public void SetLeftVignette()
    {
        vignetteX = 0.0f;
        m_Vignette.center.Override(new Vector2(vignetteX, 0.5f));
        m_Vignette.intensity.value = 1f;
        m_Vignette.smoothness.value = 1f;
    }

    public void SetMiddleVignette()
    {
        vignetteX = 0.5f;
        m_Vignette.center.Override(new Vector2(0.5f, 0.5f));
        m_Vignette.intensity.value = 1f;
        m_Vignette.smoothness.value = 1f;
    }

    public void ZeroVignette()
    {
        m_Vignette.center.Override(new Vector2(0.5f, 0.5f));
        m_Vignette.intensity.value = 0f;
        m_Vignette.smoothness.value = 0f;
    }

    public void BlackScreenVignette()
    {
        SetLeftVignette();
        vignetteX = -1f;
        m_Vignette.center.Override(new Vector2(vignetteX, 0.5f));
    }

    public void DoFadeOut(CardValues card)
    {
        fadingOut = true;
        if (card.ppsFadeMode == 1)
        {
            leftToRight = true;
        }
        else
        {
            leftToRight = false;
        }

        if (card.fadeOutSpeed == 0)
        {
            fadeOutAmount = 1f / 60f;
        }
        else
        {
            fadeOutAmount = 1f / (60f * card.fadeOutSpeed);
        }
    }


    public void DoFadeIn()
    {
        leftToRight = false;
        SetMiddleVignette();
        fadingIn = true;
        fadeInAmount = 1f / 60f;
    }

    public void FadesDone()
    {
        GameController.gameController.PPSFadesDone();
    }

    public bool FadesAreOn()
    {
        if (m_Vignette.intensity.value != 0)
        {
            return true;
        }
        return false;
    }
}
