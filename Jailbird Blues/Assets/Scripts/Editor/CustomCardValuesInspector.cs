using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


//This script creates a custom Unity Inspector for CardValues script (scriptable objects). 


[CustomEditor(typeof(CardValues))]                              //Create a custom editor, typecast it as CardValues script
public class CustomCardValuesInspector : Editor                 //We are using Editor, not MonoBehaviour
{
    private bool temp;
    private bool customInspector = true;
    private bool showInformation = false;
    private bool showRequirements = false;
    private int addedInt;
    CardValues cd;                                              //Short reference to CardValues
    private string saveOption4String;                           //Used to save option 4 textstring for later use

    private void OnEnable()                                     //OnEnable is called when the "card" is clicked
    {
        cd = (CardValues)target;                                //Use cd variable as unity's own target variable, typecast it as CardValues 
        saveOption4String = "";                                 //Clear saved string on card switch
    }

    public override void OnInspectorGUI()                       //calls override on default Unity inspector GUI
    {
        UseCustomInspector();
        if (customInspector)
        {
            CardRequirements();
            CardInformation();
            CardOptions();
        }
        else
            base.DrawDefaultInspector();
    }

    private void OptionsToggle()
    {
        cd.OptionsOn = GUILayout.Toggle(cd.OptionsOn, "Toggle all options");
        if (cd.OptionsOn)                                       //If options are toggled on...
        {
            if (cd.option4text.Contains("Continue..."))             //Replaces the option 4 text with previously saved string
                cd.option4text = saveOption4String;
        }
        else                                                    //If Options are toggled off..
        {
            cd.Option1On = false;                               //set each option to inactive..
            cd.Option2On = false;
            cd.Option3On = false;
            cd.Option4On = true;                                //set option 4 to active
            if (!cd.option4text.Contains("Continue..."))            //Override option 4 text and save the overriden text for later use
            {
                saveOption4String = cd.option4text;
                cd.option4text = ("Continue...");
            }
        }
    }

    private void CardRequirements()
    {
        showRequirements = EditorGUILayout.Foldout(showRequirements, "Card Requirements");
        if (showRequirements)
        {
            serializedObject.Update();
            EditorGUIUtility.labelWidth = 100;
            EditorGUILayout.PropertyField(serializedObject.FindProperty("requiredSwitches"), true);

            EditorGUILayout.BeginHorizontal();
            EditorGUIUtility.labelWidth = 80;
            EditorGUIUtility.fieldWidth = 15;
            EditorGUILayout.PropertyField(serializedObject.FindProperty("RepIrs"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("RepPunks"), true);
            GUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("RepShake"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("RepGuard"), true);
            GUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("timeOfDay"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("storyPhase"), true);
            GUILayout.EndHorizontal();
            serializedObject.ApplyModifiedProperties();
        }
       
    }

    private void CardInformation()
    {
        EditorGUILayout.LabelField("Card information settings", EditorStyles.boldLabel);
        showInformation = EditorGUILayout.Foldout(showInformation, "Card Information");
        if (showInformation)
        {
            EditorGUIUtility.labelWidth = 80;
            EditorGUIUtility.fieldWidth = 15;
            cd.backgroundImage = (Sprite)EditorGUILayout.ObjectField("Background", cd.backgroundImage, typeof(Sprite), false);
            GUILayout.BeginHorizontal();
            cd.foregroundImage = (Sprite)EditorGUILayout.ObjectField("Foreground 1", cd.foregroundImage, typeof(Sprite), false);
            cd.foregroundImage2 = (Sprite)EditorGUILayout.ObjectField("Foreground 2", cd.foregroundImage2, typeof(Sprite), false);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            cd.foregroundImage3 = (Sprite)EditorGUILayout.ObjectField("Foreground 3", cd.foregroundImage3, typeof(Sprite), false);
            cd.foregroundImage4 = (Sprite)EditorGUILayout.ObjectField("Foreground 4", cd.foregroundImage4, typeof(Sprite), false);
            GUILayout.EndHorizontal();

            serializedObject.Update();
            EditorGUIUtility.labelWidth = 150;
            EditorGUILayout.PropertyField(serializedObject.FindProperty("cardText"), true);
            GUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("endCard"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("repeatable"), true);
            GUILayout.EndHorizontal();
            serializedObject.ApplyModifiedProperties();
        }                  
    }

    private void CardOptions()
    {
        EditorGUILayout.LabelField("Card option settings", EditorStyles.boldLabel);
        OptionsToggle();
        cd.Option1On = GUILayout.Toggle(cd.Option1On, "Toggle option 1");
        if (cd.Option1On)
        {
            serializedObject.Update();
            EditorGUIUtility.labelWidth = 100;
            EditorGUILayout.PropertyField(serializedObject.FindProperty("option1ReqSwitches"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("option1text"), true);
            EditorGUIUtility.labelWidth = 55;
            EditorGUILayout.LabelField("Reputation gains:", EditorStyles.miniBoldLabel);
            GUILayout.BeginHorizontal();
            GUILayout.Space(20);
            cd.option1IrsReputation = EditorGUILayout.IntField("I.R.S", cd.option1IrsReputation);
            cd.option1PunkReputation = EditorGUILayout.IntField("Punks", cd.option1PunkReputation);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Space(20);
            cd.option1ShakeReputation = EditorGUILayout.IntField("Shakers", cd.option1ShakeReputation);
            cd.option1GuardReputation = EditorGUILayout.IntField("Guards", cd.option1GuardReputation);
            GUILayout.EndHorizontal();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("option1ObtainedSwitches"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("option1FollowCard"), true);
            serializedObject.ApplyModifiedProperties();
        }
        cd.Option2On = GUILayout.Toggle(cd.Option2On, "Toggle option 2");
        if (cd.Option2On)
        {
            serializedObject.Update();
            EditorGUIUtility.labelWidth = 100;
            EditorGUILayout.PropertyField(serializedObject.FindProperty("option2ReqSwitches"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("option2text"), true);
            EditorGUIUtility.labelWidth = 55;
            EditorGUILayout.LabelField("Reputation gains:", EditorStyles.miniBoldLabel);
            GUILayout.BeginHorizontal();
            GUILayout.Space(20);
            cd.option2IrsReputation = EditorGUILayout.IntField("I.R.S", cd.option2IrsReputation);
            cd.option2PunkReputation = EditorGUILayout.IntField("Punks", cd.option2PunkReputation);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Space(20);
            cd.option2ShakeReputation = EditorGUILayout.IntField("Shakers", cd.option2ShakeReputation);
            cd.option2GuardReputation = EditorGUILayout.IntField("Guards", cd.option2GuardReputation);
            GUILayout.EndHorizontal();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("option2ObtainedSwitches"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("option2FollowCard"), true);
            serializedObject.ApplyModifiedProperties();
        }
        cd.Option3On = GUILayout.Toggle(cd.Option3On, "Toggle option 3");
        if (cd.Option3On)
        {
            serializedObject.Update();
            EditorGUIUtility.labelWidth = 100;
            EditorGUILayout.PropertyField(serializedObject.FindProperty("option3ReqSwitches"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("option3text"), true);
            EditorGUIUtility.labelWidth = 55;
            EditorGUILayout.LabelField("Reputation gains:", EditorStyles.miniBoldLabel);
            GUILayout.BeginHorizontal();
            GUILayout.Space(20);
            cd.option3IrsReputation = EditorGUILayout.IntField("I.R.S", cd.option3IrsReputation);
            cd.option3PunkReputation = EditorGUILayout.IntField("Punks", cd.option3PunkReputation);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Space(20);
            cd.option3ShakeReputation = EditorGUILayout.IntField("Shakers", cd.option3ShakeReputation);
            cd.option3GuardReputation = EditorGUILayout.IntField("Guards", cd.option3GuardReputation);
            GUILayout.EndHorizontal();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("option3ObtainedSwitches"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("option3FollowCard"), true);
            serializedObject.ApplyModifiedProperties();
        }
        
        cd.Option4On = GUILayout.Toggle(cd.Option4On, "Toggle option 4");
        if (cd.option4text.Contains("Continue..."))             //Replaces the option 4 text with previously saved string
            cd.option4text = saveOption4String;
        if (cd.Option4On)
        {
            serializedObject.Update();
            EditorGUIUtility.labelWidth = 100;
            EditorGUILayout.PropertyField(serializedObject.FindProperty("option4ReqSwitches"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("option4text"), true);
            EditorGUIUtility.labelWidth = 55;
            EditorGUILayout.LabelField("Reputation gains:", EditorStyles.miniBoldLabel);
            GUILayout.BeginHorizontal();
            GUILayout.Space(20);
            cd.option4IrsReputation = EditorGUILayout.IntField("I.R.S", cd.option4IrsReputation);
            cd.option4PunkReputation = EditorGUILayout.IntField("Punks", cd.option4PunkReputation);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Space(20);
            cd.option4ShakeReputation = EditorGUILayout.IntField("Shakers", cd.option4ShakeReputation);
            cd.option4GuardReputation = EditorGUILayout.IntField("Guards", cd.option4GuardReputation);
            GUILayout.EndHorizontal();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("option4ObtainedSwitches"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("option4FollowCard"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }

    private void UseCustomInspector()
    {
        if (GUILayout.Button("Use custom inspector") && customInspector == false)
            customInspector = true;
        else if (GUILayout.Button("Use default inspector") && customInspector == true)
            customInspector = false;
    }
}
