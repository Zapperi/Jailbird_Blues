using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitConfirmation : MonoBehaviour {
    public void QuitToMainMenu()
    {
        gameObject.transform.parent.GetComponentInChildren<CardDisplay>().UnblockButtons();
        GameController.gameController.ReturnToMenu();     //return to menu
    }

    public void ReturnToGame()
    {
        gameObject.transform.parent.GetComponentInChildren<CardDisplay>().UnblockButtons();
        gameObject.SetActive(false);
    }
}
