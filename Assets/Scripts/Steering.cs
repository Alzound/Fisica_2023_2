using UnityEngine;

public abstract class Steering : MonoBehaviour
{
    public float speed = 5f;
    public Vector3 DesiredVelocity { get; set; }
    public Vector3 Velocity { get; set; }
    public Vector3 Position { get; set; }
    public Vector3 Target { get; set; }

    public abstract Vector3 GetForce();
}
