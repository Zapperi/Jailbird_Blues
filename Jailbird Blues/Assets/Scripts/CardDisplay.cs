using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour {

    public CardValues card;

    public Text cardText;
    public Text button1text;
    public Text button2text;
    public Text button3text;
    public Text button4text;
    public Text location;
    public Button button1;
    public Button button2;
    public Button button3;
    public Button button4;
    public Sprite background;
    public Sprite npc;

    void Start()
    {
    }
    void Update () {

        cardText.text = card.cardText.Replace("\\n", "\n");
        button1text.text = card.option1;
        button2text.text = card.option2;
        button3text.text = card.option3;
        button4text.text = card.option4;

        Button option1 = button1.GetComponent<Button>();
        option1.onClick.AddListener(Button1Pressed);
        

    }
    void Button1Pressed()
    {
        if(card.option1FollowCard)
        card = card.option1FollowCard;
        else
            button1.gameObject.SetActive(false);
    }
	
}
