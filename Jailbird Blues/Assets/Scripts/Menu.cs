using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

	public GameObject textCredits;
	public GameObject credits;
	public GameObject start;
	public GameObject quit;
	public GameObject back;
	public GameObject options;
	public GameObject Sliders;
    public Slider masterVolSlider;
    public Slider musicVolSlider;
    public Slider sfxVolSlider;
    public AudioSource menuMusicSource;

    public float masterVolume;
    public float musicVolume;
    public float sfxVolume;


	// Use this for initialization
	void Start () {
        LoadSettings();
	}
	
	// Update is called once per frame
	void Update () {
        masterVolume = masterVolSlider.value;
        musicVolume = musicVolSlider.value;
        sfxVolume = sfxVolSlider.value;
        menuMusicSource.volume = masterVolume * musicVolume;
	}

	public void OnStartGame(){
		Debug.Log ("start press");
        GetComponent<OptionsSliders>().RememberSettings();
        RememberSettings();
		SceneManager.LoadScene("tempSceneDeleteLater");
	}
	public void OnQuit(){

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();         
#endif
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

    void LoadSettings()         //is called from start, loads settings from persistent object
    {
        masterVolume = PersistentData.persistentValues.masterVolume;
        masterVolSlider.value = masterVolume;
        musicVolume = PersistentData.persistentValues.musicVolume;
        musicVolSlider.value = musicVolume;
        sfxVolume = PersistentData.persistentValues.sfxVolume;
        sfxVolSlider.value = sfxVolume;

    }

    void RememberSettings()     //at the end of the scene, records current settings to persistent object
    {
        PersistentData.persistentValues.masterVolume = masterVolume;
        PersistentData.persistentValues.musicVolume = musicVolume;
        PersistentData.persistentValues.sfxVolume = sfxVolume;
    }

}
