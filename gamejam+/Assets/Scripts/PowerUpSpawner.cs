using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public int decrementSpawnRateTime = 30;
    public float spawnRate = 60f;
    public float minimumSpawnRate = 1f;

    private Transform[] spawnPositions = new Transform[5];
    private int nPos = 0;


    // Start is called before the first frame update
    void Start()
    {
        PowerUp.health = 10.0f;

        foreach (Transform child in transform)
        {
            spawnPositions[nPos] = child;
            nPos++;
        }

        if (spawnRate < minimumSpawnRate) spawnRate = minimumSpawnRate;
        InvokeRepeating("decrementSpawnRate", spawnRate, decrementSpawnRateTime);
        Invoke("spawnPowerUp", 2);
    }


    private void spawnPowerUp()
    {
        PowerUpType newPowerUp = PowerUp.GetPowerUp();
        string powerUpName = "";
        switch (newPowerUp)
        {
            case PowerUpType.Health:
                powerUpName = "pill";
                break;
            case PowerUpType.Super_Edginess:
                powerUpName = "blue_pill";
                break;
            case PowerUpType.Bullet:
                powerUpName = "ammo";
                break;
            case PowerUpType.Points:
                powerUpName = "dollar_sign";
                break;
            case PowerUpType.Shield:
                powerUpName = "shield";
                break;
            default:
                break;
        }
        int index = Random.Range(0, nPos);
        GameObject gameObject = Instantiate(Resources.Load<GameObject>(powerUpName), spawnPositions[index].position, Quaternion.identity);
        
        Invoke("spawnPowerUp", spawnRate);
    }

    private void decrementSpawnRate()
    {
        if (spawnRate > minimumSpawnRate) spawnRate -= 0.5f;
    }

    public void stopSpawning()
    {
        CancelInvoke();
    }
}
