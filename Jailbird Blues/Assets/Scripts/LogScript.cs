using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogScript : MonoBehaviour {

    private string output;
    public Text logText;
    public string[] logEvents;
    //just for testing***
    public Button eventAdder;

	// Use this for initialization
	void Start () {
        output = "ssdssds\nertyuio\nwertyui\nertyui\nertyuio\nrtyui\ndghjg\ngfhjk\nfhkj\nfhkj\nfjh\nfj\nfjvf\ndhgk\nfjh\nfgj\nfhj\nfh\nfhj\nfjh\ng\nfh\ngj\ndgj\nfhj\ngfg\nfg\nfg\nf\nfgh\nfh\nty\nfghjk\njkj\nloppu!";
        logText.text = output;

        //initialization of the list of log events

        logEvents[0] = "I enetered the prison.";
        logEvents[1]= "I met Kowalski. Nice fellow.";
        logEvents[2] = "Kowalski took my shoes.";
        logEvents[3] = "Kowalski tried to take my shoes.";
        logEvents[4] = "I joined the punks.";
        //just for testing***
        eventAdder.onClick.AddListener(AddRandEvent);

    }


    public void UpdateLogText()
    {
        logText.text = output;
        GetComponent<ScrollRect>().content = logText.GetComponent<RectTransform>();
    }

    public void AddLogEvent(int logEvent)
    {
        string tempOutput = "DAY ";
        tempOutput += GameController.gameController.day;
        tempOutput += "\n";
        tempOutput += logEvents[logEvent];
        tempOutput += "\n";
        tempOutput += output;
        output = tempOutput;
        UpdateLogText();
    }

    //just for testing***
    void AddRandEvent()
    {
        int a = Random.Range(0, logEvents.Length);
        AddLogEvent(a);
    }
	

}
