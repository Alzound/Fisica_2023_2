using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Vector3 _target;
    public NewSeek newSeek;
    public Wander wander; 

    private void FixedUpdate()
    {
        newSeek.Target = _target;
        wander.Target = _target; 
    }
}

  
