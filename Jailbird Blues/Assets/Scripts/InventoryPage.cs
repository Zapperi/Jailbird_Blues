using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Create a simple item class that contains the information
[System.Serializable]
public class Item
{
    public Sprite itemIcon;
    public string itemDescText;
    public int itemSwitchIndex;
}

public class InventoryPage : MonoBehaviour {

    public GameObject gameController;       // Reference to the GameController, set in code
    [HideInInspector]
    public List<Item> itemList;             // List of all the aivable items in the game, set in inspector
    public Transform parentObject;          // Which gameobject is the parent of the items to show
    public ItemObjectpool itemPool;         // Reference to the itempool, set in inspector
                                            // Itempool is a workaround for instantiating/destroying gameobjects (which can cause excess memory usage)
    

    void Start()
    {
        RefreshInventory();                                     // Call RefreshInventory function when invetory is opened
        gameController = GameObject.Find("GameController");     // Set the reference to the GameController
        itemList = gameController.GetComponent<GameController>().allItemList;
    }

    // Refresh inventory by removing all items and calling AddItems function..
    public void RefreshInventory()
    {
        removeItems();
        AddItems();
    }

    // Function that cycles throught every item in the game and adds the ones which player has..
    private void AddItems()
    {
        for (int i = 0; i < itemList.Count; i++)                                                    // Cycle the throught whole itemlist
        {
            Item item = itemList[i];                                                                // Set the current item..
            if (gameController.GetComponent<GameController>().allSwitches[item.itemSwitchIndex] && i != 2)    // Check if player has obtained item in question
            {
                GameObject newItem = itemPool.GetObject();                                          // Place the item into an object from itempool
                newItem.transform.SetParent(parentObject);                                          // Set the location of the item 
                Tools.ResetLocation(newItem.transform);
                newItem.GetComponent<ItemDescriptionPopUp>().Setup(item, this);                     // Call the setup function of the item, send current item and invetory as reference
            }          
        }
    }

    // Removes every item from the object by sending the item's gameobject back to itempool
    private void removeItems()
    {
        while(parentObject.childCount > 0)                              // While there are items in child..
        {
            GameObject toRemove = transform.GetChild(0).gameObject;     // assing the first child in hierarchy..
            itemPool.ReturnObject(toRemove);                            // ..and remove it by sending it back to the itempool by calling itempool's return function.
        }
    }
}
