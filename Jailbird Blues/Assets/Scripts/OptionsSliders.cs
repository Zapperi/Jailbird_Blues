﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsSliders : MonoBehaviour {
	public float volume;
	public float textSpeed;
	public static bool instatext;
	AudioSource audioSource;
	public Image gammaImage;
	public Canvas canvas;

	void Start()
	{
		//Initiate the Slider value to half way
		volume = 0.5f;
		//Fetch the AudioSource from the GameObject
		audioSource = GetComponent<AudioSource>();

	}
	public void ScaleChanged(float value){
		canvas.GetComponent<CanvasScaler> ().scaleFactor = value;
	}
	public void VolumeChanged(float value){
		this.volume = value;
		audioSource.volume = volume;
	}
	public void SfxChanged(float value){
		//value on slideristä saatava float välillä 0-1. valuen rangen voi tarvittaessa vaihtaa
	}

	public void ScrollSpeedChanged(float value){
		this.textSpeed = value;
        if (value == 0.0f)
            instatext = true;
		else
			instatext = false;
		CardDisplay.textScrollSpeed = textSpeed/20.0f;
	}

	public void GammaChanged(float value){
		gammaImage.color = new Color(0.25f, 0.25f, 0.25f, value);
	}
}
