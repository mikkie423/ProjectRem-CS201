using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatSpawner : MonoBehaviour
{
    public GameObject ratPrefab;
    public int maxRats;
    public int countRats;

    //public float spawnLimitZ;
    public float spawnLimitX;
    public float spawnLimitZ;


    public Vector3 currentSpawnPoint;

    private float targetTime;

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale += new Vector3(spawnLimitX, 1, spawnLimitZ);
    }
    private void Update()
    {
/*        GameObject[] ratsTotal = GameObject.FindGameObjectsWithTag("Enemy");
        countRats = ratsTotal.Length;*/
        targetTime -= Time.deltaTime;
        if (targetTime <= 0 && countRats < maxRats)
        {
            SpawnRandomRat();
            targetTime = Random.Range(3, 6);
        }

    }

    void SpawnRandomRat()
    {
        // Generate  random spawn position
        Vector3 spawnPos = new Vector3(Random.Range((transform.position.x + spawnLimitX/2), (transform.position.x - spawnLimitX / 2)), 1, Random.Range((transform.position.z + spawnLimitZ / 2), (transform.position.z - spawnLimitZ / 2)));
        currentSpawnPoint = spawnPos;


        // instantiate enemy at random spawn location
        Instantiate(ratPrefab, spawnPos, ratPrefab.transform.rotation);
        countRats++;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(currentSpawnPoint, .5f);
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(new Vector3(transform.position.x, 1, transform.position.z), new Vector3(spawnLimitX, 1, spawnLimitZ));    
    }
}
