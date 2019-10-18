using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_ShieldZone : MonoBehaviour
{
    public bool characterInside;
    public float currentSize;
    public float desiredSize;
    public float maxScale;
    public float sizeLerp;
    public float shrinkSpeed;
    public float killSize;
    public GameManager.SHOTTYPE shotType;
    public GameObject player;
    public GameObject bulletPrefab;
    public GameManager GM;
    public int listID;

    [ColorUsage(true, true)] public Color screenTint;

    public void Awake()
    {
        transform.localScale = Vector3.zero;
        currentSize = 0;
        desiredSize = maxScale;
}

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
            desiredSize -= Time.deltaTime * shrinkSpeed;
            if (currentSize <= killSize)
            {
                DisableShooting();
                Destroy(gameObject);
            }
        }
    }

    void Update()
    {
        ShrinkZone();
        currentSize = Mathf.Lerp(currentSize, desiredSize, sizeLerp);
        transform.localScale = Vector3.one * currentSize;
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

    IEnumerator Spawn(float growSpeed)
    {
        for (int i = 0; i < 60; i++)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * maxScale, growSpeed);
            yield return null;
        }
        transform.localScale = Vector3.one * maxScale;
    }
}
