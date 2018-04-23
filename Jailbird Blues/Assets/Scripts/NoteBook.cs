using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NoteBook : MonoBehaviour {

    public Text noteBookText;
	public Text inventoryTextBrownie;
	public Text inventoryTextCigs;
    public Button buttonQuit;
    public Button logButton;
    public Button inventoryButton;
    public Button statsButton;
    public Button optionsButton;
    public GameObject logPage;
    public GameObject inventoryPage;
    public GameObject statsPage;
    public GameObject optionsPage;
	// Use this for initialization
	void Start () {

        buttonQuit.onClick.AddListener(ButtonQuitpressed);
        inventoryButton.onClick.AddListener(InventoryButtonpressed);
        statsButton.onClick.AddListener(StatsButtonpressed);
        logButton.onClick.AddListener(LogButtonpressed);
        optionsButton.onClick.AddListener(OptionsButtonpressed);
        this.gameObject.SetActive(false);


    }
	
	// Update is called once per frame
	void Update () {
        noteBookText.text = "IRS rep: " + GameController.gameController.irsRep + "\n" + "Punks rep: " + GameController.gameController.punksRep + "\n" +
            "Shakers rep: " + GameController.gameController.shakersRep + "\n" + "Guards rep: " + GameController.gameController.guardsRep;      
    }

    public void UpdateNotebook()
    {
        logPage.GetComponent<LogScript>().UpdateLogText();
    }

    void LogButtonpressed()
    {
        GameController.gameController.ButtonClickPLay();
        logPage.gameObject.SetActive(true);
        inventoryPage.gameObject.SetActive(false);
        statsPage.gameObject.SetActive(false);
        optionsPage.gameObject.SetActive(false);
        logPage.GetComponent<LogScript>().UpdateLogText();
    }

    void InventoryButtonpressed()
    {
        GameController.gameController.ButtonClickPLay();
        logPage.gameObject.SetActive(false);
        inventoryPage.gameObject.SetActive(true);
        statsPage.gameObject.SetActive(false);
        optionsPage.gameObject.SetActive(false);
    }

    void StatsButtonpressed()
    {
        GameController.gameController.ButtonClickPLay();
        logPage.gameObject.SetActive(false);
        inventoryPage.gameObject.SetActive(false);
        statsPage.gameObject.SetActive(true);
        optionsPage.gameObject.SetActive(false);
    }

    void OptionsButtonpressed()
    {
        GameController.gameController.ButtonClickPLay();
        logPage.gameObject.SetActive(false);
        inventoryPage.gameObject.SetActive(false);
        statsPage.gameObject.SetActive(false);
        optionsPage.gameObject.SetActive(true);
    }

    void ButtonQuitpressed()
    {
		SceneManager.LoadScene("Menu");
		/*

#if UNITY_EDITOR

        UnityEditor.EditorApplication.isPlaying = false;

#else
       
          Application.Quit();
        
#endif
*/
    }
    //this function exists just so we don't need to declare logpage gameobject in other scripts
    //public void AddEventToLog(int index)
    //{
    //    logPage.GetComponent<LogScript>().AddLogEvent(index);
    //}
}
