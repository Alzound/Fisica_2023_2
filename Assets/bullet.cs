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
        StartCoroutine(Deactivate());
        //rg.AddExplosionForce(explosionForce, transform.forward, explosionRadius); 
    }

    private IEnumerator Deactivate()
    {
        this.transform.gameObject.SetActive(false); 
        yield return new WaitForSeconds(5); 
    }
}
