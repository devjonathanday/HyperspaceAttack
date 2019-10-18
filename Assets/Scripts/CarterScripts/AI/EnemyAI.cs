using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public enum Type { Chaser }
    [Header("Debug Properties")]
    public uint ID;
    public Type type;
    public GameManager gm;
    public GameObject explosionParticle;

    [Header("Components")]
    public Transform tr;
    public Rigidbody rb;

    [Header("Enemy Settings")]
    public int damage;
    public int score;
    public float seekSpeed;
    public float seekDist;
    public float chaseSpeed;
    public float chaseDist;
    public float ramSpeed;
    public float rotSpeed;
    public float ramDist;
    public float maxDist;
    public float allignDegree;
    [Range(0f, 1f)]
    public float brakeMultiplier;
    public float blockDist;
    public float sphereStep;
    public float spherePadding;
    public float orbitSpeed;

    [Header("Acquired Fields")]
    public GameObject target;

    [HideInInspector]
    public float ? distToTarget;
    [HideInInspector]
    public RaycastHit hit;
    public Vector3[] orbitLocs;
    public float currStep;
    public int currIndx;
    public bool isOrbiting;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision with: " + collision.gameObject.tag + " on layer: " + collision.gameObject.layer);
        if(collision.gameObject.layer == 9)
        {
            S_HealthComponent t = collision.gameObject.GetComponent<S_HealthComponent>();
            t.TakeDamage(damage);
            Deactivate(false);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered with: " + other.gameObject.tag + " on layer: " + other.gameObject.layer);
        if(other.gameObject.layer == 10)
        {
            Deactivate(true);
        }
    }

    void Deactivate(bool getScore)
    {
        if (getScore)
        {
            //gm.Score += score /* * gm.multiplier */;
        }
        Instantiate<GameObject>(explosionParticle);
        gameObject.SetActive(false);
    }
}
