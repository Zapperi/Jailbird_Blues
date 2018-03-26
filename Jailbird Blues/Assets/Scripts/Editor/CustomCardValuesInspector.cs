//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEditor;


////This script creates a custom Unity Inspector for CardValues script (scriptable objects). 


//[CustomEditor(typeof(CardValues))]                              //Create a custom editor, typecast it as CardValues script
//public class CustomCardValuesInspector : Editor {               //We are using Editor, not MonoBehaviour
//    CardValues cd;                                              //Short reference to CardValues
//    private string saveOption4String;                           //Used to save option 4 textstring for later use

//    private void OnEnable()                                     //OnEnable is called when the "card" is clicked
//    {
//        cd = (CardValues)target;                                //Use cd variable as unity's own target variable, typecast it as CardValues 
//        saveOption4String = "";                                 //Clear saved string on card switch
//    }

//    public override void OnInspectorGUI()                       //calls override on default Unity inspector GUI
//    {
//        cd.OptionsOn = GUILayout.Toggle(cd.OptionsOn, "Toggle all options");
//        if (cd.OptionsOn)                                       //If options are toggled on...
//        {
            
//            cd.Option1On = GUILayout.Toggle(cd.Option1On, "Toggle option 1");                               //set each option to active..
//            cd.Option2On = GUILayout.Toggle(cd.Option2On, "Toggle option 2");
//            cd.Option3On = GUILayout.Toggle(cd.Option3On, "Toggle option 3");
//            if (cd.option4.Contains("Continue..."))             //Replaces the option 4 text with previously saved string
//                cd.option4 = saveOption4String;
//            cd.Option4On = GUILayout.Toggle(cd.Option4On, "Toggle option 4");
//            DrawDefaultInspector();
//            //base.OnInspectorGUI();                              //Shows the default Unity Inspector GUI            
//        }
//        else                                                    //If Options are toggled off..
//        {
            
//            cd.Option1On = false;                               //set each option to inactive..
//            cd.Option2On = false;
//            cd.Option3On = false;
//            cd.Option4On = true;                                //set option 4 to active
//            if (!cd.option4.Contains("Continue..."))            //Override option 4 text and save the overriden text for later use
//            { 
//                saveOption4String = cd.option4;
//                cd.option4 = ("Continue...");
//            }
//            DrawDefaultInspector();
//            //base.OnInspectorGUI();                             //Shows the default Unity Inspector GUI              
//        }

        
//    }

//}
