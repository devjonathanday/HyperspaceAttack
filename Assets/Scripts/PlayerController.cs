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
    public float shotRange; //Distance of raycast from camera to hit point
    public float bulletSpeed;

    [Header("References")]
    private Player player;

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
        #region Input

        //Camera Movement
        mouseDelta.x = player.GetAxis("MouseX");
        mouseDelta.y = player.GetAxis("MouseY");

        transform.Rotate(mouseDelta.y * (invertYAxis ? lookSpeed : -lookSpeed), mouseDelta.x * lookSpeed, 0);

        //Flying Movement
        inputVector.x = player.GetAxis("Horizontal");
        inputVector.y = player.GetAxis("Vertical");
        inputVector.z = player.GetAxis("Forward");

        rBody.AddRelativeForce(inputVector.normalized * moveSpeed);

        //Shooting
        if (player.GetButtonDown("Fire1"))
            ShootProjectile();

        #endregion
    }

    void ShootProjectile()
    {
        GameObject newBullet = Instantiate(projectileBullet, firePoint.position, Quaternion.identity);
        Vector3 trajectory = GetBulletTarget() - firePoint.position;
        newBullet.GetComponent<Rigidbody>().AddForce(trajectory.normalized * bulletSpeed);
    }

    Vector3 GetBulletTarget()
    {
        Ray trajectory = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(trajectory, out RaycastHit hit, shotRange))
            return hit.point;
        else return transform.position + (transform.forward * shotRange);
    }
}