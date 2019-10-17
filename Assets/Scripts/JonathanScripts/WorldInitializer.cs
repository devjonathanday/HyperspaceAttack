using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldInitializer : MonoBehaviour
{
    public GameObject[] bubbles;
    public float bubbleSpawnCount;
    public Vector2 bubbleSpawnRadius;

    public void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        for (int i = 0; i < bubbleSpawnCount; i++)
        {
            Vector3 spawnDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
            float spawnDistance = ((i / bubbleSpawnCount) * (bubbleSpawnRadius.y - bubbleSpawnRadius.x) + bubbleSpawnRadius.x);
            Instantiate(bubbles[Random.Range(0, bubbles.Length)], spawnDirection * spawnDistance, Quaternion.identity);
        }
    }
}