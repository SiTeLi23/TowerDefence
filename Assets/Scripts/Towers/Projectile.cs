using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody theRb;
    public float moveSpeed;

    public GameObject impactEffect;

    public float damageAmount;

    private bool hasDamaged;

    void Start()
    {
        theRb = GetComponent<Rigidbody>();
        theRb.velocity = transform.forward * moveSpeed ;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy"&&!hasDamaged) 
        {
            other.GetComponent<EnemyHealthController>().TakeDamage(damageAmount);
            hasDamaged = true;
        }
        Instantiate(impactEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    //check if specific object leave the screnn 
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
