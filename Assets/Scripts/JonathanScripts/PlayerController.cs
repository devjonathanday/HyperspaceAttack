using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerController : MonoBehaviour
{
    [Header("Input")]
    private Vector2 mouseDelta;
    public Vector3 inputVector;
    public float lookSpeed;
    public bool invertYAxis;

    [Header("Physics")]
    public Rigidbody rBody;
    public float moveSpeed;
    public LayerMask hitScanLayers;

    [Header("Visuals")]
    public float laserShakeIntensity;
    public float laserShakeDuration;

    public Animator gunAnimator;

    [Header("References")]
    private Player player;
    public CameraController cam;
    public Transform firePoint;
    public GameManager GM;
    public GameObject laser;

    public AudioClip orangeShot;
    public AudioClip blueShot;
    public AudioClip greenShot;
    public AudioSource audioSource;

    void Awake()
    {
        player = ReInput.players.GetPlayer(0);
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        GM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        #region Camera
        if (Time.timeScale != 0)
        {
            mouseDelta.x = player.GetAxis("MouseX");
            mouseDelta.y = player.GetAxis("MouseY");
            transform.Rotate(mouseDelta.y * (invertYAxis ? lookSpeed : -lookSpeed), mouseDelta.x * lookSpeed, 0);
        }

        #endregion

        #region Flying

        inputVector.x = player.GetAxis("Horizontal");
        inputVector.y = player.GetAxis("Vertical");
        inputVector.z = player.GetAxis("Forward");

        rBody.AddRelativeForce(inputVector.normalized * moveSpeed, ForceMode.Acceleration);

        #endregion

        #region Shooting

        if (GM.ShotType != GameManager.SHOTTYPE.Laser)
        {
            laser.SetActive(false);
            if ((GM.autoFire ? player.GetButton("Fire1") : player.GetButtonDown("Fire1")) && Time.time - GM.fireTimeStamp > GM.fireDelay)
                ShootProjectile();
        }
        else
        {
            if (player.GetButton("Fire1"))
            {
                laser.SetActive(true);
                cam.ScreenShake(laserShakeIntensity, laserShakeDuration);
            }
            else laser.SetActive(false);
        }

        #endregion
    }
    Vector3 GetBulletTarget()
    {
        Ray trajectory = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(trajectory, out RaycastHit hit, GM.shotRange, hitScanLayers))
                return hit.point;

        return transform.position + (transform.forward * GM.shotRange);
    }

    void ShootProjectile()
    {
        if (GM.canShoot)
        {
            switch(GM.ShotType)
            {
                case GameManager.SHOTTYPE.AutoFire:
                    audioSource.PlayOneShot(orangeShot);
                    gunAnimator.SetTrigger("OrangeShot");
                    break;
                case GameManager.SHOTTYPE.GrenadeLauncher:
                    audioSource.PlayOneShot(blueShot);
                    gunAnimator.SetTrigger("BlueShot");
                    break;
            }

            if (GM.ShotType != GameManager.SHOTTYPE.Laser)
            {
                GM.fireTimeStamp = Time.time;
                GameObject newBullet = Instantiate(GM.currentBullet, firePoint.position, transform.rotation);
                Vector3 trajectory = GetBulletTarget() - firePoint.position;
                newBullet.GetComponent<Rigidbody>().AddForce((trajectory.normalized * GM.bulletSpeed) + rBody.velocity);
            }
        }
    }

}