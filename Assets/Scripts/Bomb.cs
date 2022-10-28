using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public Transform pivot, model;

    public float moveSpeed;


    [HideInInspector]
    public Vector3 targetPoint;

    public GameObject explodeEffect;

    public float damageAmount;
    public LayerMask whatIsEnemy;

    public float explodeRange;
    void Start()
    {
        Vector3 startPosition = transform.position;

 

        //get the center point between bomb and target position
        Vector3 centerPosition = (transform.position + targetPoint) * .5f;
        //move to center point
        transform.position = centerPosition;
        transform.right = targetPoint - transform.position;

        //move back model to the start position
        model.transform.position = startPosition;

        AudioManager.instance.PlaySFX(1);
    }


    void Update()
    {
        pivot.localRotation = Quaternion.RotateTowards(pivot.localRotation, Quaternion.Euler(0f, 0f, 180f), moveSpeed * Time.deltaTime);
        model.rotation = Quaternion.identity;

        //explosion when bomb landed on target place
        if (Vector3.Distance(model.position, targetPoint) < 0.1f) 
        {
            //get all objects collider within the range
            Collider[] colliderInRange = Physics.OverlapSphere(transform.position, explodeRange, whatIsEnemy);

            //cause damage
            foreach(Collider col in colliderInRange) 
            {
                col.GetComponent<EnemyHealthController>().TakeDamage(damageAmount);
            }

            //explodsion effect
            if (explodeEffect != null) 
            {
                Instantiate(explodeEffect, model.position, Quaternion.identity);
            }

            AudioManager.instance.PlaySFX(0);
            Destroy(gameObject);
        }
    }
}
