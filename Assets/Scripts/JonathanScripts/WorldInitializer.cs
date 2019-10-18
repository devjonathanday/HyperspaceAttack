using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldInitializer : MonoBehaviour
{
    public GameObject[] bubbleTypes;
    public int minimumBubbles;
    public int maximumBubbles;
    public GameObject[] bubbleList;
    public Vector2 bubbleSpawnRadius;

    public float timer = 0;
    public float timerCheckIncrement;

    void Start()
    {
        Initialize();
    }
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > timerCheckIncrement)
        {
            timer = 0;
            CheckForNewBubbles();
        }
    }

    public void Initialize()
    {
        bubbleList = new GameObject[maximumBubbles];
        for (int i = 0; i < maximumBubbles; i++)
        {
            SpawnNewBubble(GetNewSpawnPos(GetDistanceFromIncrement(i)), i);
            //Vector3 spawnDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
            //float spawnDistance = ((i / bubbleSpawnCount) * (bubbleSpawnRadius.y - bubbleSpawnRadius.x) + bubbleSpawnRadius.x);
            //Instantiate(bubbles[Random.Range(0, bubbleTypes.Length)], spawnDirection * spawnDistance, Quaternion.identity);
        }
    }
    public Vector3 GetNewSpawnPos(float distance)
    {
        return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * distance;
    }
    public void SpawnNewBubble(Vector3 position, int ID)
    {
        GameObject newBubble = Instantiate(bubbleTypes[Random.Range(0, bubbleTypes.Length)], position, Quaternion.identity);
        bubbleList[ID] = newBubble;
        newBubble.GetComponent<S_ShieldZone>().listID = ID;
    }
    public float GetDistanceFromIncrement(float increment)
    {
        return ((increment / maximumBubbles) * (bubbleSpawnRadius.y - bubbleSpawnRadius.x) + bubbleSpawnRadius.x);
    }
    public void CheckForNewBubbles()
    {
        for(int i = 0; i < maximumBubbles; i++)
        {
            if(bubbleList[i] == null)
                SpawnNewBubble(GetNewSpawnPos(GetDistanceFromIncrement(i)), i);
        }
    }
}