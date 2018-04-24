using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogScript : MonoBehaviour {

    public Text logText;



    public void UpdateLogText()
    {
        logText.text = GameController.gameController.logText;
        //GetComponent<ScrollRect>().content = logText.GetComponent<RectTransform>();
    }

}
