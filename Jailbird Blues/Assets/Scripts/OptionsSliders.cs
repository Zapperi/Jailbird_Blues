using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsSliders : MonoBehaviour {
	public float volume;
	public float textSpeed;
	AudioSource audioSource;

	void Start()
	{
		//Initiate the Slider value to half way
		volume = 0.5f;
		//Fetch the AudioSource from the GameObject
		audioSource = GetComponent<AudioSource>();

	}

	public void VolumeChanged(float value){
		this.volume = value;
		audioSource.volume = volume;
	}

	public void ScrollSpeedChanged(float value){
		this.textSpeed = value;
		CardDisplay.textScrollSpeed = textSpeed/20.0f;
	}

}
