using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum SHOTTYPE { Standard, AutoFire }

    public int score;

    [Header("Shooting")]
    SHOTTYPE shotType;
    public SHOTTYPE ShotType
    {
        get { return shotType; }
        set
        {
            shotType = value;
            switch(shotType)
            {
                case SHOTTYPE.Standard:
                    autoFire = false;
                    break;
                case SHOTTYPE.AutoFire:
                    autoFire = true;
                    break;
            }
        }
    }
    public bool canShoot;
    public bool autoFire;
    public float shotRange; //Distance of raycast from camera to hit point
    public float bulletSpeed;
    public float fireTimeStamp;
    public float fireDelay;
}