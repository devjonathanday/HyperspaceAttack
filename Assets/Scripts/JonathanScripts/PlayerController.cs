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

    [Header("Shooting")]
    public bool canShoot;
    public float shotRange; //Distance of raycast from camera to hit point
    public float bulletSpeed;
    [Space(10)]
    public bool autoFire;
    float fireTimeStamp;
    [SerializeField] float fireDelay;

    [Header("Visuals")]
    public float boostShakeThreshold;
    public float boostShakeIntensity;
    public float boostShakeDuration;

    [Header("References")]
    private Player player;
    public CameraController cam;
    public GameObject projectileBullet;
    public Transform firePoint;

    void Awake()
    {
        player = ReInput.players.GetPlayer(0);
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
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

        if (autoFire ? (player.GetButton("Fire1") && Time.time - fireTimeStamp > fireDelay) : player.GetButtonDown("Fire1"))
            ShootProjectile();

        #endregion
    }
    Vector3 GetBulletTarget()
    {
        Ray trajectory = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(trajectory, out RaycastHit hit, shotRange))
        {
            if(!hit.collider.gameObject.CompareTag("PlayerBullet") &&
               !hit.collider.gameObject.CompareTag("EnemyBullet")) //Disable aiming at bullets
                return hit.point;
        }
        return transform.position + (transform.forward * shotRange);
    }

    void ShootProjectile()
    {
        if (canShoot)
        {
            fireTimeStamp = Time.time;
            GameObject newBullet = Instantiate(projectileBullet, firePoint.position, Quaternion.identity);
            Vector3 trajectory = GetBulletTarget() - firePoint.position;
            newBullet.GetComponent<Rigidbody>().AddForce((trajectory.normalized * bulletSpeed) + rBody.velocity);
        }
    }

}