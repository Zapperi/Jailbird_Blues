using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPage : MonoBehaviour {

    public GameObject gameController;
    public List<Image> itemImage;
    public List<Text> itemDescText;
    public List<int> itemSwitchInt;
    public GameObject itemPrefab;

    void Start()
    {
        gameController = GameObject.Find("GameController");    
    }

    void AddItem(int index)
    {
        //Instantiate(itemPrefab, )
    }

}
