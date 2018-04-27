using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsSliders : MonoBehaviour {

	public float textSpeed;
	public static bool instatext;
	public Image gammaImage;
	//public Canvas canvas;
	public GameObject card;

	void Start()
	{
		//canvas.GetComponent<CanvasScaler> ().scaleFactor = Menu.scale;
		card.transform.localScale = new Vector3(Menu.scale, Menu.scale, Menu.scale);
		CardDisplay.textScrollSpeed = Menu.textSpeed/20.0f;
		gammaImage.color = new Color(0.1f, 0.1f, 0.1f, Menu.gamma);
	}
	public void ScaleChanged(float value){
		//canvas.GetComponent<CanvasScaler> ().scaleFactor = value;
		card.transform.localScale = new Vector3(value, value, value);
		Menu.scale = value;
	}
	public void VolumeChanged(float value){
		Menu.musicVolume = value;
	}
	public void SfxChanged(float value){
		//value on slideristä saatava float välillä 0-1. valuen rangen voi tarvittaessa vaihtaa
		Menu.sfxVolume = value;
	}

	public void ScrollSpeedChanged(float value){
		this.textSpeed = value;
        if (value == 0.0f)
            instatext = true;
		else
			instatext = false;
		CardDisplay.textScrollSpeed = textSpeed/20.0f;
		Menu.textSpeed = value;
	}

	public void GammaChanged(float value){
		gammaImage.color = new Color(0.1f, 0.1f, 0.1f, value);
		Menu.gamma = value;
	}
}
