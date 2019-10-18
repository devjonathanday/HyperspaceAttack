using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public enum BULLETTYPE { Projectile, Grenade, Laser }
    [Header("Standard Attributes")]
    public BULLETTYPE type;
    public float lifeTime;
    public float deathDuration;
    private float timer;
    private bool deathSequence;
    public Rigidbody rBody;
    public GameObject explosionEffect;

    [Header("Additional Attributes")]
    public float drag;

    void Update()
    {
        timer += Time.deltaTime;
        if (deathSequence)
        {
            if (timer > lifeTime + deathDuration)
                Destroy(gameObject);
        }
        else if (timer > lifeTime) Death();

        switch (type)
        {
            case BULLETTYPE.Grenade:
                rBody.velocity *= drag;
                break;
        }
    }
    void Death()
    {
        switch (type)
        {
            case BULLETTYPE.Projectile:
                deathSequence = true;
                rBody.velocity = Vector3.zero;
                Instantiate(explosionEffect, transform.position, Quaternion.identity);
                break;
            case BULLETTYPE.Grenade:
                Instantiate(explosionEffect, transform.position, Quaternion.identity);
                Destroy(gameObject);
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(this.gameObject.tag + " triggered with: " + other.gameObject.tag + " on layer: " + other.gameObject.layer);
        if(other.gameObject.layer == 15)
        {
            Death();
        }
    }
}