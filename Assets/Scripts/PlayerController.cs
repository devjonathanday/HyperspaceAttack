using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Input")]
    private Vector2 mouseDelta;
    public float lookSpeed;

    [Header("References - Self")]
    public Transform cam;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    void Update()
    {
        #region Input

        mouseDelta.x = Input.GetAxis("Mouse X");
        mouseDelta.y = Input.GetAxis("Mouse Y");

        cam.Rotate(-mouseDelta.y * lookSpeed, mouseDelta.x * lookSpeed, 0);

        #endregion
    }
}