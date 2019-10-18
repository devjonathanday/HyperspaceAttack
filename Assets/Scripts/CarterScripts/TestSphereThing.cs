using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSphereThing : MonoBehaviour
{
    public float r;
    public float s;
    public float t;

    public Vector3 pos;

    public GameObject tracker;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        tracker.transform.position = new Vector3(r * Mathf.Cos(s) * Mathf.Sin(t), r * Mathf.Sin(s) * Mathf.Sin(t), r * Mathf.Cos(t));

        pos = tracker.transform.position;
    }
}
