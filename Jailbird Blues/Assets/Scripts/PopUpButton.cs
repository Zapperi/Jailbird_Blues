using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PopUpButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject cardDisplay;

    public void OnPointerEnter(PointerEventData eventData)
    {
        cardDisplay.GetComponent<CardDisplay>().popUpMouseOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        cardDisplay.GetComponent<CardDisplay>().popUpMouseOver = false;
    }
}
