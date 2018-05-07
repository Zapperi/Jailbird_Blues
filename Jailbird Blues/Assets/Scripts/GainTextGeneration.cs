using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GainTextGeneration : MonoBehaviour {
    public Animator _animator;                                                  // Reference to the animator component, set in inspector (prefab)
    private Text _gainsText;                                                    // Reference to the text component, set in inspector (prefab)
    private bool fading;

    void OnEnable () {                                                          // When this object is called..
        AnimatorClipInfo[] clipInfo = _animator.GetCurrentAnimatorClipInfo(0);  // Get information about the animators on this object (only one on this object).
        Destroy(gameObject, clipInfo[0].clip.length);                           // Destroy te object after the lenght of the text float animation.
        _animator.GetComponent<CanvasRenderer>().SetAlpha(0f);
        _animator.GetComponent<Text>().CrossFadeAlpha(1f, 0.5f, false);
        _gainsText = _animator.GetComponent<Text>();                            // Set the text to correspond what shows in the animator.
	}

    public void Update()
    {
       if(TimeLeft() < 0.5f && fading == false)                                     // If time left on the animation is less than half a second AND there is no fade already running..
        {
            fading = true;                                                          // Trigger boolean, fade is going
            _animator.GetComponent<Text>().CrossFadeAlpha(0f, TimeLeft(), false);   // Start fade to 0, within remaining time of the animation.
        }
            
    }

    private float TimeLeft()                                                        // Return the time left on the animation
    {
        AnimatorStateInfo clipState = _animator.GetCurrentAnimatorStateInfo(0);     // Access info about animation state (speed and such)
        AnimatorClipInfo[] clipInfo = _animator.GetCurrentAnimatorClipInfo(0);      // Access info about clip (lenght and such)
        return clipInfo[0].clip.length - (clipInfo[0].clip.length * clipState.normalizedTime); // time remaining = total length - current time
    }

    public void SetText(string newText)                                            // Function that sets the text of the floating repuation gain text.
    {
       
        _gainsText.text = newText;                                                  // Set the text.
    }
}
