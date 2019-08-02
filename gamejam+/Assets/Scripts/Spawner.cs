using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemy;

    public int decrementSpawnRateTime = 20;
    public int spawnRate = 5;
    public int minimumSpawnRate = 1;

    private Transform[] spawnPositions = new Transform[20];
    private int nPos = 0;

    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform child in transform)
        {
            spawnPositions[nPos] = child;
            nPos++;
        }

        if (spawnRate < minimumSpawnRate) spawnRate = minimumSpawnRate;
        InvokeRepeating("decrementSpawnRate", spawnRate, decrementSpawnRateTime);
        Invoke("spawnEnemy", 2);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void spawnEnemy()
    {
        int index = Random.Range(0, nPos);
        Instantiate(enemy, spawnPositions[index].position, Quaternion.identity);
        Invoke("spawnEnemy", spawnRate);
    }

    private void decrementSpawnRate()
    {
        if (spawnRate > minimumSpawnRate) spawnRate--;
    }
}
