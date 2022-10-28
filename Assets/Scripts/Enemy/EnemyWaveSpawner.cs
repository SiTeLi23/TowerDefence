using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveSpawner : MonoBehaviour
{
    public List<EnemyWave> wavesToSpawn;
    
    private float spawnCounter;
    public float waitForFirstSpawn;

    public Transform spawnPoint;
    public Castle theCastle;
    public Path thePath;
    public bool shouldSpawn = true;

    public float waveDisplayTime;
    private float waveDisplayCounter;
    private int waveCounter;

    void Start()
    {
        spawnCounter = waitForFirstSpawn;
        waveCounter = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldSpawn)
        {
            spawnCounter -= Time.deltaTime;
            if(spawnCounter <= 0) 
            {
                //as long as we have waves in the spawner
              if(wavesToSpawn.Count > 0) 
                {
                    //as long as the current wave still have enemie inside
                   if(wavesToSpawn[0].enemySpawnOrder.Count > 0) 
                    {
                        if(wavesToSpawn[0].shouldDisplayWave) 
                        {
                            wavesToSpawn[0].shouldDisplayWave = false;

                            UIController.instance.waveText.gameObject.SetActive(true);
                            UIController.instance.waveText.text = "Wave " + waveCounter;
                            waveDisplayCounter = waveDisplayTime;
                        }

                        //spawn the first enemy in the list
                        Instantiate(wavesToSpawn[0].enemySpawnOrder[0], spawnPoint.position, spawnPoint.rotation).SetUp(theCastle,thePath);
                        //wait for the next spawn
                        spawnCounter = wavesToSpawn[0].timeBetweenSpawns;
                        //remove the first enemy from the list 
                        wavesToSpawn[0].enemySpawnOrder.RemoveAt(0);

                        //when all the enemy has sapwned from the list
                        if(wavesToSpawn[0].enemySpawnOrder.Count == 0) 
                        {
                            //prepare for the next wave
                            spawnCounter = wavesToSpawn[0].timeToNextWave;
                            //remove current wave
                            wavesToSpawn.RemoveAt(0);
                            waveCounter++;

                            //if there's no more waves for this level, stop spawning
                            if(wavesToSpawn.Count == 0) 
                            {
                                shouldSpawn = false;
                            }
                        }
                    }
                }
            }
        }
        //make sure turn off the wave text if counter reach 0
        if(waveDisplayCounter > 0) 
        {
            waveDisplayCounter -= Time.deltaTime;
            if(waveDisplayCounter <= 0) 
            {
                UIController.instance.waveText.gameObject.SetActive(false);
            }
        }
    }
}

[System.Serializable]
public class EnemyWave 
{
    public List<EnemyController> enemySpawnOrder = new List<EnemyController>();
    public float timeBetweenSpawns;
    public float timeToNextWave;
    [HideInInspector]
    public bool shouldDisplayWave = true;
}
