using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerLoader : MonoBehaviour
{
    public AudioManager theAM;

    private void Awake()
    {
        if(FindObjectOfType<AudioManager>()== null) 
        {
            AudioManager.instance = Instantiate(theAM); //pre assigned a value to make sure it will be called before any other call
            DontDestroyOnLoad(AudioManager.instance.gameObject);
        }
    }

}
