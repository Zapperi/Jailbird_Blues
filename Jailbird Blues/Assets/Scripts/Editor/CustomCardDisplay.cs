using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using UnityEditor;

[CustomEditor (typeof(CardDisplay))]
public class CustomCardDisplay : Editor {
    CardDisplay cd;

    public override void OnInspectorGUI()
    {
        
        cd = (CardDisplay)target;

        if (cd.card)
        {
            cd.background.gameObject.SetActive(true);
            cd.foregroundImage2.gameObject.SetActive(true);
            cd.foregroundImage1.gameObject.SetActive(true);
            cd.foregroundImage3.gameObject.SetActive(true);
            cd.foregroundImage4.gameObject.SetActive(true);
            cd.button1.gameObject.SetActive(true);
            cd.button2.gameObject.SetActive(true);
            cd.button3.gameObject.SetActive(true);
            cd.button4.gameObject.SetActive(true);

                cd.background.sprite = cd.card.backgroundImage;
                cd.foregroundImage1.sprite = cd.card.foregroundImage;
                cd.foregroundImage2.sprite = cd.card.foregroundImage2;
                cd.foregroundImage3.sprite = cd.card.foregroundImage3;
                cd.foregroundImage4.sprite = cd.card.foregroundImage4;

            cd.cardText.text = cd.card.cardText;
            cd.button1text.text = cd.card.option1text;
            cd.button2text.text = cd.card.option2text;
            cd.button3text.text = cd.card.option3text;
            cd.button4text.text = cd.card.option4text;

            if (!cd.background.sprite)
                cd.background.gameObject.SetActive(false);
            if (!cd.foregroundImage1.sprite)
                cd.foregroundImage1.gameObject.SetActive(false);
            if (!cd.foregroundImage2.sprite)
                cd.foregroundImage2.gameObject.SetActive(false);
            if (!cd.foregroundImage3.sprite)
                cd.foregroundImage3.gameObject.SetActive(false);
            if (!cd.foregroundImage4.sprite)
                cd.foregroundImage4.gameObject.SetActive(false);

            if (!cd.card.Option1On)
                cd.button1.gameObject.SetActive(false);
            if (!cd.card.Option2On)
                cd.button2.gameObject.SetActive(false);
            if (!cd.card.Option3On)
                cd.button3.gameObject.SetActive(false);
            if (!cd.card.Option4On)
                cd.button4.gameObject.SetActive(false);
            if (!cd.card.OptionsOn)
                cd.button4text.text = "Continue...";
        }

        base.OnInspectorGUI();







    }




}
