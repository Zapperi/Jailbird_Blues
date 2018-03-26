using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Card", menuName = "Card")]					//tells Unity that this is a custom asset
public class CardValues : ScriptableObject									//switched the surface from MonoBehavior into ScriptableObject
{
    //Card's name, can be used as a followup more easily.
    //public new string name;
    public Sprite BackgroundImage;											//reference to the image that will be set as the background
	public Sprite Npc;														//reference to the image that will be set as the non playable character
    public List<int> Switches;												//the list of cards and choices that have been played
    [Header("What is required to acivate the card")]						//inspector directions
    public int RepIrs;														//the required reputation among the I.R.S. to activate this card
	public int RepPunks;													//the required reputation among the Punks to activate this card
	public int RepShake;													//the required reputation among the Protein Shaker to activate this card
	public int RepGuard;													//the required reputation among the guards to activate this card
    [Header("Time: 1:Morning, 2:Lunch, 3:Work/yardtime, 4:Nighttime")]		//inspector directions
    public int timeOfDay;													//shcedule
    public int storyPhase;													//progress

	public string cardText;													//Card's text

    public bool endCard = false;											//is the card one of the final cards in the game
    [Header("If options are off, use 4th option to add your follow card")]	//inspector directions
    public bool OptionsOn = true;											//does the card have options?

    //Option 1 values
	public bool Option1On = true;											//is the option present?
    public string option1;													//what is the option?
    public int option1IrsReputation;										//how does the reputation change from this option
    public int option1PunkReputation;
    public int option1ShakeReputation;
    public int option1GuardReputation;
    public CardValues option1FollowCard;									//does this card have a follow-up card and if yes, which card?

    //Option 2 values
    public bool Option2On = true;
    public string option2;
    public int option2IrsReputation;
    public int option2PunkReputation;
    public int option2ShakeReputation;
    public int option2GuardReputation;
    public CardValues option2FollowCard;


    //Option 3 values
    public bool Option3On = true;
    public string option3;
    public int option3IrsReputation;
    public int option3PunkReputation;
    public int option3ShakeReputation;
    public int option3GuardReputation;
    public CardValues option3FollowCard;


    //Option 4 values
    public bool Option4On = true;
    public string option4;
    public int option4IrsReputation;
    public int option4PunkReputation;
    public int option4ShakeReputation;
    public int option4GuardReputation;
    public CardValues option4FollowCard;

    public bool repeatable;													//can this card happen multiple times during one playthrough
    public bool used;														//has the card been played already?
}