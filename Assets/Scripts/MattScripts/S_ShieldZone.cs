using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_ShieldZone : MonoBehaviour
{
    private bool characterInside;
    public float shrinkSpeed;
    public GameManager.SHOTTYPE shotType;
    public GameObject player;
    public GameObject bulletPrefab;
    public GameManager GM;

    [ColorUsage(true, true)] public Color screenTint;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        GM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject == player)
            EnableShooting();
    }

    //Only uncomment if collider messages start missing
    //void OnTriggerStay(Collider collision)
    //{
    //    DUPLICATE OF OnTriggerEnter()
    //}

    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject == player)
            DisableShooting();
    }

    void ShrinkZone()
    {
        if (characterInside)
        {
            transform.localScale -= Vector3.one * Time.deltaTime * shrinkSpeed;
            if (transform.localScale.x <= 0)
            {
                Destroy(gameObject);
                DisableShooting();
            }
        }
    }

    void Update()
    {
        ShrinkZone();
    }

    public void EnableShooting()
    {
        if (GM.ShotType != GameManager.SHOTTYPE.Laser)
        {
            GM.currentBullet = bulletPrefab;
        }
        GM.canShoot = true;   
        characterInside = true;
        GM.ShotType = shotType;
        GM.EnableScreenTint(screenTint);
    }
    public void DisableShooting()
    {
        characterInside = false;
        GM.canShoot = false;
        GM.ShotType = GameManager.SHOTTYPE.Standard;
        GM.DisableScreenTint();
    }
}
