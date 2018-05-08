using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public static GameController gameController;									//a reference to the gamecontroller
    public CardDisplay cardDisplay;                                                 // Reference to the cardDisplay, set in inspector.

    private bool addToDeck;                                                         //Used to track if card can be added
    public int punksRep;															//the players reputation among the Punks
	public int irsRep;																//the players reputation among the I.R.S
	public int shakersRep;															//the players reputation among the Protein Shakers
	public int guardsRep;															//the players reputation among the prison guards
	public int day;                                                                 //counter for days passed in game
    public int cigaretteCount;

    private int scheduleSize = 4;                                                   //integer for schedule size, incase we need to expand it
    public int schedule;                                                            //integer switch for the daily activities
	public Text logCurrentDayText;
    public Text topBar;

    public List<bool> allSwitches;
    public List<CardValues> allCards;
    public List<CardValues> yardCards;												//list of cards in the yard time deck
	public List<CardValues> messCards;                                              //list of cards in the lunchtime deck
    public List<CardValues> eveningCellCards;											//list of cards in the workshop time deck
	public List<CardValues> cellCards;												//list of cards in the cell time deck
	public CardValues currentCard;                                                  //the card that is currently active in the scene
    public CardValues previousCard;
    public bool endcardOn;
    public CardValues endOfGameCard;

    public GameObject sfxSource;                                                    //gameobject of sfx audio source
    public GameObject logPage;                                                      //game log
    public string logText;
    public List<string> logEvents;
    public List<Item> allItemList;                                                     // List of all the aivable items in the game, set in inspector
    public GameObject itemImage;
    private Animator itemAnimator;
    //private GameObject canvasParent;
    public GameObject gainedAnimationSpawnpoint;
    public GameObject lostAnimationSpawnpoint;


    public GameObject mainCamera;
    public CardValues nextCardWaiting;
    public bool waitingForPPS;
    public bool waitingForSFX;
    public bool cleaningUpFades;
    public CardValues cardWaiting;

    public CardValues dayChangeCard;                                                //This card is shown at the day change
    public GameObject dayChangeImage;                                               //Used to activate day change image
    public Text dayChangeText;                                                      //direct access to day change text element
    public bool dayChangeCardDisplayed;                                             //bool used to execute day change card



    void Awake()																	//when the game starts
	{
        //canvasParent = GameObject.Find("Canvas");
        
        GainsTextHandler.Initialize();                                              // Activates the floating reputation gain element
        if (gameController == null)													//if there is no gamecontroller
		{
            //// !! DISABLED FOR DEBUGGIN !!
            for (int i = 0; i < allSwitches.Count; i++)                             // At the start of the game, make sure all switches are set to false.
                allSwitches[i] = false;
            allSwitches[1] = true;                                                  // Enable for cigarrete box
            //DontDestroyOnLoad(gameObject);									    //the gamecontroller won't reset when switching scenes
            gameController = this;													//this gamecontroller will be the gamecontroller
			punksRep = 0;															//player's reputation among factions starts at zero
			irsRep = 0;
			shakersRep = 0;
			guardsRep = 0;
			day = 1;																//the game starts at day 1
            cigaretteCount = 0;
			schedule = 3;     //Sets intro time                                                      //the day begins with the first activity in the schedule 
            SetBackgroundAudio();
            AddLogEvent();
            topBar = cardDisplay.topBar;
            logCurrentDayText = cardDisplay.logPageDayText;
            waitingForPPS = false;
            waitingForSFX = false;
            cleaningUpFades = false;
            GetComponent<OptionsSliders>().LoadSettings();
            dayChangeCardDisplayed = false;

            //GetNextCard();

        }
		else if (gameController != this)											//in case of unwanted extra gamecontrollers
		{
			Destroy(gameObject);													//delete them
		}
	}

    private void Start()
    {
        gainedAnimationSpawnpoint = GameObject.Find("GainedAnimationSpawnpoint");
        lostAnimationSpawnpoint = GameObject.Find("LostAnimationSpawnpoint");
    }


    void Update()
    {
        if (endcardOn == true)                                                       //Get next card if end card option is enabled and button 4 is pressed
        {
            GetNextCard();
            endcardOn = false;
        }
		logCurrentDayText.text = topBar.text;	
    }

    public void UpdateReputations(int slot)
    {
        switch (slot)
        {
            case 1:
                irsRep += currentCard.option1IrsReputation;
                punksRep += currentCard.option1PunkReputation;
                shakersRep += currentCard.option1ShakeReputation;
                guardsRep += currentCard.option1GuardReputation;
                break;
            case 2:
                irsRep += currentCard.option2IrsReputation;
                punksRep += currentCard.option2PunkReputation;
                shakersRep += currentCard.option2ShakeReputation;
                guardsRep += currentCard.option2GuardReputation;
                break;
            case 3:
                irsRep += currentCard.option3IrsReputation;
                punksRep += currentCard.option3PunkReputation;
                shakersRep += currentCard.option3ShakeReputation;
                guardsRep += currentCard.option3GuardReputation;
                break;
            case 4:
                irsRep += currentCard.option4IrsReputation;
                punksRep += currentCard.option4PunkReputation;
                shakersRep += currentCard.option4ShakeReputation;
                guardsRep += currentCard.option4GuardReputation;
                break;
            case 5:
                irsRep += currentCard.option5IrsReputation;
                punksRep += currentCard.option5PunkReputation;
                shakersRep += currentCard.option5ShakeReputation;
                guardsRep += currentCard.option5GuardReputation;
                break;
        }
    }

    public void PrintReputations(int optionIndex)       // Create's a floating reputation gain text from given option.
    {
        switch (optionIndex)
        {
            case 1:
                GainsTextHandler.CreateGainsText(new int[] { currentCard.option1IrsReputation, currentCard.option1PunkReputation, currentCard.option1ShakeReputation, currentCard.option1GuardReputation }, cardDisplay.popUp.transform);
                break;
            case 2:
                GainsTextHandler.CreateGainsText(new int[] { currentCard.option2IrsReputation, currentCard.option2PunkReputation, currentCard.option2ShakeReputation, currentCard.option2GuardReputation }, cardDisplay.popUp.transform);
                break;
            case 3:
                GainsTextHandler.CreateGainsText(new int[] { currentCard.option3IrsReputation, currentCard.option3PunkReputation, currentCard.option3ShakeReputation, currentCard.option3GuardReputation }, cardDisplay.popUp.transform);
                break;
            case 4:
                GainsTextHandler.CreateGainsText(new int[] { currentCard.option4IrsReputation, currentCard.option4PunkReputation, currentCard.option4ShakeReputation, currentCard.option4GuardReputation }, cardDisplay.popUp.transform);
                break;
            case 5:
                GainsTextHandler.CreateGainsText(new int[] { currentCard.option5IrsReputation, currentCard.option5PunkReputation, currentCard.option5ShakeReputation, currentCard.option5GuardReputation }, cardDisplay.popUp.transform);
                break;
        }
    }
	public void UpdateReputations(int irs, int punks, int shakers, int guards)		//updates the reputations among factions. function used by CardDisplay
	{
        irsRep += irs;																//new reputation = old reputation + changes to reputation
		punksRep += punks;
		shakersRep += shakers;
		guardsRep += guards;
	}

	public void GetNextCard()														//activates the next card from a new deck after the previous card has been resolved
	{
        int nextScheludeTime;
        if (schedule < scheduleSize - 1)
        {
            nextScheludeTime = schedule + 1;
        }
        else
        {
            nextScheludeTime = 0;
        }
        CardValues next;
		switch (nextScheludeTime)															//chooses the deck based on schedule
		{
        case (0):
                BuildDeck(cellCards);                                               //builds a new card deck from scratch
                int index = Random.Range(0, cellCards.Count);                       //picks a random number using the amount of cards in the deck as the range
                if (cellCards.Count == 0)
                {
                    next = endOfGameCard;
                } else
                {
                    next = cellCards[index];
                }
                SetCurrentCard(next);
                cardDisplay.typeTextNewTextDone = false;
                break;
            case (1):
                BuildDeck(messCards);
                index = Random.Range(0, messCards.Count);
                if (messCards.Count == 0)
                {
                    next = endOfGameCard;
                }
                else
                {
                    next = messCards[index];
                }
                SetCurrentCard(next);
                cardDisplay.typeTextNewTextDone = false;
                break;
            case (2):
                BuildDeck(yardCards);
                index = Random.Range(0, yardCards.Count);
                if (yardCards.Count == 0)
                {
                    next = endOfGameCard;
                }
                else
                {
                    next = yardCards[index];
                }
                SetCurrentCard(next);
                cardDisplay.typeTextNewTextDone = false;
                break;
		
		case (3):
                BuildDeck(eveningCellCards);
                index = Random.Range(0, eveningCellCards.Count);
                if (eveningCellCards.Count == 0)
                {
                    next = endOfGameCard;
                } else
                {
                    next = eveningCellCards[index];
                }
                SetCurrentCard(next);
                cardDisplay.typeTextNewTextDone = false;
                break;
		//case (4):
  //              BuildDeck(cellCards);
  //              index = Random.Range(0, cellCards.Count);
		//	    currentCard = cellCards[index];
		//	    break;
		}
	}

    public void SetCurrentCard(int selectedOption)                            //This is where changing begins.
    {
        cardDisplay.BlockButtons();
        //Debug.Log("Block2 " + selectedOption);
        if (currentCard.ppsHasFadeOut || currentCard.sfxHasFadeOut)
        {
            //cardDisplay.BlockButtons();
            switch (selectedOption)
            {
                case 1:
                    cardWaiting = currentCard.option1FollowCard;                // Put the next card on hold.
                    break;
                case 2:
                    cardWaiting = currentCard.option2FollowCard;                // Put the next card on hold.
                    break;
                case 3:
                    cardWaiting = currentCard.option3FollowCard;                // Put the next card on hold.
                    break;
                case 4:
                    cardWaiting = currentCard.option4FollowCard;                // Put the next card on hold.
                    break;
                case 5:
                    cardWaiting = currentCard.option5FollowCard;                // Put the next card on hold.
                    break;
                case 6:
                    cardWaiting  = currentCard.SkipCard;
                    break;
            }
            if (currentCard.ppsHasFadeOut)
            {
                waitingForPPS = true;
                mainCamera.GetComponent<PPSManager>().DoFadeOut(currentCard);
            }
            if (currentCard.sfxHasFadeOut)
            {
                waitingForSFX = true;
                sfxSource.GetComponent<SfxPlayer>().SetFadingOutTrue();
            }
        }
        else
        {
            switch (selectedOption)
            {
                case 1:
                    ActuallyChangeCard(currentCard.option1FollowCard);          // Update the next card into given card.
                    break;
                case 2:
                    ActuallyChangeCard(currentCard.option2FollowCard);          // Update the next card into given card.
                    break;
                case 3:
                    ActuallyChangeCard(currentCard.option3FollowCard);          // Update the next card into given card.
                    break;
                case 4:
                    ActuallyChangeCard(currentCard.option4FollowCard);          // Update the next card into given card.
                    break;
                case 5:
                    ActuallyChangeCard(currentCard.option5FollowCard);          // Update the next card into given card.
                    break;
                case 6:
                    ActuallyChangeCard(currentCard.SkipCard);
                    break;
            }
        }
    }

    public void SetCurrentCard(CardValues next)
    {
        //Debug.Log("Block1");
        cardDisplay.BlockButtons();
        if (currentCard.ppsHasFadeOut || currentCard.sfxHasFadeOut)
        {
            cardWaiting = next;

            if (currentCard.ppsHasFadeOut)
            {
                waitingForPPS = true;
                mainCamera.GetComponent<PPSManager>().DoFadeOut(currentCard);
            }
            if (currentCard.sfxHasFadeOut)
            {
                waitingForSFX = true;
                sfxSource.GetComponent<SfxPlayer>().SetFadingOutTrue();
            }
        }
        else
        {
            ActuallyChangeCard(next);               // Update the next card into given card.
        }
    }

    public void ActuallyChangeCard(CardValues next)             //This is where the card changes.
    {
        if (next == null)
        {
            next = endOfGameCard;
        }
        if (currentCard.endOfGame)
        {
            ReturnToMenu();                                     //at the end of game, start transition to menu
        }
        if (cleaningUpFades)                                    //This is used to prevent changing cards after cleaning up fades (it uses same function as normal fade-out which would also end up here and change card)
        {
            cleaningUpFades = false;
            cardDisplay.UnblockButtons();
            return;
        }
        if (currentCard.timeCard)
        {
            AddTime();
            if (schedule == 0)
            {
                dayChangeCardDisplayed = false;
            }
        }
        previousCard = currentCard;                         //HERE WE...
        if (schedule == 0)                                  //This if-branch is used to display dayChangeCard when schedule is 0
        {
            if (!dayChangeCardDisplayed)
            {
                dayChangeCardDisplayed = true;              //bool to avoid loop
                dayChangeCard.option5FollowCard = next;     //set the next card to the dayChangeCard
                currentCard = dayChangeCard;                //set day change card as current card
                dayChangeImage.SetActive(true);             //set the day change image active
                dayChangeText.text = "Day " + day;          //update day change text
            } else
            {                                               //Enters here when dayChangeCard is still current card but it has already been displayed.
                dayChangeImage.SetActive(false);            //Set dayChangeImage inactive
                currentCard = next;                         //Move to next card
            }
        } else
        {
            currentCard = next;                                 //FINALLY CHANGE THE CARD
        }
        //currentCard = next;                                 //FINALLY CHANGE THE CARD
        cardWaiting = null;
        for (int i =0; i<allCards.Count; i++)
        {
            if (allCards[i]==currentCard && !currentCard.repeatable)
            {
                allCards.RemoveAt(i);
                break;
            }
        }
        SetBackgroundAudio();         // Update audio.
        AddLogEvent();
        cardDisplay.StartTextCoroutine(); //****************
        cardDisplay.UpdateCardDisplay();
        if (!currentCard.isTimeBasedCard)
        {
            if (currentCard.sfx != null)
            {
                sfxSource.GetComponent<SfxPlayer>().PlayTimedSfx(currentCard);
                cardWaiting = currentCard;
            }
            if (currentCard.ppsHasFadeIn || currentCard.blackScreenStart)                               //If the next card has a fade-in
            {
                waitingForPPS = true;
                cleaningUpFades = true;
                cardDisplay.BlockButtons();
                mainCamera.GetComponent<PPSManager>().SetFades(currentCard);
                
            }
            else
            {
                if (mainCamera.GetComponent<PPSManager>().FadesAreOn())     //This checks if fades are on and fades out if necessary.
                {
                    waitingForPPS = true;
                    cleaningUpFades = true;
                    cardDisplay.BlockButtons();
                    mainCamera.GetComponent<PPSManager>().DoFadeIn();
                } else
                {
                    cardDisplay.UnblockButtons();
                }
            }

        }

        if (currentCard.isTimeBasedCard)
        {
            if (currentCard.sfx != null)
            {
                waitingForSFX = true;
                cardDisplay.BlockButtons();
                sfxSource.GetComponent<SfxPlayer>().PlayTimedSfx(currentCard);
                cardWaiting = currentCard.option5FollowCard;
            }

            if (currentCard.ppsHasFadeIn || currentCard.blackScreenStart || currentCard.ppsHasFadeOut || currentCard.ppsShowCard != 0)
            {
                waitingForPPS = true;
                cardDisplay.BlockButtons();
                mainCamera.GetComponent<PPSManager>().SetFades(currentCard);
                cardWaiting = currentCard.option5FollowCard;
            }
        }
    }


    public void PPSFadesDone()
    {
        waitingForPPS = false;
        if (!waitingForSFX)
        {
            cardDisplay.UnblockButtons();
            if (cardWaiting != currentCard)
            {
                ActuallyChangeCard(cardWaiting);
            }
            else
            {
                if (cleaningUpFades)
                {
                    cleaningUpFades = false;
                }
            }
        }
    }

    public void SFXFadesDone()
    {
        if (waitingForSFX)
        {
            waitingForSFX = false;
            if (!waitingForPPS)
            {
                cardDisplay.UnblockButtons();
                if (cardWaiting != currentCard)
                {
                    ActuallyChangeCard(cardWaiting);
                }
                else
                {
                    if (cleaningUpFades)
                    {
                        cleaningUpFades = false;
                    }
                }
            }
        }
    }


        //Cycles through all the cards in the game and adds the possible cards to given parameter deck..
        public void BuildDeck(List<CardValues> targetDeck)
    {
        targetDeck.Clear();
        //TimeofDay 0 = Cell, 1 = Yard, 2 = Mess, 3 = Eveningcell
        for (int i = 0; i < allCards.Count; i++)
        {
            //reset addToDeck to true
            addToDeck = true;

            //--Create cellDeck--
            if (targetDeck == cellCards)
            {
                //Checks the time of day and positive reputation values, if true..
                if (allCards[i].timeOfDay == 0 && ((allCards[i].RepGuard == 0 && allCards[i].RepIrs == 0 && allCards[i].RepPunks == 0 && allCards[i].RepShake == 0) || (guardsRep > 0 && guardsRep > allCards[i].RepGuard) || (irsRep > 0 && irsRep > allCards[i].RepIrs) || (punksRep > 0 && punksRep > allCards[i].RepPunks) || (shakersRep > 0 && shakersRep > allCards[i].RepShake)))
                {
                    //Cycles through gameController's switchlist and compares it to the current card's switch requirments..
                    for (int j = 0; j < allCards[i].requiredSwitches.Count; j++)
                    {
                        //if current card's switch requirment is not found, discard the current card.
                        if (!allSwitches[allCards[i].requiredSwitches[j]])
                        {
                            addToDeck = false;
                            break;
                        }
                    }
                    //if all required switches were true, add card to the deck.
                    if (addToDeck)
                        targetDeck.Add(allCards[i]);
                }
                //Checks the time and negative reputation values, if true..
                else if (allCards[i].timeOfDay == 0 && ((guardsRep > 0 && guardsRep > allCards[i].RepGuard) || (irsRep > 0 && irsRep > allCards[i].RepIrs) || (punksRep > 0 && punksRep > allCards[i].RepPunks) || (shakersRep > 0 && shakersRep > allCards[i].RepShake)))
                {
                    for (int j = 0; j < allCards[i].requiredSwitches.Count; j++)
                    {
                        //if current card's switch requirment is not found, discard the current card.
                        if (!allSwitches[allCards[i].requiredSwitches[j]])
                        {
                            addToDeck = false;
                            break;
                        }
                    }
                    //if all required switches were true, add card to the deck.
                    if (addToDeck)
                        targetDeck.Add(allCards[i]);
                }



            }

            //--Create yardDeck--
            if (targetDeck == yardCards)
            {
                //Checks the time of day and positive reputation values, if true..
                if (allCards[i].timeOfDay == 1 && ((allCards[i].RepGuard == 0 && allCards[i].RepIrs == 0 && allCards[i].RepPunks == 0 && allCards[i].RepShake == 0) || (guardsRep > 0 && guardsRep > allCards[i].RepGuard) || (irsRep > 0 && irsRep > allCards[i].RepIrs) || (punksRep > 0 && punksRep > allCards[i].RepPunks) || (shakersRep > 0 && shakersRep > allCards[i].RepShake)))
                {
                    //Cycles through gameController's switchlist and compares it to the current card's switch requirments..
                    for (int j = 0; j < allCards[i].requiredSwitches.Count; j++)
                    {
                        //if current card's switch requirment is not found, discard the current card.
                        if (!allSwitches[allCards[i].requiredSwitches[j]])
                        {
                            addToDeck = false;
                            break;
                        }
                    }
                    //if all required switches were true, add card to the deck.
                    if (addToDeck)
                        targetDeck.Add(allCards[i]);
                }
                //Checks the time and negative reputation values, if true..
                else if (allCards[i].timeOfDay == 1 && ((guardsRep > 0 && guardsRep > allCards[i].RepGuard) || (irsRep > 0 && irsRep > allCards[i].RepIrs) || (punksRep > 0 && punksRep > allCards[i].RepPunks) || (shakersRep > 0 && shakersRep > allCards[i].RepShake)))
                {
                    for (int j = 0; j < allCards[i].requiredSwitches.Count; j++)
                    {
                        //if current card's switch requirment is not found, discard the current card.
                        if (!allSwitches[allCards[i].requiredSwitches[j]])
                        {
                            addToDeck = false;
                            break;
                        }
                    }
                    //if all required switches were true, add card to the deck.
                    if (addToDeck)
                        targetDeck.Add(allCards[i]);
                }



            }

            //--Create messDeck--
            else if (targetDeck == messCards)
            {
                //Checks the time of day and positive reputation values, if true..
                if (allCards[i].timeOfDay == 2 && ((allCards[i].RepGuard == 0 && allCards[i].RepIrs == 0 && allCards[i].RepPunks == 0 && allCards[i].RepShake == 0) || (guardsRep > 0 && guardsRep > allCards[i].RepGuard) || (irsRep > 0 && irsRep > allCards[i].RepIrs) || (punksRep > 0 && punksRep > allCards[i].RepPunks) || (shakersRep > 0 && shakersRep > allCards[i].RepShake)))
                {
                    //Cycles through gameController's switchlist and compares it to the current card's switch requirments..
                    for (int j = 0; j < allCards[i].requiredSwitches.Count; j++)
                    {
                        //if current card's switch requirment is not found, discard the current card.
                        if (!allSwitches[allCards[i].requiredSwitches[j]])
                        {
                            addToDeck = false;
                            break;
                        }
                    }
                    //if all required switches were true, add card to the deck.
                    if (addToDeck)
                        targetDeck.Add(allCards[i]);
                }
                //Checks the time and negative reputation values, if true..
                else if (allCards[i].timeOfDay == 2 && ((guardsRep > 0 && guardsRep > allCards[i].RepGuard) || (irsRep > 0 && irsRep > allCards[i].RepIrs) || (punksRep > 0 && punksRep > allCards[i].RepPunks) || (shakersRep > 0 && shakersRep > allCards[i].RepShake)))
                {
                    for (int j = 0; j < allCards[i].requiredSwitches.Count; j++)
                    {
                        //if current card's switch requirment is not found, discard the current card.
                        if (!allSwitches[allCards[i].requiredSwitches[j]])
                        {
                            addToDeck = false;
                            break;
                        }
                    }
                    //if all required switches were true, add card to the deck.
                    if (addToDeck)
                        targetDeck.Add(allCards[i]);
                }

            }

            //--Create EveningcellDeck--
            else if (targetDeck == eveningCellCards)
            {
                //Checks the time of day and positive reputation values, if true..
                if (allCards[i].timeOfDay == 3 && ((allCards[i].RepGuard == 0 && allCards[i].RepIrs == 0 && allCards[i].RepPunks == 0 && allCards[i].RepShake == 0) || (guardsRep > 0 && guardsRep > allCards[i].RepGuard) || (irsRep > 0 && irsRep > allCards[i].RepIrs) || (punksRep > 0 && punksRep > allCards[i].RepPunks) || (shakersRep > 0 && shakersRep > allCards[i].RepShake)))
                {
                    //Cycles through gameController's switchlist and compares it to the current card's switch requirments..
                    for (int j = 0; j < allCards[i].requiredSwitches.Count; j++)
                    {
                        //if current card's switch requirment is not found, discard the current card.
                        if (!allSwitches[allCards[i].requiredSwitches[j]])
                        {
                            addToDeck = false;
                            break;
                        }
                    }
                    //if all required switches were true, add card to the deck.
                    if (addToDeck)
                        targetDeck.Add(allCards[i]);
                }
                //Checks the time and negative reputation values, if true..
                else if (allCards[i].timeOfDay == 3 && ((guardsRep > 0 && guardsRep > allCards[i].RepGuard) || (irsRep > 0 && irsRep > allCards[i].RepIrs) || (punksRep > 0 && punksRep > allCards[i].RepPunks) || (shakersRep > 0 && shakersRep > allCards[i].RepShake)))
                {
                    for (int j = 0; j < allCards[i].requiredSwitches.Count; j++)
                    {
                        //if current card's switch requirment is not found, discard the current card.
                        if (!allSwitches[allCards[i].requiredSwitches[j]])
                        {
                            addToDeck = false;
                            break;
                        }
                    }
                    //if all required switches were true, add card to the deck.
                    if (addToDeck)
                        targetDeck.Add(allCards[i]);
                }
            }

        }
    }
    //checks if player has required switches for that option
    public bool Check1Switches()
    {
        if (currentCard.option1ReqSwitches.Count > 0)
        {
            for (int i = 0; i < currentCard.option1ReqSwitches.Count; i++)
            {
                if (allSwitches[currentCard.option1ReqSwitches[i]] == true)
                {
                    continue;
                }
                else
                {
                    return false;
                }
            }
        }
        return true;
    }
    //checks if player has required switches for that option
    public bool Check2Switches()
    {
        if (currentCard.option2ReqSwitches.Count > 0)
        {
            for (int i = 0; i < currentCard.option2ReqSwitches.Count; i++)
            {
                if (allSwitches[currentCard.option2ReqSwitches[i]] == true)
                {
                    continue;
                }
                else
                {
                    return false;
                }
            }
        }
        return true;
    }
    //checks if player has required switches for that option
    public bool Check3Switches()
    {
        if (currentCard.option3ReqSwitches.Count > 0)
        {
            for (int i = 0; i < currentCard.option3ReqSwitches.Count; i++)
            {
                if (allSwitches[currentCard.option3ReqSwitches[i]] == true)
                {
                    continue;
                }
                else
                {
                    return false;
                }
            }
        }
        return true;
    }
    //checks if player has required switches for that option
    public bool Check4Switches()
    {
        if (currentCard.option4ReqSwitches.Count > 0)
        {
            for (int i = 0; i < currentCard.option4ReqSwitches.Count; i++)
            {
                if (allSwitches[currentCard.option4ReqSwitches[i]] == true)
                {
                    continue;
                }
                else
                {
                    return false;
                }
            }
        }
        return true;
    }
    //Adds switches from that opinion to allswitched[]

    private void PlayItemObtained(int switchIndex)  // Check if given switch is an item, if so, play animation
    {
        for (int i = 0; i < allItemList.Count; i++)                                         // Cycle through item list
        {
            if (switchIndex == allItemList[i].itemSwitchIndex)                              // If the switch was an item..
            {
                GameObject obtainedItem = Instantiate(itemImage, gainedAnimationSpawnpoint.transform);   // Spawn an item object
                itemAnimator = obtainedItem.GetComponent<Animator>();                       // Access the animator component
                itemAnimator.SetBool("ItemGained", true);                                   // Play the animation
                obtainedItem.GetComponent<Image>().sprite = allItemList[i].itemIcon;        // Set the item icon              
                ItemReceivedAudioPlay();
                Destroy(obtainedItem, 4f);                                                  // Destroy object after 3 seconds
            }
        }
    }

    private void PlayItemLost(int switchIndex)          // Check if given switch is an item, if so, play animation
    {
        for (int i = 0; i < allItemList.Count; i++)     // Cycle through allitem list..            
        {
            if (switchIndex == allItemList[i].itemSwitchIndex)                              // If the switch was an item..
            {
                GameObject lostItem = Instantiate(itemImage, lostAnimationSpawnpoint.transform);   // Spawn the object under canvas
                itemAnimator = lostItem.GetComponent<Animator>();                       // Reference to animator
                itemAnimator.SetBool("ItemLost", true);                                     // Play animation
                lostItem.GetComponent<Image>().sprite = allItemList[i].itemIcon;        // Get the icon of the item
                Destroy(lostItem, 4f);                                                  // Destroy the object in 3 seconds.
            }
        }
    }

    public void AddSwitches(int buttonIndex)                            // Function that adds switches, button # as parameter
    {
        var dictionary = new Dictionary<string, List<int>>();           // Create a dictionary with string as key and list<int> as data
        dictionary["option1"] = currentCard.option1ObtainedSwitches;    // Add each button with their corresponding option
        dictionary["option2"] = currentCard.option2ObtainedSwitches;
        dictionary["option3"] = currentCard.option3ObtainedSwitches;
        dictionary["option4"] = currentCard.option4ObtainedSwitches;
        dictionary["option5"] = currentCard.option5ObtainedSwitches;
        string optionIndex = "option" + buttonIndex;                    // Create option variable with given button parameter

        if (dictionary[optionIndex].Count > 0)                          // If there are any obtained switches..
        {
            allSwitches[3] = false;                                     // Make sure obtain cig is set to false
            for (int i = 0; i < dictionary[optionIndex].Count; i++)     // Cycle through all the added switches
            {
                PlayItemObtained(dictionary[optionIndex][i]);           // IF the switch was an item, run obtained animation
                allSwitches[dictionary[optionIndex][i]] = true;         // Change obtained switch to true
                if (allSwitches[3] == true)                             // If obtained switch was obtain cig..
                {
                    AddCig();                                           // Add the cig to player inventory
                    PlayItemObtained(4);
                    allSwitches[3] = false;                             // Set the cig obtained boolean back to false
                }
            }
        }

        //switch (switches)
        //{
        //    case 1:
        //        if (currentCard.option1ObtainedSwitches.Count > 0)
        //        {
        //            allSwitches[3] = false;
        //            for (int i = 0; i < currentCard.option1ObtainedSwitches.Count; i++)
        //            {
        //                allSwitches[currentCard.option1ObtainedSwitches[i]] = true;
        //                if (allSwitches[3] == true)
        //                {
        //                    AddCig();
        //                    allSwitches[3] = false;
        //                }
        //            }
        //        }
        //        break;
        //    case 2:
        //        if (currentCard.option2ObtainedSwitches.Count > 0)
        //        {
        //            allSwitches[3] = false;
        //            for (int i = 0; i < currentCard.option2ObtainedSwitches.Count; i++)
        //            {
        //                allSwitches[currentCard.option2ObtainedSwitches[i]] = true;
        //                if (allSwitches[3] == true)
        //                {
        //                    AddCig();
        //                    allSwitches[3] = false;
        //                }
        //            }
        //        }
        //        break;
        //    case 3:
        //        if (currentCard.option3ObtainedSwitches.Count > 0)
        //        {
        //            allSwitches[3] = false;
        //            for (int i = 0; i < currentCard.option3ObtainedSwitches.Count; i++)
        //            {
        //                allSwitches[currentCard.option3ObtainedSwitches[i]] = true;
        //                if (allSwitches[3] == true)
        //                {
        //                    AddCig();
        //                    allSwitches[3] = false;
        //                }
        //            }
        //        }
        //        break;
        //    case 4:
        //        if (currentCard.option4ObtainedSwitches.Count > 0)
        //        {
        //            allSwitches[3] = false;
        //            for (int i = 0; i < currentCard.option4ObtainedSwitches.Count; i++)
        //            {
        //                allSwitches[currentCard.option4ObtainedSwitches[i]] = true;
        //                if (allSwitches[3] == true)
        //                {
        //                    AddCig();
        //                    allSwitches[3] = false;
        //                }
        //            }
        //        }
        //        break;
        //    case 5:
        //        if (currentCard.option5ObtainedSwitches.Count > 0)
        //        {
        //            allSwitches[3] = false;
        //            for (int i = 0; i < currentCard.option5ObtainedSwitches.Count; i++)
        //            {
        //                allSwitches[currentCard.option5ObtainedSwitches[i]] = true;
        //                if (allSwitches[3] == true)
        //                {
        //                    AddCig();
        //                    allSwitches[3] = false;
        //                }
        //            }
        //        }
        //        break;
        //}
    }

    //Removes switches from allswitched[]

    public void RemoveSwitches(int buttonIndex)                         // Function that removes switches, button # as parameter
    {
        var dictionary = new Dictionary<string, List<int>>();           // Create dictionary with string as key and List<int> as result
        dictionary["option1"] = currentCard.option1RemovedSwitches;     // Setup all the buttons with their corresponding options
        dictionary["option2"] = currentCard.option2RemovedSwitches;
        dictionary["option3"] = currentCard.option3RemovedSwitches;
        dictionary["option4"] = currentCard.option4RemovedSwitches;
        dictionary["option5"] = currentCard.option5RemovedSwitches;
        string optionIndex = "option" + buttonIndex;                    // Create a variable from the parameter

        if (dictionary[optionIndex].Count > 0)                          // If there are any removed switches..
        {
            allSwitches[3] = true;                                      // Make sure obtainCig is set to true
            for (int i = 0; i < dictionary[optionIndex].Count; i++)     // Cycle through all the switches
            {
                PlayItemLost(dictionary[optionIndex][i]);               // If switch is an item, play lost animation
                allSwitches[dictionary[optionIndex][i]] = false;        // Set the switch to false to remove it
                if (allSwitches[3] == false && cigaretteCount > 0)                            // If obtain cig was set to false..
                {
                    RemoveCig();                                        // Remove cig from player
                    PlayItemLost(4);
                    allSwitches[3] = true;                              // Set obtained cig back to true.
                }
            }
        }

        //switch (switches)
        //{
        //    case 1:
        //        if (currentCard.option1RemovedSwitches.Count > 0)
        //        {
        //            allSwitches[3] = true;
        //            for (int i = 0; i < currentCard.option1RemovedSwitches.Count; i++)
        //            {
        //                allSwitches[currentCard.option1RemovedSwitches[i]] = false;
        //                if (allSwitches[3] == false)
        //                {
        //                    RemoveCig();
        //                    allSwitches[3] = true;
        //                }
        //            }
        //        }
        //        break;
        //    case 2:
        //        if (currentCard.option2RemovedSwitches.Count > 0)
        //        {
        //            allSwitches[3] = true;
        //            for (int i = 0; i < currentCard.option2RemovedSwitches.Count; i++)
        //            {
        //                allSwitches[currentCard.option2RemovedSwitches[i]] = false;
        //                if (allSwitches[3] == false)
        //                {
        //                    RemoveCig();
        //                    allSwitches[3] = true;
        //                }
        //            }
        //        }
        //        break;
        //    case 3:
        //        if (currentCard.option3RemovedSwitches.Count > 0)
        //        {
        //            allSwitches[3] = true;
        //            for (int i = 0; i < currentCard.option3RemovedSwitches.Count; i++)
        //            {
        //                allSwitches[currentCard.option3RemovedSwitches[i]] = false;
        //                if (allSwitches[3] == false)
        //                {
        //                    RemoveCig();
        //                    allSwitches[3] = true;
        //                }
        //            }
        //        }
        //        break;
        //    case 4:
        //        if (currentCard.option4RemovedSwitches.Count > 0)
        //        {
        //            allSwitches[3] = true;
        //            for (int i = 0; i < currentCard.option4RemovedSwitches.Count; i++)
        //            {
        //                allSwitches[currentCard.option4RemovedSwitches[i]] = false;
        //                if (allSwitches[3] == false)
        //                {
        //                    RemoveCig();
        //                    allSwitches[3] = true;
        //                }
        //            }
        //        }
        //        break;
        //    case 5:
        //        if (currentCard.option5RemovedSwitches.Count > 0)
        //        {
        //            allSwitches[3] = true;
        //            for (int i = 0; i < currentCard.option5RemovedSwitches.Count; i++)
        //            {
        //                allSwitches[currentCard.option5RemovedSwitches[i]] = false;
        //                if (allSwitches[3] == false)
        //                {
        //                    RemoveCig();
        //                    allSwitches[3] = true;
        //                }
        //            }
        //        }
        //        break;

        //}
    }

    //Add an event to the log.
    public void AddLogEvent()
    {
        if (currentCard.logText != "")
        {
            logEvents.Add(currentCard.logText);                            // Add the given string to the logEvent list.
            string tempOutput = "DAY ";                         // Start with "DAY..
            tempOutput += day;                                  // Add current day from gamecontroller
            tempOutput += "\n";                                 // linebreak
            tempOutput += currentCard.logText;                             // Add given event
            tempOutput += "\n";                                 // linebreak
            tempOutput += logText;                               // Include the previous events 
            logText = tempOutput;                                // Update the old output with new
        }
    }

    //Advances the schedule, adds a new day if reaches the end.
    public void AddTime()
    {
        schedule++;
        if (schedule > scheduleSize - 1)
        {
            schedule = 0;
            day++;
        }
    }
    public void AddCig()
    {
        cigaretteCount += 1;
        if (cigaretteCount >= 1)
        {
            allSwitches[4] = true;
        }
    }

    public void RemoveCig()
    {
        if(cigaretteCount > 0)
            cigaretteCount -= 1;
        if (cigaretteCount <= 0)
        {
            allSwitches[4] = false;
        }
    }

    //Calls a button click-sfx

    public void ButtonClickPLay()
    {
        sfxSource.GetComponent<SfxPlayer>().ButtonAudioPlay();
    }

    //Calls a item received-sfx 
    public void ItemReceivedAudioPlay()
    {
        sfxSource.GetComponent<SfxPlayer>().ItemAudioPlay();
    }

    public void SetBackgroundAudio()
    {

        float mVol = 1f - currentCard.musicDecrease;
        float aVol = 1f - currentCard.ambientDecrease;
        //Both bgMusic and bgAmbient are defined.
        if (currentCard.bgMusic && currentCard.bgAmbientAudio)
        {
            sfxSource.GetComponent<SfxPlayer>().SetActiveAudios(currentCard.bgMusic, currentCard.bgAmbientAudio, mVol, aVol);
        }
        //if only bgMusic is defined
        else if (currentCard.bgMusic && !currentCard.bgAmbientAudio)
        {
            sfxSource.GetComponent<SfxPlayer>().SetActiveAudios(currentCard.bgMusic, currentCard.ambientOff, mVol, aVol);
        }
        //if only ambient is defined
        else if (!currentCard.bgMusic && currentCard.bgAmbientAudio)
        {
            sfxSource.GetComponent<SfxPlayer>().SetActiveAudios(currentCard.musicOff, currentCard.bgAmbientAudio, mVol, aVol);
        }
        //if neither are defined
        else
        {
            sfxSource.GetComponent<SfxPlayer>().SetActiveAudios(currentCard.musicOff, currentCard.ambientOff, mVol, aVol);
        }

    }

    public void RememberSettings()          //this function is called just before changing the scene to transfer the current values
    {
        PersistentData.persistentValues.masterVolume = sfxSource.GetComponent<SfxPlayer>().masterVolume;
        PersistentData.persistentValues.musicVolume = sfxSource.GetComponent<SfxPlayer>().musicModifier;
        PersistentData.persistentValues.sfxVolume = sfxSource.GetComponent<SfxPlayer>().sfxModifier;
        GetComponent<OptionsSliders>().RememberSettings();
    }

    public void ReturnToMenu()
    {
        RememberSettings();                 //"save" the current values
        SceneManager.LoadScene("Menu");     //change the scene
    }

}