using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour {

    public CardValues card;

    public Text cardText;
    public Text button1;
    public Text button2;
    public Text button3;
    public Text button4;

    void Start () {

        cardText.text = card.cardText;
        button1.text = card.option1;
        button2.text = card.option2;
        button3.text = card.option3;
        button4.text = card.option4;

    }
	
}
