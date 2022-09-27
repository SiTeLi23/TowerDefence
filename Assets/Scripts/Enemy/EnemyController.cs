using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [Header("Enemy Movement")]
    public float moveSpeed;


    [Header("Path Tracking")]
    private Path thePath;
    private int currentPoint;
    private bool reachEnd;
    private Castle theCastle;
    private int selectedAttackPoint;

    [Header("Enemy Property")]
    public float timeBetweenAttacks;
    public float damagePerAttack;
    private float attackCounter;

   
   

    // Start is called before the first frame update
    void Start()
    {
        if (thePath == null)
        {
            thePath = FindObjectOfType<Path>();
        }
        if (theCastle == null)
        {
            theCastle = FindObjectOfType<Castle>();
        }

        attackCounter = timeBetweenAttacks;
    }

    // Update is called once per frame
    void Update()
    {
        if (!LevelManager.instance.levelActive) return;

        //if enemy not reaching the end, continue moving to next point
        if (!reachEnd)
        {

            //move to current point
            transform.position = Vector3.MoveTowards(transform.position, thePath.points[currentPoint].position, moveSpeed * Time.deltaTime);
            //rotate enemy to face current point
            transform.LookAt(thePath.points[currentPoint]);

            //if we are close enough to current point, move to the next point
            if (Vector3.Distance(transform.position, thePath.points[currentPoint].position) < .01f)
            {
                currentPoint += 1;

                //if the enemy reach the end
                if (currentPoint >= thePath.points.Length)
                {
                    reachEnd = true;
                    selectedAttackPoint = Random.Range(0, theCastle.attackPoints.Length);
                }


            }
        }
            //if enemey reach the end , start attack the target
        else 
        {
            transform.position = Vector3.MoveTowards(transform.position, theCastle.attackPoints[selectedAttackPoint].position, moveSpeed * Time.deltaTime);

            attackCounter -= Time.deltaTime;
            if (attackCounter <= 0) 
            {
                attackCounter = timeBetweenAttacks;

                theCastle.takeDamage(damagePerAttack);
            
            }
        
        }

    }


    //assing target castle and path to enemy
    public void SetUp(Castle newCastle,Path newPath) 
    {
        theCastle = newCastle;
        thePath = newPath;
    
    
    }




}
