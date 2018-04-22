using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tools : MonoBehaviour {

    //Resets a transform's local transformation values
    public static void ResetLocation(Transform transform)
    {
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.localScale = Vector3.one;
    }

    public static void HideFloatingText()
    {
        if(GameObject.FindGameObjectWithTag("FloatingText"))
            GameObject.FindGameObjectWithTag("FloatingText").gameObject.SetActive(false);
    }
}
