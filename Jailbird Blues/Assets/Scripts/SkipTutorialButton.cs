using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkipTutorialButton : MonoBehaviour {
    public Text skipTutorial;
    public Text clickToContinue;
    public Color lerpedColor = Color.white;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        skipTutorial.color = Color.Lerp(Color.white, Color.grey, Mathf.PingPong(Time.time, 1));
        clickToContinue.color = Color.Lerp(Color.grey, Color.white, Mathf.PingPong(Time.time, 1));

    }
}
