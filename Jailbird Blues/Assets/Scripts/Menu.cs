using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	public GameObject textCredits;
	public GameObject credits;
	public GameObject start;
	public GameObject quit;
	public GameObject back;
	public GameObject options;
	public GameObject Sliders;

	public static float scale = 1.0f;
	public static float musicVolume = 0.5f;
	public static float sfxVolume = 0.5f;
	public static float textSpeed = 0.25f;
	public static float gamma = 0.0f;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnStartGame(){
		Debug.Log ("start press");
		SceneManager.LoadScene("tempSceneDeleteLater");
	}
	public void OnQuit(){

		Debug.Log ("quit");
		Application.Quit();
	}
	public void OnCredits(){
		Debug.Log ("credits press");
		textCredits.SetActive(true);
		credits.SetActive(false);
		start.SetActive(false);
		quit.SetActive(false);
		back.SetActive(true);
		options.SetActive(false);
	}
	public void OnOptions(){

		Debug.Log ("options");
		Sliders.SetActive(true);
		credits.SetActive(false);
		start.SetActive(false);
		quit.SetActive(false);
		back.SetActive(true);
		options.SetActive(false);
	}

	public void OnReturn(){
		Debug.Log ("back press");
		textCredits.SetActive(false);
		credits.SetActive(true);
		start.SetActive(true);
		quit.SetActive(true);
		back.SetActive(false);
		Sliders.SetActive(false);
		options.SetActive(true);
	}
}
