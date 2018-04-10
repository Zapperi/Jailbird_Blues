using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]					//tells Unity that this is a custom asset
public class CardValues : ScriptableObject									//switched the surface from MonoBehavior into ScriptableObject
{
    ////Card's name, can be used as a followup more easily.
    //public new string name;
    [Header("What is required to acivate the card")]                        //inspector directions
    [Tooltip("List of booleans required by the card. See reference file for values!")]
    public List<int> requiredSwitches;												//list of switches that are required by the card to activate
    [Tooltip("Required I.R.S reputation.")]
    public int RepIrs;                                                       //the required reputation among the I.R.S. to activate this card
    [Tooltip("Required Punks reputation.")]
    public int RepPunks;                                                  //the required reputation among the Punks to activate this card
    [Tooltip("Required Protein Shakers reputation.")]
    public int RepShake;                                                  //the required reputation among the Protein Shaker to activate this card
    [Tooltip("Required Guard reputation.")]
    public int RepGuard;                                                    //the required reputation among the guards to activate this card
    [Tooltip("Required time/schedule: 0 = Cell, 1 = Yard, 2 = Mess, 3 = Workshop")]     //inspector directions	
    public int timeOfDay;                                                   //shcedule
    [Tooltip("Required story phase.")]
    public int storyPhase;                                                  //progress
    [Header("Card information")]
    [Tooltip("What image shows on the background.")]
    public Sprite backgroundImage;                                          //reference to the image that will be set as the background
    [Tooltip("What image shows on the foreground, slot 1.")]
    public Sprite foregroundImage;                                                      //reference to the image that will be set as the non playable character
    [Tooltip("What image shows on the foreground, slot 2.")]
    public Sprite foregroundImage2;
    [Tooltip("What image shows on the foreground, slot 3.")]
    public Sprite foregroundImage3;
    [Tooltip("What image shows on the foreground, slot 4.")]
    public Sprite foregroundImage4;
    [Tooltip("Text that shows on the card.")]
    [TextArea]
    public string cardText;													//Card's text
    [Tooltip("The text for log book entry.")]
    public string logText;                                                  // log text for notebook events
    [Tooltip("Does this card trigger a game ending?")]
    public bool endCard = false;                                            //is the card one of the final cards in the game
    [Tooltip("Can this card appear multiple times?")]
    public bool repeatable;                                                 //can this card happen multiple times during one playthrough
    private bool used;														//has the card been played already?
    [Header("If options are off, use 4th option to add your follow card.")]  //inspector directions
    public bool OptionsOn = true;                                           //does the card have options?


    // --CARD VALUES-- 
    //Option 1 values
    [Space(3)]
    [Header("Option 1 settings:")]
    [Tooltip("Set if this option is enabled or not.")]
	public bool Option1On = true;                                           // Is the option available?
    [Tooltip("Which triggers are required for this option.")]
    public List<int> option1ReqSwitches;                                    // What switches are required for this option
    [Tooltip("The text of the option.")]
    public string option1text;                                              // what is the option?
    [Tooltip("How much I.R.S reputation changes.")]
    public int option1IrsReputation;                                        // How does the reputation change from this option
    [Tooltip("How much Punks reputation changes.")]
    public int option1PunkReputation;
    [Tooltip("How much Protein Shakers reputation changes.")]
    public int option1ShakeReputation;
    [Tooltip("How much Guard reputation changes.")]
    public int option1GuardReputation;
    [Tooltip("Which triggers are triggered by this option.")]
    public List<int> option1ObtainedSwitches;                               // Which switches are triggered by this option
    [Tooltip("Which triggers are removed by this option.")]
    public List<int> option1RemovedSwitches;
    [Tooltip("What card comes after this option?")]
    public CardValues option1FollowCard;                                    // Does this card have a follow-up card and if yes, which card?

    //Option 2 values
    [Space(3)]
    [Header("Option 2 settings:")]
    [Tooltip("Set if this option is enabled or not.")]
    public bool Option2On = true;                                           // Is the option available?
    [Tooltip("Which triggers are required for this option.")]
    public List<int> option2ReqSwitches;                                    // What switches are required for this option
    [Tooltip("The text of the option.")]
    public string option2text;                                              // what is the option?
    [Tooltip("How much I.R.S reputation changes.")]
    public int option2IrsReputation;                                        // How does the reputation change from this option
    [Tooltip("How much Punks reputation changes.")]
    public int option2PunkReputation;
    [Tooltip("How much Protein Shakers reputation changes.")]
    public int option2ShakeReputation;
    [Tooltip("How much Guard reputation changes.")]
    public int option2GuardReputation;
    [Tooltip("Which triggers are triggered by this option.")]
    public List<int> option2ObtainedSwitches;                               // Which switches are triggered by this option
    [Tooltip("Which triggers are removed by this option.")]
    public List<int> option2RemovedSwitches;
    [Tooltip("What card comes after this option?")]
    public CardValues option2FollowCard;                                    // Does this card have a follow-up card and if yes, which card?

    //Option 3 values
    [Space(3)]
    [Header("Option 3 settings:")]
    [Tooltip("Set if this option is enabled or not.")]
    public bool Option3On = true;                                           // Is the option available?
    [Tooltip("Which triggers are required for this option.")]
    public List<int> option3ReqSwitches;                                    // What switches are required for this option
    [Tooltip("The text of the option.")]
    public string option3text;                                              // what is the option?
    [Tooltip("How much I.R.S reputation changes.")]
    public int option3IrsReputation;                                        // How does the reputation change from this option
    [Tooltip("How much Punks reputation changes.")]
    public int option3PunkReputation;
    [Tooltip("How much Protein Shakers reputation changes.")]
    public int option3ShakeReputation;
    [Tooltip("How much Guard reputation changes.")]
    public int option3GuardReputation;
    [Tooltip("Which triggers are triggered by this option.")]
    public List<int> option3ObtainedSwitches;                               // Which switches are triggered by this option
    [Tooltip("Which triggers are removed by this option.")]
    public List<int> option3RemovedSwitches;
    [Tooltip("What card comes after this option?")]
    public CardValues option3FollowCard;                                    // Does this card have a follow-up card and if yes, which card?

    //Option 4 values
    [Space(3)]
    [Header("Option 4 settings:")]
    [Tooltip("Set if this option is enabled or not.")]
    public bool Option4On = true;                                           // Is the option available?
    [Tooltip("Which triggers are required for this option.")]
    public List<int> option4ReqSwitches;                                    // What switches are required for this option
    [Tooltip("The text of the option.")]
    public string option4text;                                              // what is the option?
    [Tooltip("How much I.R.S reputation changes.")]
    public int option4IrsReputation;                                        // How does the reputation change from this option
    [Tooltip("How much Punks reputation changes.")]
    public int option4PunkReputation;
    [Tooltip("How much Protein Shakers reputation changes.")]
    public int option4ShakeReputation;
    [Tooltip("How much Guard reputation changes.")]
    public int option4GuardReputation;
    [Tooltip("Which triggers are triggered by this option.")]
    public List<int> option4ObtainedSwitches;                               // Which switches are triggered by this option
    [Tooltip("Which triggers are removed by this option.")]
    public List<int> option4RemovedSwitches;
    [Tooltip("What card comes after this option?")]
    public CardValues option4FollowCard;                                    // Does this card have a follow-up card and if yes, which card?
}
