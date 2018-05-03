using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GainsTextHandler : MonoBehaviour {
    private static GainTextGeneration floatingText;
    private static GameObject canvas;

    public static void Initialize()                                                         // Sets up information for the text object.
    {
        canvas = GameObject.Find("Canvas");                                                 // Find the canvas, later used for it's location.
        if (!floatingText)                                                                  // If there is no text yet..
            floatingText = Resources.Load<GainTextGeneration>("Prefabs/GainTextParent");    // Go find the prefab from the resources that contains the elements.
    }

    public static void CreateGainsText(int[] repAmount, Transform location)                     // Creates floating text object
    {
        bool somethingToPrint;
        somethingToPrint = false;
        for(int i = 0; i < repAmount.Length; i++)
        {
            if (repAmount[i] != 0)
                somethingToPrint = true;
        }
        if (!somethingToPrint)
            return;
        GainTextGeneration instance = Instantiate(floatingText);                            // Create object from the information gotten from initialize.   
        string textToSend ="";
        instance.transform.SetParent(canvas.transform, false);                              // Set the new location to be placed in canvas, disable scaling from canvas.
        Tools.ResetLocation(instance.transform);                                            // Reset the new location values to correspond their real values (skewed due to camera).

        // Go throught all the reputation amounts given, if it's positive, paint it screen. Otherwise red. Do nothing if no reputation change.
        if (repAmount[0] > 0)
            textToSend = (textToSend + "<color=green>IRS +" + repAmount[0] + "</color>\n");
        if (repAmount[1] > 0)
            textToSend = (textToSend + "<color=green>Punks +" + repAmount[1] + "</color>\n");
        if (repAmount[2] > 0)
            textToSend = (textToSend + "<color=green>Shakers +" + repAmount[2] + "</color>\n");
        if (repAmount[3] >0)
            textToSend = (textToSend + "<color=green>Guards +" + repAmount[3] + "</color>\n");
        if (repAmount[0] < 0)
            textToSend = (textToSend + "<color=red>IRS " + repAmount[0] + "</color>\n");
        if (repAmount[1] < 0)
            textToSend = (textToSend + "<color=red>Punks " + repAmount[1] + "</color>\n");
        if (repAmount[2] < 0)
            textToSend = (textToSend + "<color=red>Shakers " + repAmount[2] + "</color>\n");
        if (repAmount[3] < 0)
            textToSend = (textToSend + "<color=red>Guards " + repAmount[3] + "</color>\n");
        instance.SetText(textToSend);                                                             // Send compiled text to the gainsText.
    }
}
