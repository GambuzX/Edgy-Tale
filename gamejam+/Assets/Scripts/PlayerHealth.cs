using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private float health;
    private float initialTime;
    private float time_since_lost_health;
    private HealthHandler healthHandler;

    public Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        health = 100.0f;
        initialTime = Time.time;
        time_since_lost_health = Time.time;
        healthHandler = GameObject.FindObjectOfType<HealthHandler>();
        healthHandler.updateSlider();
    }

    // Update is called once per frame
    void Update()
    {
        time_since_lost_health += Time.deltaTime;
        if(time_since_lost_health - initialTime  > 10.0f) //10 seconds to gain health
        {
            this.IncreaseHealth(5.0f);
            initialTime = time_since_lost_health;
        }
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
            GameObject.FindObjectOfType<Spawner>().CancelInvoke();
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject gameObject in enemies)
            { 
                gameObject.GetComponent<Enemy>().enabled = false;
            }

            this.gameObject.GetComponent<PlayerMovement>().enabled = false;
            this.gameObject.GetComponentInChildren<PolyShooter>().enabled = false;

            foreach (Transform transform in canvas.transform)
            {
                transform.gameObject.SetActive(!transform.gameObject.activeSelf);
            }

            foreach (Enemy enemy in GameObject.FindObjectsOfType<Enemy>())
            {
                enemy.destroySelf();
            }

            foreach (EnemyBullet enemyBullet in GameObject.FindObjectsOfType<EnemyBullet>())
            {
                enemyBullet.selfDestruct();
            }
        }
        healthHandler.updateSlider();
        time_since_lost_health = Time.time;
        initialTime = Time.time;
    }
}
