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
    [Tooltip("Required time/schedule: 0 = Cell, 1 = Mess, 2 = Yard, 3 = Eveningcell")]     //inspector directions	
    public int timeOfDay;                                                   //shcedule
    [Tooltip("Required story phase.")]
    public int storyPhase;                                                  //progress
    [Header("Card information")]
    [Tooltip("Location of the scene.")]
    public string location;                                                 // Location of the scene in question.
    [Tooltip("What image shows on the background.")]
    public Sprite backgroundImage;                                          //reference to the image that will be set as the background
    [Tooltip("What image shows on the foreground right slot.")]
    public Sprite foregroundImage;                                          //reference to the image that will be set as the non playable character
    [Tooltip("Flip the facing of the image to Right Facing.")]
    public bool flipForegroundImageRight;
    [Tooltip("Is this person the speaker?")]
    public bool setRightAsSpeaker;
    [Tooltip("What image shows on the foreground left slot.")]
    public Sprite foregroundImage2;
    [Tooltip("Flip the facing of the image to Right Facing.")]
    public bool flipForegroundImageLeft;
    [Tooltip("Is this person the speaker?")]
    public bool setLeftAsSpeaker;
    [Tooltip("What image shows on the center slot.")]
    public Sprite foregroundImage3;
    [Tooltip("Flip the facing of the image to Right Facing.")]
    public bool flipForegroundImageCenter;
    [Tooltip("Is this person the speaker?")]
    public bool setCenterAsSpeaker;
    [Tooltip("What image shows on the foreground big center slot.")]
    public Sprite foregroundBigImage;
    [Tooltip("Flip the facing of the image to Right Facing.")]
    public bool flipForegroundImageBig;
    [Tooltip("Is this person the speaker?")]
    public bool setBigAsSpeaker;
    [Tooltip("What image shows on the foreground item slot.")]
    public Sprite foregroundItemImage;
    [Space(6)]
    [Tooltip("Background music for this scene. If this slot is empty, the previous music continues to play.")]
    public AudioClip bgMusic;
    [Tooltip("Background ambient soundscape.")]
    public AudioClip bgAmbientAudio;
    [Tooltip("How much music volume is decreased (0.0f - 1.0f), the default value is 0f (full volume).")]
    [Range(0.0f, 1.0f)]
    public float musicDecrease;
    [Tooltip("How much ambient audio volume is decreased (0.0f - 1.0f), the default value is 0f (full volume).")]
    [Range(0.0f, 1.0f)]
    public float ambientDecrease;
    [Tooltip("If you want to turn off the background music.")]
    public bool musicOff;
    [Tooltip("If you want to turn off the ambient audio.")]
    public bool ambientOff;
    [Tooltip("Text that shows who speaks.")]
    public string cardTextPerson;
    [Tooltip("Text that shows on the card.")]
    [TextArea]
    public string cardText;													//Card's text
    [Tooltip("The text for log book entry.")]
    public string logText;                                                  // log text for notebook events
    [Tooltip("Does this card trigger a game ending?")]
    public bool endCard = false;                                            //is the card one of the final cards in the game
	[Tooltip("Does this card advance schedule?")]
	public bool timeCard = false;											//if true, triggers gameController's AddTime function
	[Tooltip("Does this card have foreground image 1 on top of foreground image 2?")]
	public bool onTop = false;
    [Tooltip("Can this card appear multiple times?")]
    public bool repeatable;
    [Tooltip("Is this an end of game card?")]
    public bool endOfGame;
    [Tooltip("Skip to this card if pressed yes")]
    public CardValues SkipCard;
    private bool used;														//has the card been played already?
    [Header("If options are off, use Option 5 button to add your follow card.")]  //inspector directions
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


    //Option 5 values
    [Space(3)]
    [Header("Option 5 settings:")]

    [Tooltip("Set if this option is enabled or not.")]
    public bool Option5On = true;
    [Tooltip("How much I.R.S reputation changes.")]
    public int option5IrsReputation;                                        // How does the reputation change from this option
    [Tooltip("How much Punks reputation changes.")]
    public int option5PunkReputation;
    [Tooltip("How much Protein Shakers reputation changes.")]
    public int option5ShakeReputation;
    [Tooltip("How much Guard reputation changes.")]
    public int option5GuardReputation;
    [Tooltip("Which triggers are triggered by this option.")]
    public List<int> option5ObtainedSwitches;                               // Which switches are triggered by this option
    [Tooltip("Which triggers are removed by this option.")]
    public List<int> option5RemovedSwitches;
    [Tooltip("What card comes after this option?")]
    public CardValues option5FollowCard;                                    // Does this card have a follow-up card and if yes, which card?


    [Header("FX settings: ")]
    [Tooltip("Does fx timings define changing to the following card after this.")]
    public bool isTimeBasedCard;
    [Header("SFX settings:")]
    [Tooltip("Prewait before the sfx is played.")]
    public float sfxPrewait;
    [Tooltip("Played sound effect.")]
    public AudioClip sfx;
    [Tooltip("Is this sound effect faded out.")]
    public bool sfxHasFadeOut;
    [Tooltip("How long the sfx is played before it is faded out. The default is 1 second.")]
    public float sfxFadeOutAt;
    [Tooltip("How long does the fade-out at max take time.")]
    public float sfxFadeOutSpeed;
    [Tooltip("Time waited after the sfx is played.")]
    public float sfxAfterWait;
    [Header("PostProcessingEffect settings:")]
    [Tooltip("Is there a fade-in.")]
    public bool ppsHasFadeIn;
    [Tooltip("Fade mode: 0 = normal (cetered), 1 = left to right")]
    [Range(0, 1)]
    public int ppsFadeMode;
    [Tooltip("Is there a black screen before the fade-in. This is used to delay the fade-in.")]
    public bool blackScreenStart;
    [Tooltip("For how long the screen is black.")]
    public float blackScreenTime;
    [Tooltip("How long does the fade in take. The default value (if used) is 1 second.")]
    public float ppsFadeInSpeed;
    [Tooltip("How long is the card showed before fade-out or changing to the next card.")]
    public float ppsShowCard;
    [Tooltip("Is there a fade-out.")]
    public bool ppsHasFadeOut;
    [Tooltip("How long does the fade-out take.")]
    public float fadeOutSpeed;
}
