using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSeek : Steering
{
    public float SeekChase = 3f;
    public float safeRadius = 6f;

    public override Vector3 GetForce()
    {
        DesiredVelocity = (Target - Position);
        float distance = DesiredVelocity.magnitude;
        if (distance < SeekChase)
        {
            DesiredVelocity = (Target - Position).normalized * speed;
            return (DesiredVelocity - Velocity) * 1;
        }
        else if (distance > safeRadius)
        {
            DesiredVelocity = Vector3.zero;
            return DesiredVelocity - Velocity;
        }
        else return DesiredVelocity = Vector3.zero;
    }
}