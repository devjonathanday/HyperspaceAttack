using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public enum Type { chaser }
    [Header("Debug Properties")]
    public uint ID;
    public Type type;

    [Header("Components")]
    public Rigidbody rb;

    [Header("Enemy Settings")]
    public int damage;
    public int speed;
    public int rotSpeed;

    [Header("Acquired Fields")]
    public GameObject target;
    public Vector3 targetLoc;

    AIBehaviours root { get; set; }
}
