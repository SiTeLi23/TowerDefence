using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;



    private void Awake()
    {
        #region Singleton

        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;

        #endregion
    }

    public bool levelActive;
    private bool levelVictory;

    private Castle[] theCastles;

    public List<EnemyHealthController> ActiveEnemies = new List<EnemyHealthController>();

    private EnemyWaveSpawner[] waveSpawners;

    public string nextLevel;

    // Start is called before the first frame update
    void Start()
    {
        theCastles = FindObjectsOfType<Castle>();
        waveSpawners = FindObjectsOfType<EnemyWaveSpawner>();
        levelActive = true;

        AudioManager.instance.PlayBGM();
    }

    // Update is called once per frame
    void Update()
    {
        if (levelActive) 
        {
            float totalCastleHealth = 0;
            foreach(Castle cast in theCastles) 
            {
                totalCastleHealth += cast.currentHealth;
            }

           if(totalCastleHealth <= 0) 
            {
                levelActive = false;
                levelVictory = false;

                UIController.instance.levelFailedScreen.SetActive(true);
                UIController.instance.towerButtons.SetActive(false);
            }

            bool waveComplete = true;
            foreach(EnemyWaveSpawner waveSpawn in waveSpawners) 
            {
              if(waveSpawn.wavesToSpawn.Count > 0) 
                {
                    //we still need to spawn
                    waveComplete = false;
                }
            }

           if(ActiveEnemies.Count == 0 && waveComplete) 
            {
                levelActive = false;
                levelVictory = true;

                UIController.instance.levelCompleteScreen.SetActive(true);
                UIController.instance.towerButtons.SetActive(false);
            }

            if (!levelActive) 
            {
                UIController.instance.levelFailedScreen.SetActive(!levelVictory);
                UIController.instance.levelCompleteScreen.SetActive(levelVictory);

                UIController.instance.CloseTowerUpGradePanel();
            }
        
        }
        
    }
}
