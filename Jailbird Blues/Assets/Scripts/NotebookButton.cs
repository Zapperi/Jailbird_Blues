using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NotebookButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public Sprite mouseOverSprite;                                  // What sprite we want to show on mouseover
    public Image targetImage;                                       // What image we are editing, for sprite swap and color tint
    private Sprite defaultSprite;                                   // Keep in mind what the original sprite was

    public void OnPointerEnter(PointerEventData eventData)          // On mouse enter..
    {
        defaultSprite = targetImage.sprite;                         // Save original
        targetImage.sprite = mouseOverSprite;                       // Replace with new
        targetImage.color = new Color32(0xFF, 0x8A, 0x00, 0xFF);    // Tint the color to orange used in game
    }
    public void OnPointerExit(PointerEventData eventData)           // On mouse exit..
    {
        targetImage.sprite = defaultSprite;                         // Replace with default
        targetImage.color = new Color32(0xFF, 0xFF, 0xFF, 0xFF);    // Tint the color back to white
    }

}
