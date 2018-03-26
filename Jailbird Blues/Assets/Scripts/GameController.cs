using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public static GameController gameController;									//a reference to the gamecontroller

    private bool addToDeck;                                                         //Used to track if card can be added
    public int punksRep;															//the players reputation among the Punks
	public int irsRep;																//the players reputation among the I.R.S
	public int shakersRep;															//the players reputation among the Protein Shakers
	public int guardsRep;															//the players reputation among the prison guards
	public int day;                                                                 //counter for days passed in game

    private int scheduleSize = 4;                                                   //integer for schedule size, incase we need to expand it
    public int schedule;                                                            //integer switch for the daily activities

    public List<bool> allSwitches;
    public List<CardValues> allCards;
    public List<CardValues> yardCards;												//list of cards in the yard time deck
	public List<CardValues> messCards;												//list of cards in the lunchtime deck
	public List<CardValues> workshopCards;											//list of cards in the workshop time deck
	public List<CardValues> cellCards;												//list of cards in the cell time deck
	public CardValues currentCard;													//the card that is currently active in the scene

	void Awake()																	//when the game starts
	{
        //Debug testing starts
        allSwitches[0] = true;
        allSwitches[1] = false;
        allSwitches[2] = true;
        BuildDeck();
        //Debuggin ends


        if (gameController == null)													//if there is no gamecontroller
		{
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
	}

	public void UpdateReputations(int irs, int punks, int shakers, int guards)		//updates the reputations among factions. function used by CardDisplay
	{
		irsRep += irs;																//new reputation = old reputation + changes to reputation
		punksRep += punks;
		shakersRep += shakers;
		guardsRep += guards;
	}

	public void GetNextCard()														//activates the next card after the previous card has been played
	{
		switch (schedule)															//chooses the deck based on schedule
		{
        case (0):
            int index = Random.Range(0, cellCards.Count);
            currentCard = cellCards[index];
            break;
         case (1):
			index = Random.Range(0, yardCards.Count);							//picks a random number using the amount of cards in the deck as the range
			currentCard = yardCards[index];											//activates the card with the index matching the random number
			break;
		case (2):
			index = Random.Range(0, messCards.Count);
			currentCard = messCards[index];
			break;
		case (3):
			index = Random.Range(0, workshopCards.Count);
			currentCard = workshopCards[index];
			break;
		case (4):
			index = Random.Range(0, cellCards.Count);
			currentCard = cellCards[index];
			break;
		}
	}

    //Cycles through all the cards in the game and adds the possible cards to their decks
    public void BuildDeck()
    {
        //TimeofDay 0 = Cell, 1 = Yard, 2 = Mess, 3 = workshop
        for (int i = 0; i < allCards.Count; i++)
        {
            Debug.Log("Starting the cycle");
            //reset addToDeck to true
            addToDeck = true;
            //create cellDeck
            if(allCards[i].timeOfDay == 0 && ((allCards[i].RepGuard == 0 && allCards[i].RepIrs == 0 && allCards[i].RepPunks == 0 && allCards[i].RepShake == 0) || guardsRep > 0 && guardsRep > allCards[i].RepGuard) || (irsRep > 0 && irsRep > allCards[i].RepIrs) || (punksRep > 0 && punksRep > allCards[i].RepPunks || (shakersRep > 0 && shakersRep > allCards[i].RepShake)) )
            {
                //Cycles through gameController's switchlist and compares it to the current card's switch requirments..
                for (int j = 0; j < allCards[i].Switches.Count; j++)
                {
                    //if current card's switch requirment is not found, discard the current card.
                    if (!allSwitches[allCards[i].Switches[j]])
                    {
                        
                        Debug.Log(allCards[i].Switches[j] + " Switch is false");
                        addToDeck = false;
                        break;
                    }
                }
                //if all required switches were true, add card to the deck.
                if (addToDeck)
                    cellCards.Add(allCards[i]);
                

            }
            //else if (allCards[i].timeOfDay == 0 && (guardsRep < 0 && guardsRep < allCards[i].RepGuard) || (irsRep < 0 && irsRep < allCards[i].RepIrs) ||)
            //{
            //    cellCards.Add(allCards[i]);
            //}
            //create yardDeck
            if (allCards[i].timeOfDay == 1)
            {

            }
            //create messDeck
            if (allCards[i].timeOfDay == 2)
            {

            }
            //create workshopDeck
            if (allCards[i].timeOfDay == 3)
            {

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