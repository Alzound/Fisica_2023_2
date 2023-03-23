using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour
{
    [Header("Bullet")]
    [SerializeField]
    private bool isReady = true;
    [SerializeField]
    private GameObject left, right, prefab;
    [SerializeField]
    float explosionForceMagnitude;
    [SerializeField]
    ParticleSystem explosion; 

    private void Start()
    {
        StartCoroutine(ShotsFired()); 
    }

    public IEnumerator ShotsFired()
    {
        while(isReady)
        {
            Instantiate(prefab, right.transform);
            Instantiate(prefab, left.transform);
            yield return new WaitForSeconds(5);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            foreach (ContactPoint contact in collision.contacts)
            {
                // Calculate the direction from the contact point to the object
                Vector3 direction = contact.point - transform.position;

                // Apply the explosion force to the colliding object
                Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
                rb.AddForce(direction.normalized * explosionForceMagnitude, ForceMode.Impulse);

                // Instantiate a particle system prefab at the point of collision
                explosion.gameObject.SetActive(true);
                StartCoroutine(Deactivate()); 
            }
        }
    }

    private IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(2);
        this.gameObject.SetActive(false);
    }
}
