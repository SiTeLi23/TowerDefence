using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTower : MonoBehaviour
{
    private Tower theTower;

    private float bombCounter;

    public Bomb theBomb;

    public Transform spawnPoint;

    private Transform target;
    void Start()
    {
        theTower = GetComponent<Tower>();

        bombCounter = theTower.fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        bombCounter -= Time.deltaTime;

        if (theTower.enemiesInRange.Count > 0) 
        {
           if(bombCounter <= 0) 
            {
                float minDistance = theTower.range + 1f;
                foreach (EnemyController enemy in theTower.enemiesInRange)
                {
                    //incase enemy has been destroyed by tower
                    if (enemy != null)
                    {
                        //get each enemy's distance between the tower
                        float distance = Vector3.Distance(transform.position, enemy.transform.position);
                        if (distance < minDistance)
                        {
                            //update the closest enemy
                            minDistance = distance;
                            target = enemy.transform;

                        }

                    }
                }

                bombCounter = theTower.fireRate;

                
                Bomb newBomb = Instantiate(theBomb, spawnPoint.position, Quaternion.identity);
                newBomb.targetPoint = target.position;
            }
        }
    }
}
