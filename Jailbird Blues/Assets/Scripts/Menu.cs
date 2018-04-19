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

	}
	public void OnReturn(){
		Debug.Log ("back press");
		textCredits.SetActive(false);
		credits.SetActive(true);
		start.SetActive(true);
		quit.SetActive(true);
		back.SetActive(false);
	}
}
