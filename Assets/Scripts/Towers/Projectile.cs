using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody theRb;
    public float moveSpeed;


    void Start()
    {
        theRb = GetComponent<Rigidbody>();
        theRb.velocity = transform.forward * moveSpeed ;
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }

    //check if specific object leave the screnn 
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
