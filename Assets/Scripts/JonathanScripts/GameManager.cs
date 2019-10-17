using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class GameManager : MonoBehaviour
{
    public enum SHOTTYPE { Standard, AutoFire }

    public int score;
    public bool godMode;

    public PostProcessVolume ppVolume;
    public ColorGrading colorGrading;
    public Color defaultScreenColor;
    public Color tintScreenColor;
    public float currentColorScale;
    public float desiredColorScale;
    public float colorLerp;

    [Header("Shooting")]
    SHOTTYPE shotType;
    public SHOTTYPE ShotType
    {
        get { return shotType; }
        set
        {
            shotType = value;
            switch (shotType)
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

    private void Awake()
    {
        colorGrading.enabled.Override(true);
    }
    private void Update()
    {
        if (godMode)
        {
            shotType = SHOTTYPE.AutoFire;
            canShoot = true;
            autoFire = true;
        }

        currentColorScale = Mathf.Lerp(currentColorScale, desiredColorScale, colorLerp);
        colorGrading.colorFilter.value = Color.Lerp(defaultScreenColor, tintScreenColor, currentColorScale);

    }

    public void EnableScreenTint(Color color)
    {
        tintScreenColor = color;
    }
    public void DisableScreenTint()
    {
        tintScreenColor = defaultScreenColor;
    }
}