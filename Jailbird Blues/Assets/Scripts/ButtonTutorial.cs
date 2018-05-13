using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonTutorial : MonoBehaviour {

    public GameObject others;
    public GameObject textField;
    public TextMeshProUGUI text;
    public Button button;

    private void Start()
    {
        gameObject.SetActive(true);
        textField.SetActive(true);
        others.SetActive(false);
        GameController.gameController.keysEnabled = false;
    }

    public void nextPage()
    {
        textField.SetActive(false);
        others.SetActive(true);
        text.GetComponent<TMP_Text>().text = "CLOSE";
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(closeTut);
    }

    public void closeTut()
    {
        GameController.gameController.keysEnabled = true;
        gameObject.SetActive(false);
    }


}
