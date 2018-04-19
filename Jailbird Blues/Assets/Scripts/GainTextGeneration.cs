using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GainTextGeneration : MonoBehaviour {
    public Animator _animator;                                                  // Reference to the animator component, set in inspector (prefab)
    private Text _gainsText;                                                    // Reference to the text component, set in inspector (prefab)


    void OnEnable () {                                                          // When this object is called..
        AnimatorClipInfo[] clipInfo = _animator.GetCurrentAnimatorClipInfo(0);  // Get information about the animators on this object (only one on this object).
        Destroy(gameObject, clipInfo[0].clip.length);                           // Destroy te object after the lenght of the text float animation.
        _gainsText = _animator.GetComponent<Text>();                            // Set the text to correspond what shows in the animator.
	}
	
    public void SetText(string newText)                                            // Function that sets the text of the floating repuation gain text.
    {
       _gainsText.text = newText;                                                  // Set the text.
    }
	
}
