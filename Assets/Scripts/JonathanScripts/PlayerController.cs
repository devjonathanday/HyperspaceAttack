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
    public float boostShakeThreshold;
    public float boostShakeIntensity;
    public float boostShakeDuration;

    public Animator gunAnimator;

    [Header("References")]
    private Player player;
    public CameraController cam;
    public GameObject projectileBullet;
    public Transform firePoint;
    public GameManager GM;

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

        mouseDelta.x = player.GetAxis("MouseX");
        mouseDelta.y = player.GetAxis("MouseY");
        transform.Rotate(mouseDelta.y * (invertYAxis ? lookSpeed : -lookSpeed), mouseDelta.x * lookSpeed, 0);

        #endregion

        #region Flying

        inputVector.x = player.GetAxis("Horizontal");
        inputVector.y = player.GetAxis("Vertical");
        inputVector.z = player.GetAxis("Forward");

        if (inputVector.y > boostShakeThreshold)
            cam.ScreenShake(boostShakeIntensity, boostShakeDuration);

        rBody.AddRelativeForce(inputVector.normalized * moveSpeed, ForceMode.Acceleration);

        #endregion

        #region Shooting

        if (GM.autoFire ? (player.GetButton("Fire1") && Time.time - GM.fireTimeStamp > GM.fireDelay) : player.GetButtonDown("Fire1"))
            ShootProjectile();

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
            gunAnimator.SetTrigger("Shoot");
            GM.fireTimeStamp = Time.time;
            GameObject newBullet = Instantiate(projectileBullet, firePoint.position, transform.rotation);
            Vector3 trajectory = GetBulletTarget() - firePoint.position;
            newBullet.GetComponent<Rigidbody>().AddForce((trajectory.normalized * GM.bulletSpeed) + rBody.velocity);
        }
    }

}