using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    // --CARD VARIABLES--
    [HideInInspector]
    public Image overLayingImage;           // Transparent image that can be placed on top of everything, preventing player's interaction.
    public CardValues currentCard;
    public Text cardText;
    public Text cardPerson;
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
    [HideInInspector]
    public bool typeTextNewTextDone;
    private string coloredText;
    private bool coloringTextDone;
    public Color highlightColor;            // Color for the highlighted tips. 
    private string highlightColorHex;       // Save the color's hex into string, used later

    public GameObject logPage;              // Reference to the notebook's log page, set in inspector
    public GameObject inventoryPage;

    public GameObject popUp;                // Reference to the popup element, set in inspector
    public bool popUpMouseOver;             // Track if mouse is over the popup element
    public Text popUpText;                  // Reference to the popup text, set in inspector

    //Optionbutton required switches
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
    public Image cross1;
    public Image cross2;
    public Image cross3;
    public Image cross4;

    public Item item;
    public InventoryPage invPag;

	int siblingIndexOne;
	int siblingIndexTwo;


    void Awake()
    {
        overLayingImage = GameObject.Find("PlayerBlocker").GetComponent<Image>();   // At scene launch, get reference to the overLayingImage from scene. 
        overLayingImage.gameObject.SetActive(false);                                // Deactivate the gameobject, because it had to be active in order to find it.
        highlightColorHex = ColorUtility.ToHtmlStringRGB(highlightColor);
    }

    void Start()
    {     
        fadeImage.gameObject.SetActive(false);                                      // At start, set the overlaiyng fade image to disabled.
        buttonNoteBook.onClick.RemoveAllListeners();                                // Make sure all buttons have default values.
        buttonNoteBooktext.text = "NoteBook";                                       // Set the notebook button text.
        buttonNoteBook.onClick.AddListener(ButtonNotebookPressed);                  // Connect notebook to it's button.
		siblingIndexOne = foregroundImage1.transform.GetSiblingIndex();             // Set the reference to foregroundimage1 order.
		siblingIndexTwo = foregroundImage2.transform.GetSiblingIndex();             // Set the reference to foregroundimage2 order.
        UpdateTypeText();                                                           //Set text scrolling
    }
		
    void Update()
    {
       
        //Call popUp functions, if mouse is over the element, activate the popUp.
        ShowPopUp();
        Button1PopUp();
        Button2PopUp();
        Button3PopUp();
        Button4PopUp();

        UpdatePopUpField();                                         // Update the previouscard text pop up field.
        currentCard = GameController.gameController.currentCard;    // Update the current to a new one
        SiblingIndexSwitch();							            // Switches foreground images if needed.
        UpdateImages();                                             // Update to new images, hide the field if there is no image.
        RefreshTextFields();                                        // Update the textfield to current ones, does NOT affect cardText.
        UpdateButtonFunctions();                                    // Update the CardDisplay button listeners.
        RefreshOptions();                                           // Refresh options, if options are off, enable option 5.
        EnableButtons();                                            // Enables and Disables button according to options.
    }

    //--BUTTON FUNCTIONS--

    public void ButtonPressed(int button)
    {
        GameController.gameController.AddSwitches(button);
        GameController.gameController.RemoveSwitches(button);
        GameController.gameController.UpdateReputations(button);
        GameController.gameController.PrintReputations(button);
        inventoryPage.GetComponent<InventoryPage>().RefreshInventory();     // Refresh the inventory by removing everything and adding them back with updated values.
        bool followUp = false;
        if (button == 1 && currentCard.option1FollowCard)
        {
            followUp = true;
        }
        if (button == 2 && currentCard.option2FollowCard)
        {
            followUp = true;
        }
        if (button == 3 && currentCard.option3FollowCard)
        {
            followUp = true;
        }
        if (button == 4 && currentCard.option4FollowCard)
        {
            followUp = true;
        }
        if (button == 5 && currentCard.option5FollowCard)
        {
            followUp = true;
        }
        if (followUp)
        {
            GameController.gameController.SetCurrentCard(button);
        } else
        {
            GameController.gameController.endcardOn = true;                 //Just a precaution to avoid null pointer
        }
    }

    public void StartTextCoroutine()
    {
        if (typeTextRunning)                                        // Failsafe, incase two typetexts were called at the sametime.
        {
            StopCoroutine(typeTextCoroutine);                       // Stop currently running typetext..
            cardText.text = "";                                     // ..replace the text with blank   
            typeTextRunning = false;
        }
        currentCard = GameController.gameController.currentCard;    // Get reference to the new card from gamecontroller
        typeTextNewTextDone = false;                                // trigger the donetyping boolean to false
        typeTextCoroutine = TypeText(currentCard.cardText);         // Update the text from new card
        StartCoroutine(typeTextCoroutine);                          // Start printing the new text
    }

    // This is a variant of the typetext that can be used in the Update()
    private void UpdateTypeText()                           // Starts a new TypeTextCoroutine as long as there is something new to type AND there are no current texts running
    {
        if (!typeTextNewTextDone && !typeTextRunning)
        {
            cardText.text = "";
            typeTextCoroutine = TypeText(currentCard.cardText);                        // Update the text from new card
            StartCoroutine(typeTextCoroutine);                                  // Start printing the new text
        }
    }

    // Button pressed calls, must go through here in order for the switch case to work.
    private void Button1pressed()
    {
        ButtonPressed(1);
    }
    private void Button2pressed()
    {
        ButtonPressed(2);
    }
    private void Button3pressed()
    {
        ButtonPressed(3);
    }
    private void Button4pressed()
    {
        ButtonPressed(4);
    }
    private void Button5pressed()
    {
        if (currentCard.endCard == true || currentCard.option5FollowCard == null && currentCard.Option5On)
        {
            ButtonPressed(5);
            GameController.gameController.endcardOn = true;         // Update the boolean, ending the event.
        }
        if (currentCard.option5FollowCard)
            ButtonPressed(5);
    }
    
    // Notebook button
    void ButtonNotebookPressed()
    {
        if (noteBook.gameObject.activeSelf == false)                // If notebook is not active, set it to enabled.
        {
            noteBook.gameObject.SetActive(true);
            noteBook.GetComponent<NoteBook>().UpdateNotebook();
            inventoryPage.GetComponent<InventoryPage>().RefreshInventory();
        }
        else
            noteBook.gameObject.SetActive(false);                   // If notebook is active, set it to disabled.
    }

    // Coroutine for printing the card text letter by letter. Takes a text to print as parameter.
    IEnumerator TypeText(string textToType)
    {
        coloringTextDone = true;
        if (OptionsSliders.instatext)
        {                               // If text is set to instant in options..
            cardText.text = currentCard.cardText;                   // Find new text
            coloredText = cardText.text.Replace("Ä", "<color=#"+highlightColorHex+">"); // Make sure the highlighted text gets the color
            coloredText = coloredText.Replace("Ö", "</color>");                         // End the coloring area
            cardText.text = coloredText;                                                // Update the text

        }
        else
        {
            typeTextRunning = true;                                 // Trigger runnign boolean at the start of the type text..
            cardText.text = "";                                     // Start with empty text.
            foreach (char letter in textToType.ToCharArray())       // Go through the given text and print it letter by letter .
            {
                if (Input.GetMouseButtonDown(0) && typeTextRunning)                    // If left mouse button is pressed while the text is printing...
                {
                    cardText.text = currentCard.cardText;                                           // Instantly print all of the text.                
                    coloredText = cardText.text.Replace("Ä", "<color=#" + highlightColorHex + ">"); // Make sure the highlighted text gets the color
                    coloredText = coloredText.Replace("Ö", "</color>");                         // End the coloring area
                    cardText.text = coloredText;                                                    // Update the text

                    break;                                          // Break out of the foreach loop, ending the coroutine.
                }
                if (letter == 'Ä')                                  // If the keyletter "Ä" appears..
                    coloringTextDone = false;                       // Start the coloring sequence by setting boolean to false
                else if (letter == 'Ö')                             // If the keyletter "Ö" appears..
                    coloringTextDone = true;                        // End the coloring sequence.
                else if (!coloringTextDone)                         // As long as coloring sequence is running..
                    cardText.text += "<color=#" + highlightColorHex + ">" + letter + "</color>"; // Color each letter one by one.
                else
                    cardText.text += letter;                        // If no coloring sequence, continue writing in default color.
                yield return new WaitForSeconds(textScrollSpeed);   // Control the speed, 0 = by framerate.                   
            }
            typeTextRunning = false;                                // ...Reset the running boolean.
        }
        typeTextNewTextDone = true;                                 // When done typing, trigger the boolean.
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
        if (button1Hover == true && currentCard.option1ReqSwitches.Count != 0)         //when hovering
        {
            buttonPopUp1Img.SetActive(true);
            button1PopUpText.text = currentCard.option1ReqSwitches[0].ToString();      //print switch number
            for (int i = 0; i <= invPag.itemList.Count; i++)                    //loop whole inventory
            {
                item = invPag.itemList[i];
                if (item.itemSwitchIndex == currentCard.option1ReqSwitches[0])         //find right item from all items
                {
                    button1Img.sprite = item.itemIcon;                          //print switch image
                    if (GameController.gameController.Check1Switches() == false)       // checks if you have required switches
                    {
                        Debug.Log("option 1 cross enabled");
                        cross1.enabled = true;                                      //if you dont have them activate red cross
                    }
                    else
                    {
                        Debug.Log("option 1 cross disabled");
                        cross1.enabled = false;                                     // else deactivate
                    }
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
        if (button2Hover == true && currentCard.option2ReqSwitches.Count != 0)          //when hovering
        {
            buttonPopUp2Img.SetActive(true);
            button2PopUpText.text = currentCard.option2ReqSwitches[0].ToString();      //print switch number
            for (int i = 0; i <= invPag.itemList.Count; i++)                    //loop whole inventory
            {
                item = invPag.itemList[i];
                if (item.itemSwitchIndex == currentCard.option2ReqSwitches[0])         //find right item from all items
                {
                    button2Img.sprite = item.itemIcon;                          //print switch image
                    if (GameController.gameController.Check2Switches() == false)       // checks if you have required switches
                    {
                        cross2.enabled = true;                                      //if you dont have them activate red cross
                    }
                    else
                    {
                        cross2.enabled = false;                                     // else deactivate
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
        if (button3Hover == true && currentCard.option3ReqSwitches.Count != 0)          //when hovering
        {
            buttonPopUp3Img.SetActive(true);
            button3PopUpText.text = currentCard.option3ReqSwitches[0].ToString();      //print switch number
            for (int i = 0; i <= invPag.itemList.Count; i++)                    //loop whole inventory
            {
                item = invPag.itemList[i];
                if (item.itemSwitchIndex == currentCard.option3ReqSwitches[0])         //find right item from all items
                {
                    button3Img.sprite = item.itemIcon;                          //print switch image
                    if (GameController.gameController.Check3Switches() == false)       // checks if you have required switches
                    {
                        Debug.Log("option3cross enabled");
                        cross3.enabled = true;                                      //if you dont have them activate red cross
                    }
                    else
                    {
                        Debug.Log("option3cross disabled");
                        cross3.enabled = false;                                     // else deactivate
                    }
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
        if (button4Hover == true && currentCard.option4ReqSwitches.Count != 0)          //when hovering
        {
            buttonPopUp4Img.SetActive(true);
            button4PopUpText.text = currentCard.option4ReqSwitches[0].ToString();      //print switch number
            for (int i = 0; i <= invPag.itemList.Count; i++)                    //loop whole inventory
            {
                item = invPag.itemList[i];
                if (item.itemSwitchIndex == currentCard.option4ReqSwitches[0])         //find right item from all items
                {
                    button4Img.sprite = item.itemIcon;                          //print switch image
                    if (GameController.gameController.Check4Switches() == false)       // checks if you have required switches
                    {
                        cross4.enabled = true;                                      //if you dont have them activate red cross
                    }
                    else
                    {
                        cross4.enabled = false;                                     // else deactivate
                    }
                    break;
                }
            }
        }
        else
            buttonPopUp4Img.SetActive(false);
    }

    public void BlockButtons()                                    // Function that disables all player interaction by using overlayingimage
    {
        overLayingImage.color = new Color(0.25f, 0.25f, 0.25f, 0);  // Failsafe, make sure it is transparent
        overLayingImage.gameObject.SetActive(true);                 // Disable interaction by enabling the transparent image
    }
    public void UnblockButtons()                                     // Function that re-enables player interaction
    {
        overLayingImage.color = new Color(0.25f, 0.25f, 0.25f, 0);  // Failsafe, keep making sure it is transparent.
        overLayingImage.gameObject.SetActive(false);                // Disable the transparent image.
    }

	public void SiblingIndexSwitch(){								//switches the layer order of foreground images 1 and 2
		if (!currentCard.onTop) {
			foregroundImage1.transform.SetSiblingIndex (siblingIndexOne);
			foregroundImage2.transform.SetSiblingIndex (siblingIndexTwo);
		} else {
			foregroundImage1.transform.SetSiblingIndex (siblingIndexTwo);
			foregroundImage2.transform.SetSiblingIndex (siblingIndexOne);
		}
	}

    public void UpdateImages()   // Function that updates the card images, hides if there is nothing to show.
    {
        background.sprite = currentCard.backgroundImage;
        foregroundImage1.sprite = currentCard.foregroundImage;
        foregroundImage2.sprite = currentCard.foregroundImage2;
        foregroundImage3.sprite = currentCard.foregroundImage3;
        foregroundImage4.sprite = currentCard.foregroundImage4;

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
    }

    public void RefreshTextFields()                     // Function that refreshes the CardDisplay's text fields.
    {
        button1text.text = currentCard.option1text;
        button2text.text = currentCard.option2text;
        button3text.text = currentCard.option3text;
        button4text.text = currentCard.option4text;
        cardPerson.text = currentCard.cardTextPerson;
    }

    private void UpdatePopUpField()                  // Function that updates the previous card text pop up field.
    {
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
    }

    public void UpdateButtonFunctions()
    {
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
    }

    public void RefreshOptions()
    {
        //Check if card is a result card, if so, leave last button active and set the text.
        if (currentCard.OptionsOn == false)
        {
            currentCard.Option1On = false;
            currentCard.Option2On = false;
            currentCard.Option3On = false;
            currentCard.Option4On = false;
            currentCard.Option5On = true;
            button5.interactable = true; //option 4 disablee, tämä fixaa sen
        }
        else
            currentCard.Option5On = false;
    }

    public void EnableButtons()
    {
        //Activate the button gameobjects when needed, hide otherwise.
        if (currentCard.Option1On == true && GameController.gameController.Check1Switches() == true)
        {
            button1.gameObject.SetActive(true);
            button1.interactable = true;
        }
        else if (currentCard.Option1On == true && GameController.gameController.Check1Switches() == false)
        {
            button1.gameObject.SetActive(true);
            button1.interactable = false;
        }
        else
            button1.gameObject.SetActive(false);

        if (currentCard.Option2On == true && GameController.gameController.Check2Switches() == true)
        {
            button2.gameObject.SetActive(true);
            button2.interactable = true;
        }
        else if (currentCard.Option2On == true && GameController.gameController.Check2Switches() == false)
        {
            button2.gameObject.SetActive(true);
            button2.interactable = false;
        }
        else
            button2.gameObject.SetActive(false);

        if (currentCard.Option3On == true && GameController.gameController.Check3Switches() == true)
        {
            button3.gameObject.SetActive(true);
            button3.interactable = true;
        }
        else if (currentCard.Option3On == true && GameController.gameController.Check3Switches() == false)
        {
            button3.gameObject.SetActive(true);
            button3.interactable = false;
        }
        else
            button3.gameObject.SetActive(false);

        if (currentCard.Option4On == true && GameController.gameController.Check4Switches() == true)
        {
            button4.gameObject.SetActive(true);
            button4.interactable = true;
            button5.interactable = false;
        }
        else if (currentCard.Option4On == true && GameController.gameController.Check4Switches() == false)
        {
            button4.gameObject.SetActive(true);
            button4.interactable = false;
        }
        else
            button4.gameObject.SetActive(false);

        if (typeTextRunning)
            button5.gameObject.SetActive(false);
        else if (currentCard.Option5On == true)
            button5.gameObject.SetActive(true);
        else
            button5.gameObject.SetActive(false);
    }

}

