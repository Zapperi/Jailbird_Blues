using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsSliders : MonoBehaviour
{

    public static bool instatext;
    public Image gammaImage;
    public GameObject card;
    public Slider textSlider;
    public Slider gammaSlider;
    public Slider scaleSlider;
    public float textSpeed;
    public float gamma;
    public float scale;

    void Start()
    {
        LoadSettings();
        scaleSlider.value = scale;
        textSlider.value = textSpeed;
        gammaSlider.value = gamma;
    }


    public void ScaleChanged(float value)
    {
        card.transform.localScale = new Vector3(value, value, value);
        scale = value;

    }


    public void ScrollSpeedChanged(float value)
    {
        if (value == 0.0f)
            instatext = true;
        else
            instatext = false;
        CardDisplay.textScrollSpeed = value;
        textSpeed = value;
    }

    public void GammaChanged(float value)
    {
        gammaImage.color = new Color(0.1f, 0.1f, 0.1f, value);
        gamma = value;
    }

    public void LoadSettings()
    {
        scale = PersistentData.persistentValues.scale;
        card.transform.localScale = new Vector3(scale, scale, scale);
        textSpeed = PersistentData.persistentValues.textSpeed;
        CardDisplay.textScrollSpeed = textSpeed;
        gamma = PersistentData.persistentValues.gamma;
        gammaImage.color = new Color(0.1f, 0.1f, 0.1f, gamma);
    }

    public void RememberSettings()
    {
        PersistentData.persistentValues.scale = scale;
        PersistentData.persistentValues.textSpeed = textSpeed;
        PersistentData.persistentValues.gamma = gamma;
    }

}
