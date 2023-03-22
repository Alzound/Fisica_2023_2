using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : Steering
{
    public float circleDistance = 1;
    public float circleRadius = .5f;
    public float[] targetChange = new float[] { 1f, 10f };
    public float[] targetSpace = new[] { 8f, 5f };
    public float[] angleChange = new[] { 1f, 20f };
    public float[] angleRange = new[] { -45f, 45f };
    public bool showVectors = true;
    [SerializeField]
    private bool _startRandom = true;
    private float _rotationAngle = 10f;
    private Seek _seek;

    private void Start()
    {
        _seek = GetComponent<Seek>();
        StartCoroutine(RandomTarget());
        StartCoroutine(RandomAngle());
    }

    public override Vector3 GetForce()
    {
        Vector3 circleCenter = Velocity.normalized * circleDistance;
        //Vector3 cirlceP = transform.position + circleCenter;
        Vector3 displesment = Vector3.right * circleRadius;
        Quaternion rotate = Quaternion.AngleAxis(_rotationAngle, Vector3.forward);
        displesment = rotate * displesment;
        Vector3 wanderforce = circleCenter + displesment;
        if (showVectors == true)
        {
            DrawVectors();
        }
        return wanderforce;
    }
    
    IEnumerator RandomTarget()
    {
        while(_startRandom)
        {
            Vector3 randomTarget = new Vector3(Random.Range(-25f, 25),
                                           0,
                                           Random.Range(-25,25));
            Debug.Log(randomTarget + " target");
            _seek.Target = randomTarget;
            yield return new WaitForSeconds(15);
        }
    }

    IEnumerator RandomAngle()
    {
        while(_startRandom)
        {
            Debug.Log("angle");
            _rotationAngle = Random.Range(angleRange[0], angleRange[1]);
            yield return new WaitForSeconds(15);
        }
    }

    private void DrawVectors()
    {
        Vector3 circleCenter = Velocity.normalized * circleDistance;
        Vector3 cirlceP = transform.position + circleCenter;
        Vector3 displesment = Vector3.up * circleRadius;
        Quaternion rotate = Quaternion.AngleAxis(_rotationAngle, Vector3.forward);
        displesment = rotate * displesment;
        Vector3 wanderforce = circleCenter + displesment;

        Debug.DrawLine(transform.position, cirlceP, Color.red);
        Debug.DrawLine(cirlceP, cirlceP + displesment, Color.blue);
        Debug.DrawLine(transform.position, transform.position + wanderforce, Color.green);
    }
}
