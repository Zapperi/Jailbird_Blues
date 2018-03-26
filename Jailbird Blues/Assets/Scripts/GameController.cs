using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public static GameController gameController;									//a reference to the gamecontroller

	public int punksRep;															//the players reputation among the Punks
	public int irsRep;																//the players reputation among the I.R.S
	public int shakersRep;															//the players reputation among the Protein Shakers
	public int guardsRep;															//the players reputation among the prison guards
	public int day;																	//counter for days passed in game
	public int schedule;															//integer switch for the daily activities
	public List<CardValues> yardCards;												//list of cards in the yard time deck
	public List<CardValues> messCards;												//list of cards in the lunchtime deck
	public List<CardValues> workshopCards;											//list of cards in the workshop time deck
	public List<CardValues> cellCards;												//list of cards in the cell time deck
	public CardValues currentCard;													//the card that is currently active in the scene

	void Awake()																	//when the game starts
	{
		if (gameController == null)													//if there is no gamecontroller
		{
			DontDestroyOnLoad(gameObject);											//the gamecontroller won't reset when switching scenes
			gameController = this;													//this gamecontroller will be the gamecontroller
			punksRep = 0;															//player's reputation among factions starts at zero
			irsRep = 0;
			shakersRep = 0;
			guardsRep = 0;
			day = 1;																//the game starts at day 1
			schedule = 1;															//the day begins with the first activity in the schedule 
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
		case (1):
			int index = Random.Range(0, yardCards.Count);							//picks a random number using the amount of cards in the deck as the range
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
}