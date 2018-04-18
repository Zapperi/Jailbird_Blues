using UnityEngine;
using System.Collections;

public class UIDrag : MonoBehaviour
{
	//copypastascripti worldspace/screenspace - overlay canvasmoodeihin youtube tutorialista https://www.youtube.com/watch?v=hzuxb8CPGyQ
	//mahdollistaa pelin aikana dragattavan UIn
	//Liitä haluttuun UIhin tämä scripti ja Event Trigger componentti
	//lisää Event Triggeriin alla olevat functiot inspectorista

	Vector3 screenPoint;

	//Event Trigger -> Begin Drag(base event data)
	public void BeginDrag()
	{
	}
	//Event Trigger -> Drag(base event data)
	public void OnDrag()
	{
		screenPoint = Input.mousePosition;
		screenPoint.z = 1f; //distance of the plane from the camera
		transform.position = Camera.main.ScreenToWorldPoint(screenPoint);
	}
}﻿