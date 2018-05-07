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
    public Button closeButton;
    public GameObject logPage;
    public GameObject inventoryPage;
    public GameObject statsPage;
    public GameObject optionsPage;
    public GameObject quitConfirmation;

	// Use this for initialization
	void Start () {

        buttonQuit.onClick.AddListener(ButtonQuitpressed);
        inventoryButton.onClick.AddListener(InventoryButtonpressed);
        statsButton.onClick.AddListener(StatsButtonpressed);
        logButton.onClick.AddListener(LogButtonpressed);
        optionsButton.onClick.AddListener(OptionsButtonpressed);
        closeButton.onClick.AddListener(CloseButtonpressed);
        this.gameObject.SetActive(false);
        UpdateStats();


    }
	
	// Update is called once per frame
	void Update () {
        UpdateStats();    
    }

    private void UpdateStats()
    {
        statsText = "<size=40>REPUTATIONS</size>\n\n";
        if (GameController.gameController.irsRep >= 0)
            statsText += "I.R.S: <color=green>" + GameController.gameController.irsRep + "</color>\n";
        if (GameController.gameController.irsRep < 0)
            statsText += "I.R.S: <color=red>" + GameController.gameController.irsRep + "</color>\n";
        if (GameController.gameController.punksRep >= 0)
            statsText += "Punks: <color=green>" + GameController.gameController.punksRep + "</color>\n";
        if (GameController.gameController.punksRep < 0)
            statsText += "Punks: <color=red>" + GameController.gameController.punksRep + "</color>\n";
        if (GameController.gameController.shakersRep >= 0)
            statsText += "Protein Shakers: <color=green>" + GameController.gameController.shakersRep + "</color>\n";
        if (GameController.gameController.shakersRep < 0)
            statsText += "Protein Shakers: <color=red>" + GameController.gameController.shakersRep + "</color>\n";
        if (GameController.gameController.guardsRep >= 0)
            statsText += "Guards: <color=green>" + GameController.gameController.guardsRep + "</color>\n";
        if (GameController.gameController.guardsRep < 0)
            statsText += "Guards: <color=red>" + GameController.gameController.guardsRep + "</color>\n";
        noteBookText.text = statsText;
    }

    public void UpdateNotebook()
    {
        UpdateStats();
        logPage.GetComponent<LogScript>().UpdateLogText();
    }

    public void LogButtonpressed()
    {
        GameController.gameController.ButtonClickPLay();
        logPage.gameObject.SetActive(true);
        inventoryPage.gameObject.SetActive(false);
        statsPage.gameObject.SetActive(false);
        optionsPage.gameObject.SetActive(false);
        logPage.GetComponent<LogScript>().UpdateLogText();
        UpdateStats();
    }

    public void InventoryButtonpressed()
    {
        GameController.gameController.ButtonClickPLay();
        logPage.gameObject.SetActive(false);
        inventoryPage.gameObject.SetActive(true);
        statsPage.gameObject.SetActive(false);
        optionsPage.gameObject.SetActive(false);
    }

    public void StatsButtonpressed()
    {
        GameController.gameController.ButtonClickPLay();
        logPage.gameObject.SetActive(false);
        inventoryPage.gameObject.SetActive(false);
        statsPage.gameObject.SetActive(true);
        optionsPage.gameObject.SetActive(false);
    }

    public void OptionsButtonpressed()
    {
        GameController.gameController.ButtonClickPLay();
        logPage.gameObject.SetActive(false);
        inventoryPage.gameObject.SetActive(false);
        statsPage.gameObject.SetActive(false);
        optionsPage.gameObject.SetActive(true);
    }

    public void CloseButtonpressed()
    {
        GameController.gameController.ButtonClickPLay();
        gameObject.SetActive(false);
    }

    public void ButtonQuitpressed()
    {
        if (!quitConfirmation.activeSelf)
            gameObject.transform.parent.GetComponentInChildren<CardDisplay>().BlockButtons();
        quitConfirmation.SetActive(true);
    }
}
