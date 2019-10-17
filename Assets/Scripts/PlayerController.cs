using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerController : MonoBehaviour
{
    [Header("Input")]
    private Vector2 mouseDelta;
    public Vector2 inputVector;
    public float lookSpeed;
    public bool invertYAxis;

    [Header("Physics")]
    public Rigidbody rBody;
    public float moveSpeed;

    [Header("References")]
    private Player player;

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
        mouseDelta.x = Input.GetAxis("Mouse X");
        mouseDelta.y = Input.GetAxis("Mouse Y");

        transform.Rotate(mouseDelta.y * (invertYAxis ? lookSpeed : -lookSpeed), mouseDelta.x * lookSpeed, 0);

        //Flying Movement
        inputVector.x = player.GetAxis("Horizontal");
        inputVector.y = player.GetAxis("Vertical");

        rBody.AddRelativeForce(new Vector3(inputVector.x, 0, inputVector.y).normalized * moveSpeed);

        #endregion
    }
}