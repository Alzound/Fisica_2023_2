using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject prefab; 

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemies(); 
    }

    private void SpawnEnemies()
    {
        Instantiate(prefab, new Vector3(Random.Range(100, -100), 0, Random.Range(100, -100)), Quaternion.identity);
        StartCoroutine(respawn());
    }

    private IEnumerator respawn()
    {
        yield return new WaitForSeconds(6);
        SpawnEnemies(); 
    }
}
