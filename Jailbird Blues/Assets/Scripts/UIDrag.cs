using UnityEngine;
using System.Collections;

public class UIDrag : MonoBehaviour
{
    //copypastascripti worldspace/screenspace - overlay canvasmoodeihin youtube tutorialista https://www.youtube.com/watch?v=hzuxb8CPGyQ
    //mahdollistaa pelin aikana dragattavan UIn
    //Liitä haluttuun UIhin tämä scripti ja Event Trigger componentti
    //lisää Event Triggeriin alla olevat functiot inspectorista
    Vector3 screenPoint;
    Vector3 offset;

    //Event Trigger -> Begin Drag(base event data)
    public void BeginDrag()
    {
        screenPoint = Camera.main.WorldToScreenPoint(transform.position);                                                                       // Get the location where the object is located at.
        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z)); // Calculate the offset between the object's center at the mouseposition.
    }

	public void OnDrag()
	{
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);                                      // Get the location of the mouse and the z location of the object.
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset - new Vector3(0, 0, Camera.main.nearClipPlane);           // Add the offset between cursor and the element and fix the Z-value caused by screenspace(camera) with nearClipPlane.
        transform.position = curPosition;                                                                                                       // Update the new location of the notebook.
    }
}﻿