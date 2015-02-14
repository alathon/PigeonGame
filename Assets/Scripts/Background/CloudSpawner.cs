using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class CloudSpawner : ObjectPool
{
    public Vector3 cloudMinSpawn = new Vector3(-6.5f, 8f, 0f);
    public Vector3 cloudMaxSpawn = new Vector3(6.1f, 8f, 0f);
    public int cloudChance = 50;
    public float cloudMinScale = 2.5f;
    public float cloudMaxScale = 5f;
    public int minClouds = 1;
    public int maxClouds = 5;

    void Start()
    {
        InvokeRepeating("SpawnClouds", 1f, 1f);
    }

    private void SpawnClouds()
    {
        Debug.Log("SpawnClouds");
        if (Random.Range(1,100) > cloudChance)
        {
            Debug.Log("Not spawning.");
            return;
        }

        var amt = Random.Range(minClouds, maxClouds);
        for (var i = 0; i < amt; i++)
        {
            var gObj = this.GetPooledObject();
            if (gObj == null) return;

            gObj.transform.position = new Vector3(Random.Range(cloudMinSpawn.x, cloudMaxSpawn.x), Random.Range(cloudMinSpawn.y, cloudMaxSpawn.y), 0f);
            gObj.SetActive(true);
        }
    }
}