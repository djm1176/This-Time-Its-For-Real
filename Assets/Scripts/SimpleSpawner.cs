using System.Collections;
using UnityEngine;

public class SimpleSpawner : MonoBehaviour
{
    public GameObject spawnObj;
    public float spawnRate;
    public int spawnCount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine("SpawnCoroutine");
    }

    IEnumerator SpawnCoroutine()
    {
        while (spawnCount != 0)
        {
            spawnCount--;
            Instantiate(spawnObj, transform.position, transform.rotation);

            yield return new WaitForSeconds(spawnRate);
        }
        
    }

}
