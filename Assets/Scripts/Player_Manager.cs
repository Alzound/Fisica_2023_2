using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

using UnityEngine.UIElements;
using TMPro;
using Cinemachine;


public class Player_Manager : MonoBehaviour
{
    [Header("Input System")]
    [SerializeField]
    private PlayerInput playerInput;
    private InputAction shoot, move;
    [SerializeField]
    private Rigidbody rg;
    [SerializeField]
    private int jumpForce = 10, speedMovement = 10;
    [SerializeField]
    private GameObject positionLeft, positionRight;
    [SerializeField]
    private GameObject prefab;

    public Vector3 tempVector; 

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        rg = GetComponent<Rigidbody>(); 

        move = playerInput.actions["Movement"];
        shoot = playerInput.actions["Shoot"];
        //shootRight = playerInput.actions["RightClick"];
    }

    private void OnEnable()
    {
        move.performed += Movement;
        shoot.performed += ShootAction;
        //shootRight.performed += RightClickAction;
    }

    private void OnDisable()
    {
        move.performed -= Movement;
        shoot.performed -= ShootAction;
    }

    private void FixedUpdate()
    {
        rg.AddForce(tempVector * speedMovement * Time.deltaTime); 
    }

    public void Movement(InputAction.CallbackContext context)
    {
        //Debug.Log(context.ReadValue<Vector3>());
        if(context.ReadValue<Vector3>() == Vector3.up)
        {
            Debug.Log("enter"); 
            rg.AddForce( Vector3.up * jumpForce); 
        }
        tempVector = context.ReadValue<Vector3>();
    }



    public void ShootAction(InputAction.CallbackContext context)
    {
        //Debug.Log(context + " contexto");
        if(context.ReadValue<float>() >= 1)
        {
            Debug.Log("enter right");
            Instantiate(prefab, positionRight.transform.position, positionRight.transform.rotation);
            rg.AddExplosionForce(1005, positionRight.transform.position, 10);
        }
        if (context.ReadValue<float>() <= -1)
        {
            Debug.Log("enter left");
            Instantiate(prefab, positionLeft.transform.position, positionLeft.transform.rotation);
            rg.AddExplosionForce(1005, positionLeft.transform.position, 10);
        }

    }

}