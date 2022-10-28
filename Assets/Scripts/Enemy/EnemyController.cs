using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [Header("Enemy Movement")]
    public float moveSpeed;
    [HideInInspector]
    public float speedMod = 1f;


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

    [Header("Enemy type")]
    public bool isFlying;
    public float flyHeight;
   

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

        if (isFlying) 
        {
            transform.position += Vector3.up * flyHeight;
            currentPoint = thePath.points.Length - 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!LevelManager.instance.levelActive) return;

        //if enemy not reaching the end, continue moving to next point
        if (!reachEnd)
        {
            if (!isFlying)
            {
                //move to current point
                transform.position = Vector3.MoveTowards(transform.position, thePath.points[currentPoint].position, moveSpeed * Time.deltaTime * speedMod);
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
            else 
            {
                //fly to current point
                transform.position = Vector3.MoveTowards(transform.position, thePath.points[currentPoint].position + (Vector3.up*flyHeight), moveSpeed * Time.deltaTime * speedMod);
                //rotate enemy to face current point
                transform.LookAt(thePath.points[currentPoint]);

                //if we are close enough to current point, fly to the next point
                if (Vector3.Distance(transform.position, thePath.points[currentPoint].position + (Vector3.up * flyHeight)) < .01f)
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
        }
            //if enemey reach the end , start attack the target
        else 
        {
            if (!isFlying)
            {
                transform.position = Vector3.MoveTowards(transform.position, theCastle.attackPoints[selectedAttackPoint].position, moveSpeed * Time.deltaTime * speedMod);
            }
            else 
            {
                transform.position = Vector3.MoveTowards(transform.position, theCastle.attackPoints[selectedAttackPoint].position + (Vector3.up*flyHeight), moveSpeed * Time.deltaTime * speedMod);
                transform.LookAt(theCastle.transform.position);
            }
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
