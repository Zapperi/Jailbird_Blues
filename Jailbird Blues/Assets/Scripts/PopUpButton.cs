using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PopUpButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject cardDisplay;

    void Start()
    {
     //_mouseOver = GameObject.Find("Card").GetComponent<CardDisplay>().popUpMouseOver;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        cardDisplay.GetComponent<CardDisplay>().popUpMouseOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        cardDisplay.GetComponent<CardDisplay>().popUpMouseOver = false;
    }

    //void Update()
    //{
    //    OnMouseOver();
    //    OnMouseExit();
    //}

     
    //void OnMouseOver()
    //{
    //    cardDisplay.GetComponent<CardDisplay>().popUpMouseOver = true;
    //    _mouseOver = true;
    //}
    //void OnMouseExit()
    //{
    //    cardDisplay.GetComponent<CardDisplay>().popUpMouseOver = false;
    //    _mouseOver = false;
    //}
}
