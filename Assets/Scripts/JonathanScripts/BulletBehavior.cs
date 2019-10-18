using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float lifeTime;
    public float deathDuration;
    private float timer;
    private bool deathSequence;
    public Rigidbody rBody;
    public GameObject explosionEffect;

    void Update()
    {
        timer += Time.deltaTime;
        if(deathSequence)
        {
            if (timer > lifeTime + deathDuration)
                Destroy(gameObject);
        }
        else if(timer > lifeTime) Death();
    }
    void Death()
    {
        deathSequence = true;
        rBody.velocity = Vector3.zero;
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
    }
}