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

    public List<GameObject> bullets_Container = new List<GameObject>();
    public int amount = 10;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        rg = GetComponent<Rigidbody>(); 

        move = playerInput.actions["Movement"];
        shoot = playerInput.actions["Shoot"];
        //shootRight = playerInput.actions["RightClick"];

        for (int i = 0; i <= amount; i++) //este lo realiza por la cantidad de balas, -intancia objeto-lo desactiva-lo agrega a la lista
        {
            var prefabInstance = Instantiate(prefab);
            prefabInstance.SetActive(false);
            bullets_Container.Add(prefabInstance);
        }
    }

    private void OnEnable()
    {
        move.performed += Movement;
        shoot.performed += ShootAction;
        //shootRight.performed += RightClickAction;

    }

    private void OnDisable()
    {


    }

    public void Movement(InputAction.CallbackContext context)
    {
        //Debug.Log(context.ReadValue<Vector3>());
        if(context.ReadValue<Vector3>() == Vector3.up)
        {
            //Debug.Log("enter"); 
            rg.velocity = Vector3.up * jumpForce * 5 * Time.deltaTime; 
        }
        rg.AddForce(context.ReadValue<Vector3>() * speedMovement * 5 * Time.deltaTime, ForceMode.Force);
    }



    public void ShootAction(InputAction.CallbackContext context)
    {
        Debug.Log(context + " contexto");
        if(context.ReadValue<float>() >= 1)
        {
            Debug.Log("enter right");
            Instantiate(prefab, positionRight.transform);
        }
        if (context.ReadValue<float>() <= -1)
        {
            Debug.Log("enter left");
            Instantiate(prefab, positionLeft.transform);
        }

    }

    public GameObject getNewBullet() //esta funcion es para darle un objeto desactivado al input y que este lo active
    {
        if (amount > 0)
        {
            for (int i = 0; i <= amount; i++)
            {
                if (!bullets_Container[i].activeInHierarchy) //comienza el conteo, si no esta activo, lo manda al input pasando por todos los objetos
                {
                    return bullets_Container[i];
                }
            }
        }
        if (amount <= 0)
        {

            //animator.SetBool("reload", true);
            Debug.Log("reload anim");
        }
        return null;
    }

}