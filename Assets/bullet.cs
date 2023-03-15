using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rg;
    [SerializeField]
    private float explosionForce, explosionRadius;

    private void Awake()
    {
        rg = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rg.AddForce(transform.position * 5 * 1000 * Time.deltaTime, ForceMode.Impulse); 
        //rg.AddExplosionForce(explosionForce, transform.forward, explosionRadius); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
