using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]					//tells Unity that this is a custom asset
public class CardValues : ScriptableObject									//switched the surface from MonoBehavior into ScriptableObject
{
    //Card's name, can be used as a followup more easily.
    //public new string name;
    [Header("What is required to acivate the card")]                        //inspector directions
    [Tooltip("List of booleans required by the card. See reference file for values!")]
    public List<int> Switches;												//list of switches that are required by the card to activate
    [Tooltip("Required I.R.S reputation")]
    public int RepIrs;                                                       //the required reputation among the I.R.S. to activate this card
    [Tooltip("Required Punks reputation")]
    public int RepPunks;                                                  //the required reputation among the Punks to activate this card
    [Tooltip("Required Protein Shakers reputation")]
    public int RepShake;                                                  //the required reputation among the Protein Shaker to activate this card
    [Tooltip("Required Guard reputation")]
    public int RepGuard;                                                    //the required reputation among the guards to activate this card
    [Tooltip("Required time/schedule: 0 = Cell, 1 = Yard, 2 = Mess, 3 = Workshop")]     //inspector directions	
    public int timeOfDay;                                                   //shcedule
    [Tooltip("Required story phase")]
    public int storyPhase;                                                  //progress

    [Header("Card information")]
    [Tooltip("What image shows on the background.")]
    public Sprite backgroundImage;                                          //reference to the image that will be set as the background
    [Tooltip("What image shows on the foreground.")]
    public Sprite foregroundImage;                                                      //reference to the image that will be set as the non playable character
    [Tooltip("Text that shows on the card")]
    [TextArea]
    public string cardText;													//Card's text
    [Tooltip("Does this card trigger a game ending?")]
    public bool endCard = false;                                            //is the card one of the final cards in the game
    [Tooltip("Can this card appear multiple times?")]
    public bool repeatable;                                                 //can this card happen multiple times during one playthrough
    private bool used;														//has the card been played already?
    [Header("If options are off, use 4th option to add your follow card")]  //inspector directions
    public bool OptionsOn = true;											//does the card have options?

    //Option 1 values
	public bool Option1On = true;											//is the option present?
    public string option1;													//what is the option?
    public int option1IrsReputation;										//how does the reputation change from this option
    public int option1PunkReputation;
    public int option1ShakeReputation;
    public int option1GuardReputation;
    public CardValues option1FollowCard;                                    //does this card have a follow-up card and if yes, which card?

    //Option 2 values
    [Space(10)]
    public bool Option2On = true;
    public string option2;
    public int option2IrsReputation;
    public int option2PunkReputation;
    public int option2ShakeReputation;
    public int option2GuardReputation;
    public CardValues option2FollowCard;


    //Option 3 values
    [Space(10)]
    public bool Option3On = true;
    public string option3;
    public int option3IrsReputation;
    public int option3PunkReputation;
    public int option3ShakeReputation;
    public int option3GuardReputation;
    public CardValues option3FollowCard;


    //Option 4 values
    [Space(10)]
    public bool Option4On = true;
    public string option4;
    public int option4IrsReputation;
    public int option4PunkReputation;
    public int option4ShakeReputation;
    public int option4GuardReputation;
    public CardValues option4FollowCard;
}
