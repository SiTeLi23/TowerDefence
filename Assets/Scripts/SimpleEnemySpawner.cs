using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemySpawner : MonoBehaviour
{

    public EnemyController[] enemiesToSpawn;

    public Transform spawnPoint;

    public float timeBetweenSpawns;
    private float spawnCounter;

    public int amountToSpawn = 15;


    public Castle theCastle;
    public Path thePath;


    // Start is called before the first frame update
    void Start()
    {
        spawnCounter = timeBetweenSpawns;
    }

    // Update is called once per frame
    void Update()
    {
        if (amountToSpawn > 0 && LevelManager.instance.levelActive)
        {
            spawnCounter -= Time.deltaTime;
            if (spawnCounter <= 0)
            {
                spawnCounter = timeBetweenSpawns;
                //instantiate an enemy and assigned a target castle and path to it
                Instantiate(enemiesToSpawn[Random.Range(0,enemiesToSpawn.Length)], spawnPoint.position, Quaternion.identity).SetUp(theCastle,thePath);
                amountToSpawn--;
            }
        }

    }
}
