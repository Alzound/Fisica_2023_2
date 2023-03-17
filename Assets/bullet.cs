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
        rg.AddForce(transform.forward * explosionForce, ForceMode.Impulse);
        StartCoroutine(Deactivate());
        //rg.AddExplosionForce(explosionForce, transform.forward, explosionRadius); 
    }

    private IEnumerator Deactivate()
    {
        
        yield return new WaitForSeconds(5);
        this.transform.gameObject.SetActive(false);
    }
}
