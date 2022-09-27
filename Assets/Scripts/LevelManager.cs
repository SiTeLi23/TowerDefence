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

    private Castle theCastle;

    public List<EnemyHealthController> ActiveEnemies = new List<EnemyHealthController>();

    private SimpleEnemySpawner enemySpawner;

    // Start is called before the first frame update
    void Start()
    {
        theCastle = FindObjectOfType<Castle>();
        enemySpawner = FindObjectOfType<SimpleEnemySpawner>();
        levelActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (levelActive) 
        { 
           if(theCastle.currentHealth <= 0) 
            {
                levelActive = false;
                levelVictory = false;

                UIController.instance.levelFailedScreen.SetActive(true);
                UIController.instance.towerButtons.SetActive(false);
            }

           if(ActiveEnemies.Count == 0 && enemySpawner.amountToSpawn == 0) 
            {
                levelActive = false;
                levelVictory = true;

                UIController.instance.levelCompleteScreen.SetActive(true);
                UIController.instance.towerButtons.SetActive(false);
            }
        
        }
        
    }
}
