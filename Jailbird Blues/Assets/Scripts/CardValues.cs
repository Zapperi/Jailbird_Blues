using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu (fileName = "New Card", menuName = "Card")]
public class CardValues : ScriptableObject {
    //Card's name, can be used as a followup more easily.
    //public new string name;
    public Sprite BackgroundImage;
    public Sprite Npc;
    public List<int> Switches;
    public int RepIrs;
    public int RepPunks;
    public int RepShake;
    public int RepGuard;
    // Time: 0:Morning, 1:Afternoon, ...
    public int timeOfDay;
    public int storyPhase;


    //Card's text
    public string cardText;

    public bool OptionsOn = true;
    public bool endCard = false;
    //Option 1 values
    public bool Option1On = true;
    public string option1;
    public int option1IrsReputation;
    public int option1PunkReputation;
    public int option1ShakeReputation;
    public int option1GuardReputation;
    public CardValues option1FollowCard;

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




}
