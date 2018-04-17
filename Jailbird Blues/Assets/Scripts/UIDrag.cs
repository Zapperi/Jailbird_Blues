using UnityEngine;
using System.Collections;

public class UIDrag : MonoBehaviour
{
	//copypastascripti youtube tutorialista https://www.youtube.com/watch?v=hzuxb8CPGyQ
	//mahdollistaa pelin aikana dragattavan UIn
	//Liitä haluttuun UIhin tämä scripti ja Event Trigger componentti
	//lisää Event Triggeriin alla olevat functiot inspectorista

	private float offsetX;
	private float offsetY;

	//Event Trigger -> Begin Drag(base event data)
	public void BeginDrag()
	{
		offsetX = transform.position.x - Input.mousePosition.x;
		offsetY = transform.position.y - Input.mousePosition.y;
	}
	//Event Trigger -> Drag(base event data)
	public void OnDrag()
	{
		transform.position = new Vector3(offsetX + Input.mousePosition.x, offsetY + Input.mousePosition.y);
	}
}﻿