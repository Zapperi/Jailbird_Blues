using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentData : MonoBehaviour {

    public static PersistentData persistentValues;
    public float masterVolume;
    public float musicVolume;
    public float sfxVolume;

    private void Awake()
    {
        if (persistentValues == null)
        {
            DontDestroyOnLoad(gameObject);
            persistentValues = this;
            masterVolume = 1f;
            musicVolume = 0.5f;
            sfxVolume = 0.5f;
        } else if (persistentValues != this)
        {
            Destroy(gameObject);
        }
    }



}
