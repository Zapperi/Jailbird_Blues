﻿using System.Collections;
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
        BuildDeck(cellCards);
        BuildDeck(yardCards);
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

	public void GetNextCard()														//activates the next card from a new deck after the previous card has been resolved
	{
		switch (schedule)															//chooses the deck based on schedule
		{
        case (0):
                BuildDeck(cellCards);                                               //builds a new card deck from scratch
                int index = Random.Range(0, cellCards.Count);						//picks a random number using the amount of cards in the deck as the range
                currentCard = cellCards[index];										//activates the card with the index matching the random number
                break;
         case (1):
                BuildDeck(yardCards);
                index = Random.Range(0, yardCards.Count);
			    currentCard = yardCards[index];
			    break;
		case (2):
                BuildDeck(messCards);
                index = Random.Range(0, messCards.Count);
			    currentCard = messCards[index];
			    break;
		case (3):
                BuildDeck(workshopCards);
                index = Random.Range(0, workshopCards.Count);
			    currentCard = workshopCards[index];
			    break;
		case (4):
                BuildDeck(cellCards);
                index = Random.Range(0, cellCards.Count);
			    currentCard = cellCards[index];
			    break;
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
                    for (int j = 0; j < allCards[i].Switches.Count; j++)
                    {
                        //if current card's switch requirment is not found, discard the current card.
                        if (!allSwitches[allCards[i].Switches[j]])
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
                    for (int j = 0; j < allCards[i].Switches.Count; j++)
                    {
                        //if current card's switch requirment is not found, discard the current card.
                        if (!allSwitches[allCards[i].Switches[j]])
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
                    for (int j = 0; j < allCards[i].Switches.Count; j++)
                    {
                        //if current card's switch requirment is not found, discard the current card.
                        if (!allSwitches[allCards[i].Switches[j]])
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
                    for (int j = 0; j < allCards[i].Switches.Count; j++)
                    {
                        //if current card's switch requirment is not found, discard the current card.
                        if (!allSwitches[allCards[i].Switches[j]])
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
                    for (int j = 0; j < allCards[i].Switches.Count; j++)
                    {
                        //if current card's switch requirment is not found, discard the current card.
                        if (!allSwitches[allCards[i].Switches[j]])
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
                    for (int j = 0; j < allCards[i].Switches.Count; j++)
                    {
                        //if current card's switch requirment is not found, discard the current card.
                        if (!allSwitches[allCards[i].Switches[j]])
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
                    for (int j = 0; j < allCards[i].Switches.Count; j++)
                    {
                        //if current card's switch requirment is not found, discard the current card.
                        if (!allSwitches[allCards[i].Switches[j]])
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
                    for (int j = 0; j < allCards[i].Switches.Count; j++)
                    {
                        //if current card's switch requirment is not found, discard the current card.
                        if (!allSwitches[allCards[i].Switches[j]])
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