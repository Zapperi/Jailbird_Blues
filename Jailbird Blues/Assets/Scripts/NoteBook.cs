using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NoteBook : MonoBehaviour {

    public Text noteBookText;
    private string statsText;
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
        UpdateStats();


    }
	
	// Update is called once per frame
	void Update () {
        UpdateStats();    
    }

    private void UpdateStats()
    {
        if (GameController.gameController.irsRep >= 0)
            statsText = "IRS rep: <color=green>" + GameController.gameController.irsRep + "</color>\n";
        if (GameController.gameController.irsRep < 0)
            statsText = "IRS rep: <color=red>" + GameController.gameController.irsRep + "</color>\n";
        if (GameController.gameController.punksRep >= 0)
            statsText += "Punks rep: <color=green>" + GameController.gameController.punksRep + "</color>\n";
        if (GameController.gameController.punksRep < 0)
            statsText += "Punks rep: <color=red>" + GameController.gameController.punksRep + "</color>\n";
        if (GameController.gameController.shakersRep >= 0)
            statsText += "Shakers rep: <color=green>" + GameController.gameController.shakersRep + "</color>\n";
        if (GameController.gameController.shakersRep < 0)
            statsText += "Shakers rep: <color=red>" + GameController.gameController.shakersRep + "</color>\n";
        if (GameController.gameController.guardsRep >= 0)
            statsText += "Guards rep: <color=green>" + GameController.gameController.guardsRep + "</color>\n";
        if (GameController.gameController.guardsRep < 0)
            statsText += "Guards rep: <color=red>" + GameController.gameController.guardsRep + "</color>\n";
        noteBookText.text = statsText;
    }

    public void UpdateNotebook()
    {
        UpdateStats();
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
        UpdateStats();
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
