using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum SHOTTYPE { Standard, AutoFire, GrenadeLauncher, Laser }

    private int score;
    public int Score
    {
        get { return score; }
        set
        {
            score = value;
            scoreText.text = "Score: " + score;
        }
    }
    public bool godMode;

    public PostProcessVolume ppVolume;
    public ColorGrading colorGrading;
    [ColorUsage(true, true)] public Color defaultScreenColor;
    [ColorUsage(true, true)] public Color tintScreenColor;
    public float currentColorScale;
    public float desiredColorScale;
    public float colorLerp;
    public float colorSettleAmount;
    public GameObject currentBullet;
    public MeshRenderer gunRenderer;
    private List<Material> gunMaterials = new List<Material>();
    public Material[] gunGlassMaterials;
    public TextMeshProUGUI scoreText;

    public AudioClip enterBubbleSound;
    public AudioClip exitBubbleSound;
    public AudioSource audioSource;

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
                    gunMaterials[1] = gunGlassMaterials[0];
                    gunRenderer.materials = gunMaterials.ToArray();
                    break;
                case SHOTTYPE.AutoFire:
                    autoFire = true;
                    gunMaterials[1] = gunGlassMaterials[1];
                    gunRenderer.materials = gunMaterials.ToArray();
                    fireDelay = 0.25f;
                    break;
                case SHOTTYPE.GrenadeLauncher:
                    autoFire = false;
                    gunMaterials[1] = gunGlassMaterials[2];
                    gunRenderer.materials = gunMaterials.ToArray();
                    fireDelay = 1;
                    break;
                case SHOTTYPE.Laser:
                    autoFire = true;
                    gunMaterials[1] = gunGlassMaterials[3];
                    gunRenderer.materials = gunMaterials.ToArray();
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
        ppVolume.profile.TryGetSettings(out colorGrading);
        colorGrading.enabled.Override(true);
        gunRenderer.GetMaterials(gunMaterials);
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
        currentColorScale = 1;
        desiredColorScale = colorSettleAmount;
        audioSource.PlayOneShot(enterBubbleSound);
    }
    public void DisableScreenTint()
    {
        desiredColorScale = 0;
        audioSource.PlayOneShot(exitBubbleSound);
    }

   public void ButtonExitClicked()
    {
        Application.Quit();
    }

    public void PlayerDied()
    {
        Time.timeScale = 0;

    }

    public void RestartButtonClicked()
    {
        SceneManager.LoadScene("JonathanTest");
    }
}