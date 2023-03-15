using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemie : MonoBehaviour
{

    [Header("Wander")]
    public Vector3 targetChange;
    public float circleDistance = 1, circleRadius = .5f, angleChange;
    private Vector3 velocityW;
    private float maxspeedW = 2, speedW = 2;
    private Queue<Vector3> targetQueue = new Queue<Vector3>();

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


    // Start is called before the first frame update
    void Start()
    {
        _actions.Add("Wander", CalculateWander);
        FillTargetQueue();
        StartCoroutine(ChangeTarget());

        rg = GetComponent<Rigidbody>(); 
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
        rg.AddForce(transform.position * 5 * 1000 * Time.deltaTime, ForceMode.Impulse);
        yield return new WaitForSeconds(5);
        isReady = true;
    }

    void CalculateWander()
    {
        Vector3 circleCenter = velocityW.normalized * circleDistance;
        Vector3 displacement = Vector3.right * circleRadius;
        StartCoroutine(ChangeAngle());
        SetAngle(displacement, angleChange);

        Vector3 wanderForce = circleCenter + displacement;

        //Debug.DrawLine(circleCenter + transform.position, circleCenter + transform.position + displacement, Color.blue);
        //Debug.DrawLine(transform.position, transform.position + circleCenter, Color.yellow);
        //Debug.DrawLine(transform.position, transform.position + wanderForce, Color.red);

        steering = SeekW(wanderForce) + SeekW(targetChange);

        MoveW(steering);
    }

    private void SetAngle(Vector3 vector, float value)
    {
        float len = vector.magnitude;
        vector.x = Mathf.Cos(value) * len;
        vector.y = Mathf.Sin(value) * len;
    }

    Vector3 SeekW(Vector3 seekTarget)
    {
        Vector3 desiredVelocity = (seekTarget - transform.position).normalized * maxspeedW;
        return desiredVelocity - velocityW;
    }

    void MoveW(Vector3 steering)
    {
        velocityW += steering;
        velocityW = Vector3.ClampMagnitude(velocityW, maxspeedW);
        transform.position += velocityW * Time.deltaTime;
    }

    private void FillTargetQueue()
    {
        for (int i = 0; i < 10; i++)
        {
            Vector3 randomTarget = new Vector3(UnityEngine.Random.Range(-50f, 50f), 0, UnityEngine.Random.Range(-50f, 50f));

            targetQueue.Enqueue(randomTarget);
        }
    }


    IEnumerator ChangeTarget()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);
            Debug.Log("Hola cada 5 seg");
            if (targetQueue.Count <= 0)
            {
                FillTargetQueue();
            }
            targetChange = targetQueue.Dequeue();
            Quaternion rot = Quaternion.AngleAxis(angleChange, Vector3.forward);
            transform.rotation = rot;
            //prefab.transform.position = targetChange;
        }
    }

    private IEnumerator ChangeAngle()
    {
        angleChange = UnityEngine.Random.Range(0, 360);
        yield return new WaitForSeconds(.5f);
    }
}
