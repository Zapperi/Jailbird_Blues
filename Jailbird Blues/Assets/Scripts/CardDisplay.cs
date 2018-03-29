using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{

    public CardValues card;

    public Text cardText;
    public Text button1text;
    public Text button2text;
    public Text button3text;
    public Text button4text;
    public Text buttonNoteBooktext;
    public Text location;
    public Button button1;
    public Button button2;
    public Button button3;
    public Button button4;
    public Button buttonNoteBook;
    public Image background;
    public Image foregroundImage1;
    public Image foregroundImage2;
    public Image foregroundImage3;
    public Image foregroundImage4;
    public Image fadeImage;
    public float fadeSpeed = 0.25f;
    public GameObject noteBook;

    void Start()
    {
        fadeImage.gameObject.SetActive(false);
        buttonNoteBook.onClick.RemoveAllListeners();
        buttonNoteBooktext.text = "NoteBook";
        buttonNoteBook.onClick.AddListener(buttonNotebookPressed);


    }
    void Update()
    {

        card = GameController.gameController.currentCard;
        // Update the card images from the current card
        background.sprite = card.backgroundImage;
        foregroundImage1.sprite = card.foregroundImage;
        foregroundImage2.sprite = card.foregroundImage2;
        foregroundImage3.sprite = card.foregroundImage3;
        foregroundImage4.sprite = card.foregroundImage4;
        
        // IF there is no image, hide the field. Otherwise show the new image
        if (!foregroundImage1.sprite)
            foregroundImage1.gameObject.SetActive(false);
        else
            foregroundImage1.gameObject.SetActive(true);
        if (!foregroundImage2.sprite)
            foregroundImage2.gameObject.SetActive(false);
        else
            foregroundImage2.gameObject.SetActive(true);
        if (!foregroundImage3.sprite)
            foregroundImage3.gameObject.SetActive(false);
        else
            foregroundImage3.gameObject.SetActive(true);
        if (!foregroundImage4.sprite)
            foregroundImage4.gameObject.SetActive(false);
        else
            foregroundImage4.gameObject.SetActive(true);
        
        //Replace any \n in card's text string with line break
        cardText.text = card.cardText.Replace("\\n", "\n");

        //Update options's texts to current ones
        button1text.text = card.option1;
        button2text.text = card.option2;
        button3text.text = card.option3;
        button4text.text = card.option4;
        

        //Update current button functions
        button1.onClick.RemoveAllListeners();
        button2.onClick.RemoveAllListeners();
        button3.onClick.RemoveAllListeners();
        button4.onClick.RemoveAllListeners();
        button1.onClick.AddListener(button1pressed);
        button2.onClick.AddListener(button2pressed);
        button3.onClick.AddListener(button3pressed);
        button4.onClick.AddListener(button4pressed);
        

        //Check if card is result card, if so, leave last button active and set the text.
        if (card.OptionsOn == false)
        {
            card.Option1On = false;
            card.Option2On = false;
            card.Option3On = false;
            card.Option4On = true;
            button4text.text = "Continue...";
        }
        //Activate the button gameobjects when needed, hide otherwise.
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


    //--Button functions--
    //All the magic happens here~
    void button1pressed()
    {
        //If card is not a result card, do this..
        if (card.option1FollowCard)
        {     
            StartCoroutine(FadeImage(fadeSpeed));
            //Update the reputation values in gameController with current card values.
            GameController.gameController.UpdateReputations(card.option1IrsReputation, card.option1PunkReputation, card.option1ShakeReputation, card.option1GuardReputation);
            //Setup the result card.
            GameController.gameController.currentCard = card.option1FollowCard;
            card = card.option1FollowCard;            
        }
        else {
            GameController.gameController.UpdateReputations(card.option1IrsReputation, card.option1PunkReputation, card.option1ShakeReputation, card.option1GuardReputation);
        }
        //If card is a result card, do this..
    }
    void button2pressed()
    {
        //If card is not a result card, do this..
        if (card.option2FollowCard)
        {
            StartCoroutine(FadeImage(fadeSpeed));
            GameController.gameController.UpdateReputations(card.option2IrsReputation, card.option2PunkReputation, card.option2ShakeReputation, card.option2GuardReputation);
            GameController.gameController.currentCard = card.option2FollowCard;         
            card = card.option2FollowCard;
        }
        else {
            GameController.gameController.UpdateReputations(card.option2IrsReputation, card.option2PunkReputation, card.option2ShakeReputation, card.option2GuardReputation);
        }
        //If card is a result card, do this..
    }
    void button3pressed()
    {
        //If card is not a result card, do this..
        if (card.option3FollowCard)
        {
            StartCoroutine(FadeImage(fadeSpeed));
            GameController.gameController.UpdateReputations(card.option3IrsReputation, card.option3PunkReputation, card.option3ShakeReputation, card.option3GuardReputation);
            GameController.gameController.currentCard = card.option3FollowCard;
            card = card.option3FollowCard;
        }
        else {
            GameController.gameController.UpdateReputations(card.option3IrsReputation, card.option3PunkReputation, card.option3ShakeReputation, card.option3GuardReputation);
        }
        //If card is a result card, do this..
    }
    void button4pressed()
    {

        //If card is not a result card, do this..
        if (card.option4FollowCard)
        {
            StartCoroutine(FadeImage(fadeSpeed));
            GameController.gameController.UpdateReputations(card.option4IrsReputation, card.option4PunkReputation, card.option4ShakeReputation, card.option4GuardReputation);
            GameController.gameController.currentCard = card.option4FollowCard; 
            card = card.option4FollowCard;
        }
        
        else if (card.endCard == true)
        {
            GameController.gameController.UpdateReputations(card.option4IrsReputation, card.option4PunkReputation, card.option4ShakeReputation, card.option4GuardReputation);
            GameController.gameController.endcardOn = true;
           
        }
        
        //If card is a result card, do this..
    }
    void buttonNotebookPressed()
    {
        if (noteBook.gameObject.activeSelf == false)
        {
            noteBook.gameObject.SetActive(true);
            
        }
        else
        {
            noteBook.gameObject.SetActive(false);
            
        }
         
    }

    //Coroutine for image fade between cards, takes in time (float) as parameter
    IEnumerator FadeImage(float time)                                       
    {
        fadeImage.gameObject.SetActive(true);                           //Activate the overlaying image
        // fade from transparent to opaque
        for (float i = 0; i <= time; i += Time.deltaTime)               // loop over time second
        {
            fadeImage.color = new Color(0.25f, 0.25f, 0.25f, i);        // set color with i as alpha
            yield return null;                                          // Continue coroutine
        }
        // fade from opaque to transparent
        for (float i = time; i >= 0; i -= Time.deltaTime)               // loop over time second backwards
        {
                fadeImage.color = new Color(0.25f, 0.25f, 0.25f, i);    // set color with i as alpha
            yield return null;                                          // Continue coroutine
        }
            fadeImage.gameObject.SetActive(false);                      //Deactivate the overlaying image
    }
}

