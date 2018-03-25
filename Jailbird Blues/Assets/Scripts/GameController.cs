using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public static GameController gameController;

	public int punksRep;
	public int irsRep;
	public int shakersRep;
	public int guardsRep;
	public int day;
	public int schedule;
	public List<CardValues> yardCards;
	public List<CardValues> messCards;
	public List<CardValues> workshopCards;
	public List<CardValues> cellCards;
    public GameObject card;

	void Awake()
	{
		if (gameController == null)
		{
			DontDestroyOnLoad(gameObject);
			gameController = this;
			punksRep = 0;
			irsRep = 0;
			shakersRep = 0;
			guardsRep = 0;
			day = 1;
			schedule = 1;
            card.GetComponent<CardDisplay>().card=yardCards[0];//testivaiheessa
            

		}
		else if (gameController != this)
		{
			Destroy(gameObject);
		}
	}

	public void UpdateReputations(int irs, int punks, int shakers, int guards)
	{
		irsRep += irs;
		punksRep += punks;
		shakersRep += shakers;
		guardsRep += guards;
	}

	public void GetNextCard()
	{
		switch (schedule)
		{
		case (1):
			int index = Random.Range(0, yardCards.Count);
                card.GetComponent<CardDisplay>().card = yardCards[index];
                break;
		case (2):
			index = Random.Range(0, messCards.Count);
                card.GetComponent<CardDisplay>().card = messCards[index];
                break;
		case (3):
			index = Random.Range(0, workshopCards.Count);
                card.GetComponent<CardDisplay>().card = workshopCards[index];
                break;
		case (4):
			index = Random.Range(0, cellCards.Count);
                card.GetComponent<CardDisplay>().card = cellCards[index];
                break;
		}
	}
}