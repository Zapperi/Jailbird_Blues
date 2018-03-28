using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteBook : MonoBehaviour {

    public Text noteBookText;
    public Button buttonQuit;
	// Use this for initialization
	void Start () {
      


    }
	
	// Update is called once per frame
	void Update () {

        
        buttonQuit.onClick.AddListener(buttonQuitpressed);

        noteBookText.text = "IRS rep: " + GameController.gameController.irsRep + "\n" + "Punks rep: " + GameController.gameController.punksRep + "\n" +
            "Shakers rep: " + GameController.gameController.shakersRep + "\n" + "Guards rep: " + GameController.gameController.guardsRep;

        
    }
    void buttonQuitpressed()
    {
#if UNITY_EDITOR

        UnityEditor.EditorApplication.isPlaying = false;

#else
       
          Application.Quit();
        
#endif
    }
}
