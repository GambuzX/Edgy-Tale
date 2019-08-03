﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int decrementSpawnRateTime = 20;
    public float spawnRate = 5f;
    public float minimumSpawnRate = 1f;

    private Transform[] spawnPositions = new Transform[20];
    private int nPos = 0;

    private EdginessHandler edginessHandler;
    private SpriteHandler spriteHandler;

    // Start is called before the first frame update
    void Start()
    {
        edginessHandler = GameObject.FindObjectOfType<EdginessHandler>();
        spriteHandler = GameObject.FindObjectOfType<SpriteHandler>();

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
        GameObject newEnemy = spriteHandler.GetNewEnemy(edginessHandler.getEdges());
        if (newEnemy != null)
        {
            int index = Random.Range(0, nPos);
            Instantiate(newEnemy, spawnPositions[index].position, Quaternion.identity);
        }
        Invoke("spawnEnemy", spawnRate);
    }

    private void decrementSpawnRate()
    {
        if (spawnRate > minimumSpawnRate) spawnRate -= 0.5f;
    }
}
