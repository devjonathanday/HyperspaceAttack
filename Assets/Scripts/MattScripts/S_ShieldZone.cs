using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_ShieldZone : MonoBehaviour
{
    private bool characterInside;
    public float shrinkSpeed;
    public GameManager.SHOTTYPE shotType;
    public GameManager GM;

    private void Start()
    {
        GM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            characterInside = true;
            GM.canShoot = true;
            GM.ShotType = shotType;
        }
    }

    //Only uncomment if collider messages start missing
    //void OnTriggerStay(Collider collision)
    //{
    //    DUPLICATE OF OnTriggerEnter()
    //}

    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            characterInside = false;
            GM.canShoot = false;
        }
    }

    void ShrinkZone()
    {
        if (characterInside)
        {
            transform.localScale -= Vector3.one * Time.deltaTime * shrinkSpeed;
            if (transform.localScale.x <= 0)
                Destroy(gameObject);
        }
    }

    void Update()
    {
        ShrinkZone();
    }
}
