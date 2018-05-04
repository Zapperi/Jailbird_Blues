using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using UnityEditor;

[CustomEditor(typeof(CardDisplay))]
public class CustomCardDisplay : Editor
{
    CardDisplay cd;

    public override void OnInspectorGUI()
    {

        cd = (CardDisplay)target;

        if (cd.currentCard)
        {
            cd.background.gameObject.SetActive(true);
            cd.foregroundImage2.gameObject.SetActive(true);
            cd.foregroundImage1.gameObject.SetActive(true);
            cd.foregroundImage3.gameObject.SetActive(true);
            cd.button1.gameObject.SetActive(true);
            cd.button2.gameObject.SetActive(true);
            cd.button3.gameObject.SetActive(true);
            cd.button4.gameObject.SetActive(true);

            cd.background.sprite = cd.currentCard.backgroundImage;
            cd.foregroundImage1.sprite = cd.currentCard.foregroundImage;
            cd.foregroundImage2.sprite = cd.currentCard.foregroundImage2;
            cd.foregroundImage3.sprite = cd.currentCard.foregroundImage3;

            cd.cardText.text = cd.currentCard.cardText;
            cd.button1text.text = cd.currentCard.option1text;
            cd.button2text.text = cd.currentCard.option2text;
            cd.button3text.text = cd.currentCard.option3text;
            cd.button4text.text = cd.currentCard.option4text;

            if (!cd.background.sprite)
                cd.background.gameObject.SetActive(false);
            if (!cd.foregroundImage1.sprite)
                cd.foregroundImage1.gameObject.SetActive(false);
            if (!cd.foregroundImage2.sprite)
                cd.foregroundImage2.gameObject.SetActive(false);
            if (!cd.foregroundImage3.sprite)
                cd.foregroundImage3.gameObject.SetActive(false);

            if (!cd.currentCard.Option1On)
                cd.button1.gameObject.SetActive(false);
            if (!cd.currentCard.Option2On)
                cd.button2.gameObject.SetActive(false);
            if (!cd.currentCard.Option3On)
                cd.button3.gameObject.SetActive(false);
            if (!cd.currentCard.Option4On)
                cd.button4.gameObject.SetActive(false);
            if (!cd.currentCard.OptionsOn)
                cd.button4text.text = "Continue...";
        }

        base.OnInspectorGUI();







    }




}
