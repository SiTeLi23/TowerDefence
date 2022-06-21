using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PorjectilTower : MonoBehaviour
{
    Tower theTower;

    public GameObject projectile;
    public Transform firePoint;
    public float timeBetweenShot = 1f;
    float shotCounter;

    private Transform target;
    public Transform launcherModel;

    void Start()
    {
        theTower = GetComponent<Tower>();
        shotCounter = timeBetweenShot;
    }

    // Update is called once per frame
    void Update()
    {
        //rotate weapon toward target
        if (target != null) 
        {
            //smooth rotation
            launcherModel.rotation =  Quaternion.Slerp(launcherModel.rotation, Quaternion.LookRotation(target.position - transform.position),5f*Time.deltaTime);

            //limit x and y rotation to 0, Euler is transforming Quaternion into normal Vector 3,that's why we are not directly use LauncherModel.rotation.y
            //but  to use the eulerAngles.y to transform it into normal vector 3 version's y
            launcherModel.rotation = Quaternion.Euler(0f,launcherModel.rotation.eulerAngles.y , 0f);
        }


        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0 && target!=null) 
        {

            shotCounter = timeBetweenShot;

            firePoint.LookAt(target);

            Instantiate(projectile, firePoint.position, firePoint.rotation);
        }

        #region Find The Closet Enemy

        //optimized the checking function by adding a if check, so it won't call everyframe but will only called when enemiesUpdated == true;
        if (theTower.enemiesUpdated)
        {
            if (theTower.enemiesInRange.Count > 0)
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
                            //find the closest enemy
                            minDistance = distance;
                            target = enemy.transform;

                        }

                    }
                }

            }
        }

        else
        {
            //if we don't have any enemy at all within range
            target = null;
        }

        #endregion

    }
}
