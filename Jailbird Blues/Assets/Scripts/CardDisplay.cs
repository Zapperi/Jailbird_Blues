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

        button1.onClick.AddListener(button1pressed);
        button2.onClick.AddListener(button2pressed);
        button3.onClick.AddListener(button3pressed);
        button4.onClick.AddListener(button4pressed);

        if (card.OptionsOn == false)
        {
            card.Option1On = false;
            card.Option2On = false;
            card.Option3On = false;
            card.Option4On = true;
            button4text.text = "Continue...";
        }

        if (card.Option1On == true)
        {
            button1.gameObject.SetActive(true);
        }
        else
        {
            button1.gameObject.SetActive(false);
        }

        if (card.Option2On == true)
        {
            button2.gameObject.SetActive(true);
        }
        else
        {
            button2.gameObject.SetActive(false);
        }

        if (card.Option3On == true)
        {
            button3.gameObject.SetActive(true);
        }
        else
        {
            button3.gameObject.SetActive(false);
        }

        if (card.Option4On == true)
        {
            button4.gameObject.SetActive(true);
        }
        else
        {
            button4.gameObject.SetActive(false);
        }
    }

	void button1pressed()
    {
        if (card.option1FollowCard)
        {
			GameController.gameController.UpdateReputations(card.option1IrsReputation, card.option1PunkReputation, card.option1ShakeReputation, card.option1GuardReputation);
            card = card.option1FollowCard;
        }
    }
    void button2pressed()
    {
        if (card.option2FollowCard)
        {
			GameController.gameController.UpdateReputations(card.option2IrsReputation, card.option2PunkReputation, card.option2ShakeReputation, card.option2GuardReputation);
            card = card.option2FollowCard;
        }
    }
    void button3pressed()
    {
        if (card.option3FollowCard)
        {
			GameController.gameController.UpdateReputations(card.option3IrsReputation, card.option3PunkReputation, card.option3ShakeReputation, card.option3GuardReputation);
            card = card.option3FollowCard;
        }
    }
    void button4pressed()
    {
        if (card.option4FollowCard)
        {
			GameController.gameController.UpdateReputations(card.option4IrsReputation, card.option4PunkReputation, card.option4ShakeReputation, card.option4GuardReputation);
            card = card.option4FollowCard;
        }
    }
}
