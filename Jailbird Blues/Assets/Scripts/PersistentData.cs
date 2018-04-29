using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentData : MonoBehaviour
{

    public static PersistentData persistentValues;
    public float masterVolume;
    public float musicVolume;
    public float sfxVolume;
    public float gamma;
    public float textSpeed;
    public float scale;

    private void Awake()
    {
        if (persistentValues == null)
        {
            DontDestroyOnLoad(gameObject);
            persistentValues = this;
            masterVolume = 1f;
            musicVolume = 0.5f;
            sfxVolume = 0.5f;
            gamma = 0f;
            textSpeed = 0.004f;
            scale = 1f;

        }
        else if (persistentValues != this)
        {
            Destroy(gameObject);
        }
    }

}
