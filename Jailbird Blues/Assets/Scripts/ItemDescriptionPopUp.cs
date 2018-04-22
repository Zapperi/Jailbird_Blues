using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDescriptionPopUp : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //[HideInInspector]
    public Image ItemDescBackground;            // Reference to the popup text background, set in code
    //[HideInInspector]
    public Text ItemDescText;                   // Reference to the popup text, set in code
    public int itemIndex;                       // Reference to the item's switchlist index, set in inspector
    public Image itemIcon;                      // Reference to the item's icon, set in inspector

    private Item _item;                         // Current item

    // At the start, get the references from the objects.
    void OnEnable()
    {
        ItemDescBackground = gameObject.transform.Find("ItemImageBackground").GetComponent<Image>();
        ItemDescText = gameObject.transform.Find("ItemImageBackground").transform.Find("ItemImageText").GetComponent<Text>();
    }

    // When mouse is over the object..
    public void OnPointerEnter(PointerEventData eventData)
    {
        ItemDescBackground.gameObject.SetActive(true);      // Show background
        ItemDescText.gameObject.SetActive(true);            // Show text
    }

    // When mouse leaves the object..
    public void OnPointerExit(PointerEventData eventData)
    {
        ItemDescBackground.gameObject.SetActive(false);     // Hide background
        ItemDescText.gameObject.SetActive(false);           // Hide text
    }

    // Setup the item's information from the list (only gets the items that are TRUE on gamecontroller's switch list)
    public void Setup(Item currentItem, InventoryPage currentInventory)
    {
        if (currentItem.itemSwitchIndex == 1)
        {
            _item = currentItem;
            ItemDescText.text = "Cigarette count: " + GameController.gameController.cigaretteCount;
            itemIndex = _item.itemSwitchIndex;
            itemIcon.sprite = _item.itemIcon;
        }
        else
        {
            _item = currentItem;
            ItemDescText.text = _item.itemDescText;
            itemIndex = _item.itemSwitchIndex;
            itemIcon.sprite = _item.itemIcon;
        }
    }
}
