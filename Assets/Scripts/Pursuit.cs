using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pursuit : Steering
{
    private int T = 3;
    public GameObject pursuitTarget;
    private PlayerController _pController;

    private void Start()
    {
        _pController = pursuitTarget.GetComponent<PlayerController>();
        Target = pursuitTarget.transform.position;
        Velocity = Vector3.zero;
    }
    public override Vector3 GetForce()
    {
        Vector3 futurePosition = _pController.transform.position +_pController.velocity * T;
        return Seek(futurePosition);
    }
    private Vector3 Seek(Vector3 Target)
    {
        DesiredVelocity = (Target - Position).normalized * speed;
        Velocity = Vector3.ClampMagnitude(DesiredVelocity - Velocity, speed);
        return Velocity;
    }
}