using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 _target;
    public NewSeek newSeek;
    public Vector3 velocity = Vector3.zero;

    private void FixedUpdate()
    {
        newSeek.Target = _target;
    }
}
