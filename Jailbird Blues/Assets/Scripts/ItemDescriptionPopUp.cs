using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDescriptionPopUp : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //[HideInInspector]
    public Image ItemDescBackground;
    //[HideInInspector]
    public Text ItemDescText;

    void Start()
    {
        ItemDescBackground = gameObject.transform.Find("ItemImageBackground").GetComponent<Image>();
        ItemDescText = gameObject.transform.Find("ItemImageBackground").transform.Find("ItemImageText").GetComponent<Text>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ItemDescBackground.gameObject.SetActive(true);
        ItemDescText.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ItemDescBackground.gameObject.SetActive(false);
        ItemDescText.gameObject.SetActive(false);
    }

}
