using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    // --CARD VARIABLES--
    public CardValues card;
    private CardValues previousCard;
    public Text cardText;
    public Text button1text;
    public Text button2text;
    public Text button3text;
    public Text button4text;
    public Text buttonNoteBooktext;
    public Text location;               // name of the location where the event is, for example "Yard"
    public Button button1;
    public Button button2;
    public Button button3;
    public Button button4;
    public Button button5;
    public Button buttonNoteBook;
    public Image background;
    public Image foregroundImage1;
    public Image foregroundImage2;
    public Image foregroundImage3;
    public Image foregroundImage4;
    public Image fadeImage;
    public float fadeSpeed = 0.25f;         // set how fast the overlaying image fades in and out.
    public GameObject noteBook;
    private bool coroutineRunning;
	public static float textScrollSpeed;           // Used to control the speed of TypeText coroutine (text speed)
    private IEnumerator typeTextCoroutine;  // create coroutine variable, for stopping and starting.
    private bool continuebutton;

 


    void Start()
    {
        fadeImage.gameObject.SetActive(false);                      // At start, set the overlaiyng fade image to disabled
        buttonNoteBook.onClick.RemoveAllListeners();                // Make sure all buttons have default values
        buttonNoteBooktext.text = "NoteBook";
        buttonNoteBook.onClick.AddListener(buttonNotebookPressed);  // Connect notebook to it's button
        typeTextCoroutine = TypeText(card.cardText);                // Make sure the text scroll coroutine has something in it
        StartCoroutine(typeTextCoroutine);                          // Start TypeText coroutine as game opens

    }
    void Update()
    {
        card = GameController.gameController.currentCard;           // Update the current to a new one
        
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
        //cardText.text = card.cardText.Replace("\\n", "\n");
        

        //Update options's texts to current ones
        button1text.text = card.option1text;
        button2text.text = card.option2text;
        button3text.text = card.option3text;
        button4text.text = card.option4text;
        
        //Update current button functions
        button1.onClick.RemoveAllListeners();           // Make sure to remove old functions before adding new ones..
        button2.onClick.RemoveAllListeners();
        button3.onClick.RemoveAllListeners();
        button4.onClick.RemoveAllListeners();
        button5.onClick.RemoveAllListeners();
        button1.onClick.AddListener(button1pressed);    // ..Add new button functions with updated parameters
        button2.onClick.AddListener(button2pressed);
        button3.onClick.AddListener(button3pressed);
        button4.onClick.AddListener(button4pressed);
        button5.onClick.AddListener(button4pressed);

        //Check if card is a result card, if so, leave last button active and set the text.
        if (card.OptionsOn == false)
        {
            card.Option1On = false;
            card.Option2On = false;
            card.Option3On = false;
            card.Option4On = false;
            continuebutton = true;
        }

        //Activate the button gameobjects when needed, hide otherwise.
        if (card.Option1On == true && GameController.gameController.Check1Switches() == true)
            button1.gameObject.SetActive(true);
        else
            button1.gameObject.SetActive(false);
        if (card.Option2On == true && GameController.gameController.Check2Switches() == true)
            button2.gameObject.SetActive(true);
        else
            button2.gameObject.SetActive(false);
        if (card.Option3On == true && GameController.gameController.Check3Switches() == true)
            button3.gameObject.SetActive(true);
        else
            button3.gameObject.SetActive(false);
        if (card.Option4On == true && GameController.gameController.Check4Switches() == true)
            button4.gameObject.SetActive(true);
        else
            button4.gameObject.SetActive(false);
        if (continuebutton == true)
            button5.gameObject.SetActive(true);
        else
            button5.gameObject.SetActive(false);
        if(coroutineRunning == true)
            button5.gameObject.SetActive(false);
        else if(coroutineRunning == false && card.OptionsOn == false)
            button5.gameObject.SetActive(true);
    }

    //--BUTTON FUNCTIONS--
    void button1pressed()
    {
        GameController.gameController.Add1Switches();
        GameController.gameController.Remove1Switches();
        if (card.option1FollowCard)                                                // If card is not a result card, do this..
        {
            StopCoroutine(typeTextCoroutine);                                   // Stop ongoing coroutine, so they won't mix up
            StartCoroutine(FadeImage(fadeSpeed));                               // Fade in and out a overlay image and update card values under it.
            //Update the reputation values in gameController with current card values.
            GameController.gameController.UpdateReputations(card.option1IrsReputation, card.option1PunkReputation, card.option1ShakeReputation, card.option1GuardReputation);
            GameController.gameController.currentCard = card.option1FollowCard; // Update the next card into given card in gameController.
            card = card.option1FollowCard;                                      // Update the next card into given in cardDisplay.                                                         
            typeTextCoroutine = TypeText(card.cardText);                        // Update the text from new card
            StartCoroutine(typeTextCoroutine);                                  // Start printing the new text
        }
        else
        {                                                                       //If card is a result card, do this..
            //Update the reputation values in gameController with current card values.
            GameController.gameController.UpdateReputations(card.option1IrsReputation, card.option1PunkReputation, card.option1ShakeReputation, card.option1GuardReputation);
        }
    }
    void button2pressed()
    {
        GameController.gameController.Add2Switches();
        GameController.gameController.Remove2Switches();
        if (card.option2FollowCard)
        {
            StopCoroutine(typeTextCoroutine);
            StartCoroutine(FadeImage(fadeSpeed));
            GameController.gameController.UpdateReputations(card.option2IrsReputation, card.option2PunkReputation, card.option2ShakeReputation, card.option2GuardReputation);
            GameController.gameController.currentCard = card.option2FollowCard;
            card = card.option2FollowCard;
            typeTextCoroutine = TypeText(card.cardText);
            StartCoroutine(typeTextCoroutine);
        }
        else {
            GameController.gameController.UpdateReputations(card.option2IrsReputation, card.option2PunkReputation, card.option2ShakeReputation, card.option2GuardReputation);
        }
    }
    void button3pressed()
    {
        GameController.gameController.Add3Switches();
        GameController.gameController.Remove3Switches();
        if (card.option3FollowCard)
        {
            StopCoroutine(typeTextCoroutine);
            StartCoroutine(FadeImage(fadeSpeed));
            GameController.gameController.UpdateReputations(card.option3IrsReputation, card.option3PunkReputation, card.option3ShakeReputation, card.option3GuardReputation);
            GameController.gameController.currentCard = card.option3FollowCard;
            card = card.option3FollowCard;
            typeTextCoroutine = TypeText(card.cardText);
            StartCoroutine(typeTextCoroutine);
        }
        else {
            GameController.gameController.UpdateReputations(card.option3IrsReputation, card.option3PunkReputation, card.option3ShakeReputation, card.option3GuardReputation);
        }
    }
    void button4pressed()
    {
        GameController.gameController.Add4Switches();
        GameController.gameController.Remove4Switches();
        if (card.option4FollowCard)
        {
            StopCoroutine(typeTextCoroutine);            
            StartCoroutine(FadeImage(fadeSpeed));
            GameController.gameController.UpdateReputations(card.option4IrsReputation, card.option4PunkReputation, card.option4ShakeReputation, card.option4GuardReputation);
            GameController.gameController.currentCard = card.option4FollowCard;
            card = card.option4FollowCard;
            typeTextCoroutine = TypeText(card.cardText);
            StartCoroutine(typeTextCoroutine);
        }
        
        else if (card.endCard == true)                              // If the card is an end card (ends the event), do this..
        {
            GameController.gameController.UpdateReputations(card.option4IrsReputation, card.option4PunkReputation, card.option4ShakeReputation, card.option4GuardReputation);
            GameController.gameController.endcardOn = true;         // Update the boolean, ending the event. 
           
        }
    }
    void button5pressed()
    {

        GameController.gameController.Add4Switches();
        GameController.gameController.Remove4Switches();
        if (card.option4FollowCard)
        {
            StopCoroutine(typeTextCoroutine);
            StartCoroutine(FadeImage(fadeSpeed));
            GameController.gameController.UpdateReputations(card.option4IrsReputation, card.option4PunkReputation, card.option4ShakeReputation, card.option4GuardReputation);
            GameController.gameController.currentCard = card.option4FollowCard;
            card = card.option4FollowCard;
            typeTextCoroutine = TypeText(card.cardText);
            StartCoroutine(typeTextCoroutine);
        }

        else if (card.endCard == true)                              // If the card is an end card (ends the event), do this..
        {
            GameController.gameController.UpdateReputations(card.option4IrsReputation, card.option4PunkReputation, card.option4ShakeReputation, card.option4GuardReputation);
            GameController.gameController.endcardOn = true;         // Update the boolean, ending the event. 

        }

    }
    // Notebook button
    void buttonNotebookPressed()
    {
        if (noteBook.gameObject.activeSelf == false)                // If notebook is not active, set it to enabled.
        {
            noteBook.gameObject.SetActive(true);
            noteBook.GetComponent<NoteBook>().UpdateNotebook();
        }
        else
            noteBook.gameObject.SetActive(false);                   // If notebook is active, set it to disabled.
    }

    

    // Coroutine for image fade between cards, takes in time (float) as parameter.
    IEnumerator FadeImage(float time)                                       
    {
        fadeImage.gameObject.SetActive(true);                           // Enable the overlaying image.
        // fade from transparent to opaque
        for (float i = 0; i <= time; i += Time.deltaTime)               // Loop over "time" seconds.
        {
            fadeImage.color = new Color(0.25f, 0.25f, 0.25f, i);        // Set color with "i" as the alpha value.
            yield return null;                                          // Continue coroutine.
        }
        // fade from opaque to transparent
        for (float i = time; i >= 0; i -= Time.deltaTime)               // Loop over "time" seconds backwards.
        {
                fadeImage.color = new Color(0.25f, 0.25f, 0.25f, i);    // Set color with "i" as the alpha value.
            yield return null;                                          // Continue coroutine.
        }
            fadeImage.gameObject.SetActive(false);                      // Disable the overlaying image when fade in & out is completed.
    }

    // Coroutine for printing the card text letter by letter. Takes a text to print as parameter.
    IEnumerator TypeText(string textToType)
    {
        coroutineRunning = true;
        
        cardText.text = "";                                     // Start with empty text
        foreach (char letter in textToType.ToCharArray())       // Go through the given text and print it letter by letter 
        {
			if (Input.GetMouseButtonDown(0) || OptionsSliders.instatext)                    // If left mouse button is pressed while the text is printing...
            {
                cardText.text = card.cardText;                  // Instantly print all of the text
                break;                                          // Break out of the foreach loop, ending the coroutine
            }
            cardText.text += letter;
            yield return new WaitForSeconds(textScrollSpeed);   // Control the speed, 0 = by framerate                   
        }
        coroutineRunning = false;
        
    }
}

