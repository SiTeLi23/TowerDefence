using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private void Awake()
    {
        #region Singleton

        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;

        #endregion
   
        DontDestroyOnLoad(gameObject);
    }

    public AudioSource menuMusic, levelSelectMusic;
    public AudioSource[] bgm;
    public AudioSource[] sfx;

    private int currentBGM;
    private bool playingBGM;



    // Update is called once per frame
    void Update()
    {
        if (playingBGM) 
        {
            if (bgm[currentBGM].isPlaying == false)
            {
                currentBGM++;

                if(currentBGM >= bgm.Length) 
                {
                    currentBGM = 0;
                }

                bgm[currentBGM].Play();
            }
        }
    }

    public void PlayMenuMusic() 
    {
        StopMusic();
        menuMusic.Play();
    }

    public void PlayLevelSelectMusic() 
    {
        StopMusic();
        levelSelectMusic.Play();
    }

    public void StopMusic() 
    {
        menuMusic.Stop();
        levelSelectMusic.Stop();

        foreach(AudioSource track in bgm) 
        {
            track.Stop();
        }
        playingBGM = false;
    }

    public void PlayBGM() 
    {
        StopMusic();

        currentBGM = Random.Range(0, bgm.Length);

        bgm[currentBGM].Play();
        playingBGM = true;
    }

    public void PlaySFX(int sfxToPlay) 
    {
        sfx[sfxToPlay].Stop();//make sure the sfx won't play just one time if it being called twice
        sfx[sfxToPlay].Play();
    }
}
