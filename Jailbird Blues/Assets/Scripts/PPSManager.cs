using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PPSManager : MonoBehaviour {

    PostProcessVolume m_Volume;
    Vignette m_Vignette;

    bool fadingIn;
    bool fadingOut;
    bool showingCard;
    bool leftToRight;
    bool endFadeAfterPan;
    float timer;
    float fadeInAmount;
    float showTime;
    float fadeOutAmount;
    bool blackScreenOn;
    float blackScreenTime;
    float vignetteX;

	// Use this for initialization
	void Awake () {
        fadingIn = false;
        fadingOut = false;
        showingCard = false;
        leftToRight = false;
        endFadeAfterPan = false;
        timer = 0f;
        fadeInAmount = 0;
        fadeOutAmount = 0;
        blackScreenOn = false;
        blackScreenTime = 0f;
        vignetteX = 0.5f;

        m_Vignette = ScriptableObject.CreateInstance<Vignette>();
        m_Vignette.enabled.Override(true);
        m_Vignette.center.Override(new Vector2(0.5f, 0.5f));
        m_Vignette.smoothness.Override(0f);

        m_Volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, m_Vignette);
	}
	
	void Update () {
		if (blackScreenOn)
        {
            if (Time.deltaTime - timer >= blackScreenTime)
            {
                blackScreenTime = 0f;
                blackScreenOn = false;
                timer = Time.deltaTime;
            }
            if (fadeInAmount != 0f)
            {
                fadingIn = true;
                if (leftToRight)
                {
                    SetLeftVignette();
                } else
                {
                    SetMiddleVignette();
                }

            } else
            {
                showingCard = true;
            }
        }
        if (fadingIn && leftToRight)
        {
            vignetteX += fadeInAmount / 2f;
            m_Vignette.center.Override(new Vector2(vignetteX, 0.5f));
            if (vignetteX >= 0.5f)
            {
                vignetteX = 0.5f;
                m_Vignette.center.Override(new Vector2(vignetteX, 0.5f));
                fadingIn = false;
                timer = Time.deltaTime;
                showingCard = true;
                fadeInAmount = 0f;
            }

        }
        if (fadingIn && !leftToRight)
        {
            m_Vignette.intensity.value -= fadeInAmount;
            m_Vignette.smoothness.value -= fadeInAmount*0.6f;
            if (m_Vignette.intensity.value <= 0.1f)
            {
                m_Vignette.intensity.value = 0f;
                m_Vignette.smoothness.value = 0f;
                fadingIn = false;
                timer = Time.deltaTime;
                showingCard = true;
                fadeInAmount = 0f;

            }
        }
        if (showingCard)
        {
            if (Time.deltaTime-timer >= showTime)
            {
                if (fadeOutAmount != 0f)
                {
                    showingCard = false;
                    fadingOut = true;
                    timer = 0f;
                } else
                {
                    showingCard = false;
                    timer = 0f;
                    ZeroVignette();
                    FadesDone();
                }
            }
        }
        if (fadingOut && leftToRight)
        {
            vignetteX += fadeInAmount;
            m_Vignette.center.Override(new Vector2(vignetteX, 0.5f));
            if (vignetteX >= 1f)
            {
                vignetteX = 1f;
                m_Vignette.center.Override(new Vector2(vignetteX, 0.5f));
                fadingOut = false;
                timer = 0f;
                endFadeAfterPan = true;
            }
        }
        if (fadingOut && !leftToRight)
        {
            m_Vignette.intensity.value += fadeInAmount;
            m_Vignette.smoothness.value += fadeInAmount * 0.6f;
            if (m_Vignette.intensity.value >= 0.99f)
            {
                m_Vignette.intensity.value = 1f;
                m_Vignette.smoothness.value = 1f;
                fadingOut = false;
                timer = 0;
                fadeOutAmount = 0f;
                FadesDone();

            }
        }

        if (endFadeAfterPan)
        {
            m_Vignette.intensity.value += 2f*fadeInAmount;
            m_Vignette.smoothness.value += fadeInAmount * 1.2f;
            if (m_Vignette.intensity.value >= 0.99f)
            {
                m_Vignette.intensity.value = 1f;
                m_Vignette.smoothness.value = 1f;
                endFadeAfterPan = false;
                timer = 0;
                fadeOutAmount = 0f;
                FadesDone();

            }
        }
	}

    public void SetFades(CardValues card)
    {
        ResetVignette();
        timer = Time.deltaTime;
        if (card.ppsFadeMode == 0)
        {
            leftToRight = false;
        } else
        {
            leftToRight = true;
        }

        if (card.blackScreenStart)
        {
            blackScreenOn = true;
            blackScreenTime = card.blackScreenTime;
            m_Vignette.intensity.value = 1f;
            m_Vignette.smoothness.value = 0.6f;
        }
        if (card.ppsHasFadeIn)
        {
            if (card.ppsFadeInSpeed == 0)
            {
                fadeInAmount = 1f / (60f);
            } else
            {
                fadeInAmount = 1f/(60f*card.ppsFadeInSpeed);
            }
            if (!card.blackScreenStart)
            {
                fadingIn = true;
                if (leftToRight)
                {
                    SetLeftVignette();
                } else
                {
                    SetMiddleVignette();
                }
            }
        }
        if (card.ppsShowCard == 0)
        {
            showTime = 5f;
        } else
        {
            showTime = card.ppsShowCard;
        }
        if (!card.blackScreenStart && !card.ppsHasFadeIn)
        {
            showingCard = true;
        }
        if (card.ppsHasFadeOut)
        {
            if (card.fadeOutSpeed == 0)
            {
                fadeOutAmount = 1f/60f;
            } else
            {
                fadeOutAmount = 1f/(60f*card.fadeOutSpeed);
            }
        }
    }

    public void ResetVignette()
    {
        vignetteX = 0.5f;
        m_Vignette.center.Override(new Vector2(vignetteX, 0.5f));
        m_Vignette.intensity.value = 0f;
        m_Vignette.smoothness.value = 0f;
    }

    public void SetLeftVignette()
    {
        vignetteX = 0.5f;
        m_Vignette.center.Override(new Vector2(0.0f, 0.5f));
        m_Vignette.intensity.Override(1f);
        m_Vignette.smoothness.Override(0.6f);
    }

    public void SetMiddleVignette()
    {
        vignetteX = 0.5f;
        m_Vignette.center.Override(new Vector2(0.5f, 0.5f));
        m_Vignette.intensity.Override(1f);
        m_Vignette.smoothness.Override(0.6f);
    }

    public void ZeroVignette()
    {
        m_Vignette.center.Override(new Vector2(0.5f, 0.5f));
        m_Vignette.intensity.value = 0f;
        m_Vignette.smoothness.value = 0f;
    }

    public void FadesDone()
    {
        //lähettää viestin game controllerille että valmis. Kun game controller vaihtaa kortin se tarkistaa uuden
        //kortin feidi asetukset ja tsekkaa ja tarvittaessa resetoi feidit asemiin
    }

}
