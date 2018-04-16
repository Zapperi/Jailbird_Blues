using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    // --CARD VARIABLES--
    public CardValues card;
    public Text cardText;
    public Text button1text;
    public Text button2text;
    public Text button3text;
    public Text button4text;
    public Text buttonNoteBooktext;
    public Text location;                   // name of the location where the event is, for example "Yard"
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
    public float fadeSpeed = 0.25f;         // Set how fast the overlaying image fades in and out.
    public GameObject noteBook;
    private bool typeTextRunning;           // Used to track if typeTextCoroutine is still running.
	public static float textScrollSpeed;    // Used to control the speed of TypeText coroutine (text speed)
    [HideInInspector]
    public IEnumerator typeTextCoroutine;   // Create coroutine variable, for stopping and starting.
    private bool continuebutton;

    public GameObject logPage;              // Reference to the notebook's log page, set in inspector


    public GameObject popUp;                // Reference to the popup element, set in inspector
    public bool popUpMouseOver;             // Track if mouse is over the popup element
    public Text popUpText;                  // Reference to the popup text, set in inspector

    public GameObject buttonPopUp1Img;
    public GameObject buttonPopUp2Img;
    public GameObject buttonPopUp3Img;
    public GameObject buttonPopUp4Img;
    public Text button1PopUpText;
    public Text button2PopUpText;
    public Text button3PopUpText;
    public Text button4PopUpText;
    public bool button1Hover;
    public bool button2Hover;
    public bool button3Hover;
    public bool button4Hover;
    public Image button1Img;
    public Image button2Img;
    public Image button3Img;
    public Image button4Img;

    public Item item;
    public InventoryPage invPag;


    void Start()
    {
        fadeImage.gameObject.SetActive(false);                      // At start, set the overlaiyng fade image to disabled
        buttonNoteBook.onClick.RemoveAllListeners();                // Make sure all buttons have default values
        buttonNoteBooktext.text = "NoteBook";                       // Set the notebook button text
        buttonNoteBook.onClick.AddListener(ButtonNotebookPressed);  // Connect notebook to it's button
        typeTextCoroutine = TypeText(card.cardText);                // Make sure the text scroll coroutine has something in it
        StartCoroutine(typeTextCoroutine);                          // Start TypeText coroutine as game opens

    }
    void Update()
    {
       
        //Call popUp function
        ShowPopUp();
        Button1PopUp();
        Button2PopUp();
        Button3PopUp();
        Button4PopUp();
        //If there was text in the previous card, add text to popup element.
        if (GameController.gameController.previousCard)
        {
            popUpText.fontSize = 15;
            popUpText.text = GameController.gameController.previousCard.cardText;
        }
        // If there was no text on previous card, type donuts instead.    
        else
        {
            popUpText.fontSize = 30;
            popUpText.text = "Mmmm... Donuts.";
        }



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
        button1.onClick.AddListener(Button1pressed);    // ..Add new button functions with updated parameters
        button2.onClick.AddListener(Button2pressed);
        button3.onClick.AddListener(Button3pressed);
        button4.onClick.AddListener(Button4pressed);
        button5.onClick.AddListener(Button5pressed);

        //Check if card is a result card, if so, leave last button active and set the text.
        if (card.OptionsOn == false)
        {
            card.Option1On = false;
            card.Option2On = false;
            card.Option3On = false;
            card.Option4On = false;
            continuebutton = true;
            button5.interactable = true; //option 4 disablee, tämä fixaa sen
        }
        else
            continuebutton = false;

        //Activate the button gameobjects when needed, hide otherwise.
        if (card.Option1On == true && GameController.gameController.Check1Switches() == true){
			button1.gameObject.SetActive (true);
			button1.interactable = true;
		}else if (card.Option1On == true && GameController.gameController.Check1Switches() == false){
			button1.gameObject.SetActive(true);
			button1.interactable = false;
            
        }
        else
            button1.gameObject.SetActive(false);

        if (card.Option2On == true && GameController.gameController.Check2Switches () == true) {
			button2.gameObject.SetActive (true);
			button2.interactable = true;
		}else if (card.Option2On == true && GameController.gameController.Check2Switches() == false){
			button2.gameObject.SetActive(true);
			button2.interactable = false;
        }
        else
            button2.gameObject.SetActive(false);

        if (card.Option3On == true && GameController.gameController.Check3Switches() == true){
			button3.gameObject.SetActive (true);
			button3.interactable = true;
		}else if (card.Option3On == true && GameController.gameController.Check3Switches() == false){
			button3.gameObject.SetActive(true);
			button3.interactable = false;
        }
        else
            button3.gameObject.SetActive(false);

        if (card.Option4On == true && GameController.gameController.Check4Switches() == true){
			button4.gameObject.SetActive (true);
			button4.interactable = true;
            button5.interactable = false;
		}else if (card.Option4On == true && GameController.gameController.Check4Switches() == false){
			button4.gameObject.SetActive(true);
			button4.interactable = false;     
        }
        else
            button4.gameObject.SetActive(false);

        if (continuebutton == true)
            button5.gameObject.SetActive(true);
        else if (typeTextRunning == false && card.OptionsOn == false)
            button5.gameObject.SetActive(true);
        else if (typeTextRunning == true)
        {
            button5.gameObject.SetActive(false);
        }
        else
        {
            button5.gameObject.SetActive(false);
        }
       
    }

    //--BUTTON FUNCTIONS--
    void Button1pressed()
    {
        GameController.gameController.Add1Switches();
        GameController.gameController.Remove1Switches();
        GameController.gameController.UpdateReputations(1);                     //Update reputations
        if (card.option1FollowCard)                                                // If card is not a result card, do this..
        {
            GameController.gameController.previousCard = card;                  // Updates gamecontroller's previous card to current card
            StopCoroutine(typeTextCoroutine);                                   // Stop ongoing coroutine, so they won't mix up
            StartCoroutine(FadeImage(fadeSpeed));                               // Fade in and out a overlay image and update card values under it.
            GameController.gameController.SetCurrentCard(1);
            card = card.option1FollowCard;                                      // Update the next card into given in cardDisplay.                                                         
            typeTextCoroutine = TypeText(card.cardText);                        // Update the text from new card
            StartCoroutine(typeTextCoroutine);                                  // Start printing the new text
        }

    }
    void Button2pressed()
    {
        GameController.gameController.Add2Switches();
        GameController.gameController.Remove2Switches();
        GameController.gameController.UpdateReputations(2);
        if (card.option2FollowCard)
        {
            GameController.gameController.previousCard = card;
            StopCoroutine(typeTextCoroutine);
            StartCoroutine(FadeImage(fadeSpeed));
            GameController.gameController.SetCurrentCard(2);
            card = card.option2FollowCard;
            typeTextCoroutine = TypeText(card.cardText);
            StartCoroutine(typeTextCoroutine);
        }
    }
    void Button3pressed()
    {
        GameController.gameController.Add3Switches();
        GameController.gameController.Remove3Switches();
        GameController.gameController.UpdateReputations(3);
        if (card.option3FollowCard)
        {
            GameController.gameController.previousCard = card;
            StopCoroutine(typeTextCoroutine);
            StartCoroutine(FadeImage(fadeSpeed));
            GameController.gameController.SetCurrentCard(3);
            card = card.option3FollowCard;
            typeTextCoroutine = TypeText(card.cardText);
            StartCoroutine(typeTextCoroutine);
        }
    }
    void Button4pressed()
    {
        GameController.gameController.Add4Switches();
        GameController.gameController.Remove4Switches();
        GameController.gameController.UpdateReputations(4);
        if (card.option4FollowCard)
        {
            GameController.gameController.previousCard = card;
            StopCoroutine(typeTextCoroutine);            
            StartCoroutine(FadeImage(fadeSpeed));
            GameController.gameController.SetCurrentCard(4);
            card = card.option4FollowCard;
            typeTextCoroutine = TypeText(card.cardText);
            StartCoroutine(typeTextCoroutine);
        }
    }
    void Button5pressed()
    {
        GameController.gameController.Add4Switches();
        GameController.gameController.Remove4Switches();
        GameController.gameController.UpdateReputations(4);
        if (card.option4FollowCard)
        {
            GameController.gameController.previousCard = card;
            StopCoroutine(typeTextCoroutine);
            StartCoroutine(FadeImage(fadeSpeed));
            GameController.gameController.SetCurrentCard(4);
            card = card.option4FollowCard;
            typeTextCoroutine = TypeText(card.cardText);
            StartCoroutine(typeTextCoroutine);
        }
        else if (card.endCard == true || card.option4FollowCard == null && card.Option4On)                              // If the card is an end card (ends the event), do this..
        {
            GameController.gameController.endcardOn = true;         // Update the boolean, ending the event. 
        }
    }
    // Notebook button
    void ButtonNotebookPressed()
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
        if (OptionsSliders.instatext)                               // If text is set to instant in options..
            cardText.text = card.cardText;                          // Just replace old text with new and skip the rest.
        else
        {
            typeTextRunning = true;                                 // Trigger tracker at the start of the type text..

            cardText.text = "";                                     // Start with empty text
            foreach (char letter in textToType.ToCharArray())       // Go through the given text and print it letter by letter 
            {
                if (Input.GetMouseButtonDown(0)) // If left mouse button is pressed while the text is printing...
                {
                    cardText.text = card.cardText;                  // Instantly print all of the text
                    break;                                          // Break out of the foreach loop, ending the coroutine
                }
                cardText.text += letter;
                yield return new WaitForSeconds(textScrollSpeed);   // Control the speed, 0 = by framerate                   
            }
            typeTextRunning = false;                                // ...Reset the tracker at the end
        }
        
        
    }

    // Function for pop up element
    private void ShowPopUp()
    {
        if (popUpMouseOver)             // If mouse is over the element...
            popUp.SetActive(true);      // .. set the element to active.
        else
         popUp.SetActive(false);        // Otherwise disable the element.      
    }

    //optionbutton required items popup
    public void Button1PopUp()
    {
        if (button1Hover == true && card.option1ReqSwitches.Count != 0)         //when hovering
        {
            buttonPopUp1Img.SetActive(true);
            button1PopUpText.text = card.option1ReqSwitches[0].ToString();      //print switch number
            for (int i = 0; i <= invPag.itemList.Count; i++)                    //loop whole inventory
            {
                item = invPag.itemList[i];                          
                if (item.itemSwitchIndex == card.option1ReqSwitches[0])         //find right item from all items
                {
                    button1Img.sprite = item.itemIcon;                          //print switch image
                    break;
                }
            }
            
        }
        else
            buttonPopUp1Img.SetActive(false);
    }
    //optionbutton required items popup
    public void Button2PopUp()
    {
        if (button2Hover == true && card.option2ReqSwitches.Count != 0)          //when hovering
        {
            buttonPopUp2Img.SetActive(true);
            button2PopUpText.text = card.option2ReqSwitches[0].ToString();      //print switch number
            for (int i = 0; i <= invPag.itemList.Count; i++)                    //loop whole inventory
            {
                item = invPag.itemList[i];
                if (item.itemSwitchIndex == card.option2ReqSwitches[0])         //find right item from all items
                {
                    button2Img.sprite = item.itemIcon;                          //print switch image
                    if (GameController.gameController.Check1Switches() == false)
                    {
                        button2Img.color = new Color32(90, 90, 90, 255);
                    }
                    break;
                }
            }
        }
        else
            buttonPopUp2Img.SetActive(false);
    }
    //optionbutton required items popup
    public void Button3PopUp()
    {
        if (button3Hover == true && card.option3ReqSwitches.Count != 0)          //when hovering
        {
            buttonPopUp3Img.SetActive(true);
            button3PopUpText.text = card.option3ReqSwitches[0].ToString();      //print switch number
            for (int i = 0; i <= invPag.itemList.Count; i++)                    //loop whole inventory
            {
                item = invPag.itemList[i];
                if (item.itemSwitchIndex == card.option3ReqSwitches[0])         //find right item from all items
                {
                    button3Img.sprite = item.itemIcon;                          //print switch image
                    break;
                }
            }
        }
        else
            buttonPopUp3Img.SetActive(false);
    }
    //optionbutton required items popup
    public void Button4PopUp()
    {
        if (button4Hover == true && card.option4ReqSwitches.Count != 0)          //when hovering
        {
            buttonPopUp4Img.SetActive(true);
            button4PopUpText.text = card.option4ReqSwitches[0].ToString();      //print switch number
            for (int i = 0; i <= invPag.itemList.Count; i++)                    //loop whole inventory
            {
                item = invPag.itemList[i];
                if (item.itemSwitchIndex == card.option4ReqSwitches[0])         //find right item from all items
                {
                    button4Img.sprite = item.itemIcon;                          //print switch image
                    break;
                }
            }
        }
        else
            buttonPopUp4Img.SetActive(false);
    }
    
}

