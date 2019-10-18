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
    [Range(1f, 360f)]
    public float sphereStep;
    public float spherePadding;

    [Header("Acquired Fields")]
    public GameObject target;
    public Vector3 targetLoc;

    [HideInInspector]
    public float ? distToTarget;
    [HideInInspector]
    public RaycastHit hit;
    [HideInInspector]
    public Vector3[] sphereLocs;

}
