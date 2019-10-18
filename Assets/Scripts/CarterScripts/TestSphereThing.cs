using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSphereThing : MonoBehaviour
{
    public float r;
    public float s;
    public float t;
    public float p;


    public GameObject sphere;
    public GameObject tracker;

    void Start()
    {
        r = sphere.transform.localScale.x / 2;
        s = Vector3.Angle(transform.up, Vector3.right) * Mathf.PI / 180;
    }

    // Update is called once per frame
    void Update()
    {
        tracker.transform.position = new Vector3((r + p) * Mathf.Cos(s) * Mathf.Sin(t), (r + p) * Mathf.Sin(s) * Mathf.Sin(t), (r + p) * Mathf.Cos(t));
    }
}
