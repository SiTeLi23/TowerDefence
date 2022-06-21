using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public float range = 3f;

    public LayerMask whatIsEnemy;
    private Collider[] colliderInRange;

    public List<EnemyController> enemiesInRange = new List<EnemyController>();

    private float checkCounter;
    public float checkTime = .2f;

    [HideInInspector]
    public bool enemiesUpdated;

    void Start()
    {
        checkCounter = checkTime;
    }

    // Update is called once per frame
    void Update()
    {
        #region Tower Checking Enemy In Range

        //each frame when the loop start, enemisUpdated should be set back to false
        enemiesUpdated = false;

        //we add a CD for tower to check enemy so it's not calling that check every frame which will make its computing less expensive
        checkCounter -= Time.deltaTime;
        if (checkCounter <= 0)
        {
            checkCounter = checkTime;

            //get all objects collider within the range
            colliderInRange = Physics.OverlapSphere(transform.position, range, whatIsEnemy);
            //make sure the list is empty at very start
            enemiesInRange.Clear();
            //add all enemies wihtin colliderInRange array into the enemy list
            foreach (Collider col in colliderInRange)
            {
                enemiesInRange.Add(col.GetComponent<EnemyController>());
            }

            enemiesUpdated = true;
        }

        #endregion
    }
}
