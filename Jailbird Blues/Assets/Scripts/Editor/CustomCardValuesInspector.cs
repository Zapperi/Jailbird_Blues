using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


//This script creates a custom Unity Inspector for CardValues script (scriptable objects). 


[CustomEditor(typeof(CardValues))]                              //Create a custom editor, typecast it as CardValues script
public class CustomCardValuesInspector : Editor                 //We are using Editor, not MonoBehaviour
{
    private bool temp;
    private bool customInspector = false;
    private bool showInformation = true;
    private bool showRequirements = true;
    private int addedInt;
    CardValues cv;                                              //Short reference to CardValues
    private string saveOption4String;                           //Used to save option 4 textstring for later use

    private void OnEnable()                                     //OnEnable is called when the "card" is clicked
    {
        cv = (CardValues)target;                                //Use cd variable as unity's own target variable, typecast it as CardValues 
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
        cv.OptionsOn = GUILayout.Toggle(cv.OptionsOn, "Toggle all options");
        if (cv.OptionsOn)                                       //If options are toggled on...
        {
            //if (cd.option4text.Contains("Continue..."))             //Replaces the option 4 text with previously saved string
            //    cd.option4text = saveOption4String;
        }
        else                                                    //If Options are toggled off..
        {
            cv.Option1On = false;                               //set each option to inactive..
            cv.Option2On = false;
            cv.Option3On = false;
            cv.Option4On = true;                                //set option 4 to active
            if (!cv.option4text.Contains("Continue..."))            //Override option 4 text and save the overriden text for later use
            {
                saveOption4String = cv.option4text;
                cv.option4text = ("Continue...");
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
            cv.backgroundImage = (Sprite)EditorGUILayout.ObjectField("Background", cv.backgroundImage, typeof(Sprite), false);
            GUILayout.BeginHorizontal();
            cv.foregroundImage = (Sprite)EditorGUILayout.ObjectField("Foreground 1", cv.foregroundImage, typeof(Sprite), false);
            cv.foregroundImage2 = (Sprite)EditorGUILayout.ObjectField("Foreground 2", cv.foregroundImage2, typeof(Sprite), false);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            cv.foregroundImage3 = (Sprite)EditorGUILayout.ObjectField("Foreground 3", cv.foregroundImage3, typeof(Sprite), false);
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
        cv.Option1On = GUILayout.Toggle(cv.Option1On, "Toggle option 1");
        if (cv.Option1On)
        {
            serializedObject.Update();
            EditorGUIUtility.labelWidth = 100;
            EditorGUILayout.PropertyField(serializedObject.FindProperty("option1ReqSwitches"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("option1text"), true);
            EditorGUIUtility.labelWidth = 55;
            EditorGUILayout.LabelField("Reputation gains:", EditorStyles.miniBoldLabel);
            GUILayout.BeginHorizontal();
            GUILayout.Space(20);
            cv.option1IrsReputation = EditorGUILayout.IntField("I.R.S", cv.option1IrsReputation);
            cv.option1PunkReputation = EditorGUILayout.IntField("Punks", cv.option1PunkReputation);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Space(20);
            cv.option1ShakeReputation = EditorGUILayout.IntField("Shakers", cv.option1ShakeReputation);
            cv.option1GuardReputation = EditorGUILayout.IntField("Guards", cv.option1GuardReputation);
            GUILayout.EndHorizontal();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("option1ObtainedSwitches"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("option1FollowCard"), true);
            serializedObject.ApplyModifiedProperties();
        }
        cv.Option2On = GUILayout.Toggle(cv.Option2On, "Toggle option 2");
        if (cv.Option2On)
        {
            serializedObject.Update();
            EditorGUIUtility.labelWidth = 100;
            EditorGUILayout.PropertyField(serializedObject.FindProperty("option2ReqSwitches"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("option2text"), true);
            EditorGUIUtility.labelWidth = 55;
            EditorGUILayout.LabelField("Reputation gains:", EditorStyles.miniBoldLabel);
            GUILayout.BeginHorizontal();
            GUILayout.Space(20);
            cv.option2IrsReputation = EditorGUILayout.IntField("I.R.S", cv.option2IrsReputation);
            cv.option2PunkReputation = EditorGUILayout.IntField("Punks", cv.option2PunkReputation);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Space(20);
            cv.option2ShakeReputation = EditorGUILayout.IntField("Shakers", cv.option2ShakeReputation);
            cv.option2GuardReputation = EditorGUILayout.IntField("Guards", cv.option2GuardReputation);
            GUILayout.EndHorizontal();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("option2ObtainedSwitches"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("option2FollowCard"), true);
            serializedObject.ApplyModifiedProperties();
        }
        cv.Option3On = GUILayout.Toggle(cv.Option3On, "Toggle option 3");
        if (cv.Option3On)
        {
            serializedObject.Update();
            EditorGUIUtility.labelWidth = 100;
            EditorGUILayout.PropertyField(serializedObject.FindProperty("option3ReqSwitches"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("option3text"), true);
            EditorGUIUtility.labelWidth = 55;
            EditorGUILayout.LabelField("Reputation gains:", EditorStyles.miniBoldLabel);
            GUILayout.BeginHorizontal();
            GUILayout.Space(20);
            cv.option3IrsReputation = EditorGUILayout.IntField("I.R.S", cv.option3IrsReputation);
            cv.option3PunkReputation = EditorGUILayout.IntField("Punks", cv.option3PunkReputation);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Space(20);
            cv.option3ShakeReputation = EditorGUILayout.IntField("Shakers", cv.option3ShakeReputation);
            cv.option3GuardReputation = EditorGUILayout.IntField("Guards", cv.option3GuardReputation);
            GUILayout.EndHorizontal();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("option3ObtainedSwitches"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("option3FollowCard"), true);
            serializedObject.ApplyModifiedProperties();
        }
        
        cv.Option4On = GUILayout.Toggle(cv.Option4On, "Toggle option 4");
        if (cv.option4text.Contains("Continue..."))             //Replaces the option 4 text with previously saved string
            cv.option4text = saveOption4String;
        if (cv.Option4On)
        {
            serializedObject.Update();
            EditorGUIUtility.labelWidth = 100;
            EditorGUILayout.PropertyField(serializedObject.FindProperty("option4ReqSwitches"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("option4text"), true);
            EditorGUIUtility.labelWidth = 55;
            EditorGUILayout.LabelField("Reputation gains:", EditorStyles.miniBoldLabel);
            GUILayout.BeginHorizontal();
            GUILayout.Space(20);
            cv.option4IrsReputation = EditorGUILayout.IntField("I.R.S", cv.option4IrsReputation);
            cv.option4PunkReputation = EditorGUILayout.IntField("Punks", cv.option4PunkReputation);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Space(20);
            cv.option4ShakeReputation = EditorGUILayout.IntField("Shakers", cv.option4ShakeReputation);
            cv.option4GuardReputation = EditorGUILayout.IntField("Guards", cv.option4GuardReputation);
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
