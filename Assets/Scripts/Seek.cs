
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Seek : Steering
{
    [SerializeField]
    bool arrival; 
    float slowingRadius = 2; 

    public override Vector3 GetForce()
    {
        DesiredVelocity = (Target - Position).normalized * speed;
        DesiredVelocity = (Target - Position);
        float distance = DesiredVelocity.magnitude;
        //Debug.Log(distance);
        if (distance < slowingRadius)
        {
            arrival = true;
            DesiredVelocity = DesiredVelocity.normalized * speed * (distance / slowingRadius);
        }
        else
        {
            arrival = false;
            DesiredVelocity = (Target - Position).normalized * speed;
        }

        return DesiredVelocity - Velocity;
    }
}
