using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public static GameController gameController;									//a reference to the gamecontroller
    public CardValues cardValues;

    private bool addToDeck;                                                         //Used to track if card can be added
    public int punksRep;															//the players reputation among the Punks
	public int irsRep;																//the players reputation among the I.R.S
	public int shakersRep;															//the players reputation among the Protein Shakers
	public int guardsRep;															//the players reputation among the prison guards
	public int day;                                                                 //counter for days passed in game

    private int scheduleSize = 4;                                                   //integer for schedule size, incase we need to expand it
    public int schedule;                                                            //integer switch for the daily activities
	public Text timeOfDayText;
	private string scheduleName;
    public Text locationText;

    public List<bool> allSwitches;
    public List<CardValues> allCards;
    public List<CardValues> yardCards;												//list of cards in the yard time deck
	public List<CardValues> messCards;												//list of cards in the lunchtime deck
	public List<CardValues> workshopCards;											//list of cards in the workshop time deck
	public List<CardValues> cellCards;												//list of cards in the cell time deck
	public CardValues currentCard;                                                  //the card that is currently active in the scene
    public CardValues previousCard;
    public bool endcardOn;
	void Awake()																	//when the game starts
	{
        //// --FOR DEBUGGIN, REMOVE BEFORE BUILD!--
        ////Debug testing starts
        //allSwitches[0] = true;
        //allSwitches[1] = false;
        //allSwitches[2] = true;
        //BuildDeck(cellCards);
        //BuildDeck(yardCards);
        ////Debuggin ends


        if (gameController == null)													//if there is no gamecontroller
		{
            //// !! DISABLED FOR DEBUGGIN !!
            //for (int i = 0; i < allSwitches.Count; i++)                             // At the start of the game, make sure all switches are set to false.
            //    allSwitches[i] = false;               
			DontDestroyOnLoad(gameObject);											//the gamecontroller won't reset when switching scenes
			gameController = this;													//this gamecontroller will be the gamecontroller
			punksRep = 0;															//player's reputation among factions starts at zero
			irsRep = 0;
			shakersRep = 0;
			guardsRep = 0;
			day = 1;																//the game starts at day 1
			schedule = 0;															//the day begins with the first activity in the schedule 
			//GetNextCard();

		}
		else if (gameController != this)											//in case of unwanted extra gamecontrollers
		{
			Destroy(gameObject);													//delete them
		}
		scheduleName = "tutorial";


	}
    void Update()
    {

        if (endcardOn == true)                                                       //Get next card if end card option is enabled and button 4 is pressed
        {
            Debug.Log("arvo uusi kortti");
            GetNextCard();
            endcardOn = false;
        }
		timeOfDayText.text = "Day " + day + " : " + scheduleName +" time";
        locationText.text = scheduleName;

    }

	public void UpdateReputations(int irs, int punks, int shakers, int guards)		//updates the reputations among factions. function used by CardDisplay
	{
		irsRep += irs;																//new reputation = old reputation + changes to reputation
		punksRep += punks;
		shakersRep += shakers;
		guardsRep += guards;
        Debug.Log("Update works");
	}

	public void GetNextCard()														//activates the next card from a new deck after the previous card has been resolved
	{
		switch (schedule)															//chooses the deck based on schedule
		{
        case (0):
                BuildDeck(cellCards);                                               //builds a new card deck from scratch
                int index = Random.Range(0, cellCards.Count);						//picks a random number using the amount of cards in the deck as the range
                currentCard = cellCards[index];										//activates the card with the index matching the random number
				scheduleName = "cell";
                break;
         case (1):
                BuildDeck(yardCards);
                index = Random.Range(0, yardCards.Count);
			    currentCard = yardCards[index];
				scheduleName = "yard";
			    break;
		case (2):
                BuildDeck(messCards);
                index = Random.Range(0, messCards.Count);
			    currentCard = messCards[index];
				scheduleName = "lunch";
			    break;
		case (3):
                BuildDeck(workshopCards);
                index = Random.Range(0, workshopCards.Count);
			    currentCard = workshopCards[index];
				scheduleName = "workshop";
			    break;
		//case (4):
  //              BuildDeck(cellCards);
  //              index = Random.Range(0, cellCards.Count);
		//	    currentCard = cellCards[index];
		//	    break;
		}
	}
    

    //Cycles through all the cards in the game and adds the possible cards to given parameter deck..
    public void BuildDeck(List<CardValues> targetDeck)
    {
        targetDeck.Clear();
        //TimeofDay 0 = Cell, 1 = Yard, 2 = Mess, 3 = Workshop
        for (int i = 0; i < allCards.Count; i++)
        {
            //reset addToDeck to true
            addToDeck = true;
            
            //--Create cellDeck--
            if(targetDeck == cellCards)
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

            //--Create workshopDeck--
            else if (targetDeck == workshopCards)
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
    public void Add1Switches()
    {
        if (currentCard.option1ObtainedSwitches.Count > 0)
        {
            for (int i = 0; i < currentCard.option1ObtainedSwitches.Count; i++)
            {
                allSwitches[currentCard.option1ObtainedSwitches[i]] = true;
            }
        }
    }
    //Adds switches from that opinion to allswitched[]
    public void Add2Switches()
    {
        if (currentCard.option2ObtainedSwitches.Count > 0)
        {
            for (int i = 0; i < currentCard.option2ObtainedSwitches.Count; i++)
            {
                allSwitches[currentCard.option2ObtainedSwitches[i]] = true;
            }
        }
    }
    //Adds switches from that opinion to allswitched[]
    public void Add3Switches()
    {
        if (currentCard.option2ObtainedSwitches.Count > 0)
        {
            for (int i = 0; i < currentCard.option3ObtainedSwitches.Count; i++)
            {
                allSwitches[currentCard.option3ObtainedSwitches[i]] = true;
            }
        }
    }
    //Adds switches from that opinion to allswitched[]
    public void Add4Switches()
    {
        if (currentCard.option4ObtainedSwitches.Count > 0)
        {
            for (int i = 0; i < currentCard.option4ObtainedSwitches.Count; i++)
            {
                allSwitches[currentCard.option4ObtainedSwitches[i]] = true;
            }
        }
    }
    //Removes switches from allswitched[]
    public void Remove1Switches()
    {
        if (currentCard.option1RemovedSwitches.Count > 0)
        {
            for (int i = 0; i < currentCard.option1RemovedSwitches.Count; i++)
            {
                allSwitches[currentCard.option1RemovedSwitches[i]] = false;
            }
        }
    }
    //Removes switches from allswitched[]
    public void Remove2Switches()
    {
        if (currentCard.option2RemovedSwitches.Count > 0)
        {
            for (int i = 0; i < currentCard.option2RemovedSwitches.Count; i++)
            {
                allSwitches[currentCard.option2RemovedSwitches[i]] = false;
            }
        }
    }
    //Removes switches from allswitched[]
    public void Remove3Switches()
    {
        if (currentCard.option3RemovedSwitches.Count > 0)
        {
            for (int i = 0; i < currentCard.option3RemovedSwitches.Count; i++)
            {
                allSwitches[currentCard.option3RemovedSwitches[i]] = false;
            }
        }
    }
    //Removes switches from allswitched[]
    public void Remove4Switches()
    {
        if (currentCard.option4RemovedSwitches.Count > 0)
        {
            for (int i = 0; i < currentCard.option4RemovedSwitches.Count; i++)
            {
                allSwitches[currentCard.option4RemovedSwitches[i]] = false;
            }
        }
    }

    //Advances the schedule, adds a new day if reaches the end.
    public void AddTime()
    {
        schedule++;
        if (schedule > scheduleSize)
        {
            schedule = 0;
            day++;
        }
    }


    
}


