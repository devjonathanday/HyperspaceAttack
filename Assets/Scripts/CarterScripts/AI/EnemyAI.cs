using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public enum Type { Chaser }
    [Header("Debug Properties")]
    public uint ID;
    public Type type;

    [Header("Components")]
    public Transform tr;
    public Rigidbody rb;

    [Header("Enemy Settings")]
    public float damage;
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
    public bool itUp = true;
}
