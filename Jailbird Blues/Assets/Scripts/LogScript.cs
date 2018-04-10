using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogScript : MonoBehaviour {

    private string output;
    public Text logText;
    public List<string> logEvents;
    //just for testing***
    public Button eventAdder;

	// Use this for initialization
	void Start () {
        output = "";
        logText.text = output;

        //initialization of the list of log events

        //logEvents[0] = "I enetered the prison.";
        //logEvents[1]= "I met Kowalski. Nice fellow.";
        //logEvents[2] = "Kowalski took my shoes.";
        //logEvents[3] = "Kowalski tried to take my shoes.";
        //logEvents[4] = "I joined the punks.";
        ////just for testing***
        //eventAdder.onClick.AddListener(AddRandEvent);

    }


    public void UpdateLogText()
    {
        logText.text = output;
        GetComponent<ScrollRect>().content = logText.GetComponent<RectTransform>();
    }

    // Add new event to the notebook with given string.
    public void AddLogEvent(string logEvent)
    {
        logEvents.Add(logEvent);                            // Add the given string to the logEvent list.
        string tempOutput = "DAY ";                         // Start with "DAY..
        tempOutput += GameController.gameController.day;    // Add current day from gamecontroller
        tempOutput += "\n";                                 // linebreak
        tempOutput += logEvent;                             // Add given event
        tempOutput += "\n";                                 // linebreak
        tempOutput += output;                               // Include the previous events 
        output = tempOutput;                                // Update the old output with new
        UpdateLogText();                                    // Send it to the notebook
    }

    //public void AddLogEvent(int index)
    //{
    //    string tempOutput = "DAY ";
    //    tempOutput += GameController.gameController.day;
    //    tempOutput += "\n";
    //    tempOutput += logEvents[index];
    //    tempOutput += "\n";
    //    tempOutput += output;
    //    output = tempOutput;
    //    UpdateLogText();
    //}

    //just for testing***
    //void AddRandEvent()
    //{
    //    int a = Random.Range(0, logEvents.Length);
    //    AddLogEvent(a);
    //}


}
