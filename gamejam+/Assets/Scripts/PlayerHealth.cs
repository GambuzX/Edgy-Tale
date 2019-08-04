using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private float health;
    private float initialTime;
    private HealthHandler healthHandler;

    public Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        health = 100.0f;
        initialTime = Time.time;
        healthHandler = GameObject.FindObjectOfType<HealthHandler>();
        healthHandler.updateSlider();
    }

    public float GetHealth()
    {
        return health;
    }

    public void IncreaseHealth(float value)
    {
        health += value;
        if (health > 100.0f)
            health = 100.0f;
        healthHandler.updateSlider();
    }

    public void DecreaseHealth(float value)
    {
        health -= value;
        if(health <= .0f)
        {
            GameObject.FindObjectOfType<Spawner>().stopSpawning();
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject gameObject in enemies)
            {
                Destroy(gameObject);
            }

            this.gameObject.GetComponent<PlayerMovement>().enabled = false;
            this.gameObject.GetComponentInChildren<PolyShooter>().enabled = false;

            foreach (EnemyBullet enemyBullet in GameObject.FindObjectsOfType<EnemyBullet>())
            {
                enemyBullet.selfDestruct();
            }

            GameObject.FindObjectOfType<PowerUpSpawner>().CancelInvoke();
            foreach (PowerUp powerUp in GameObject.FindObjectsOfType<PowerUp>())
            {
                Destroy(powerUp.gameObject);
            }

            foreach (Transform transform in canvas.transform)
            {
                if (transform.name == "GameOverScreen")
                {
                    transform.gameObject.SetActive(true);
                    break;
                }
            }

            this.enabled = false;
        }
        healthHandler.updateSlider();
        initialTime = Time.time;
    }
}
