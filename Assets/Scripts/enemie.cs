using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class enemie : Steering
{
    [Header("Wander")]
    public float circleDistance;
    public float circleRadius;
    public float[] targetChange = new float[] { 1f, 10f };
    public float[] targetSpace = new[] { 8f, 5f };
    public float[] angleChange = new[] { 1f, 20f };
    public float[] angleRange = new[] { -45f, 45f };
    public bool showVectors = true;
    [SerializeField]
    private bool _startRandom = true;
    private float _rotationAngle = 10f;
    private Seek _seek;


    [Header("Bullet")]
    [SerializeField]
    private bool isReady = true; 
    private Rigidbody rg;
    [SerializeField]
    private GameObject left, right, prefab; 
    private enum SteringType
    {
        Seek, RunAway, Wander
    }

    [SerializeField]
    private SteringType _type = SteringType.Seek;
    private Dictionary<string, Action> _actions = new Dictionary<string, Action>();
    private Vector3 steering;


    

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
        while (_startRandom)
        {
            Vector3 randomTarget = new Vector3(UnityEngine.Random.Range(-8f, targetSpace[0]),
                                           UnityEngine.Random.Range(-5f, targetSpace[1]),
                                           0);
            Debug.Log(randomTarget + " target");
            _seek.Target = randomTarget;
            yield return new WaitForSeconds(UnityEngine.Random.Range(targetChange[0], targetChange[1]));
        }
    }

    IEnumerator RandomAngle()
    {
        while (_startRandom)
        {
            Debug.Log("angle");
            _rotationAngle = UnityEngine.Random.Range(angleRange[0], angleRange[1]);
            yield return new WaitForSecondsRealtime(UnityEngine.Random.Range(angleChange[0], angleChange[1]));
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


    // Update is called once per frame
    void Update()
    {
        if(isReady == true)
        {
            StartCoroutine(ShotsFired());
        }
    }

    public IEnumerator ShotsFired()
    {
        isReady = false; 
        Instantiate(prefab, right.transform);
        Instantiate(prefab, left.transform);
        //rg.AddForce(transform.position * 5 * 1000 * Time.deltaTime, ForceMode.Impulse);
        yield return new WaitForSeconds(5);
        isReady = true;
    }


}
