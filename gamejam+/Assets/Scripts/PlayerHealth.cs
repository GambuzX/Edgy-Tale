using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private float health;
    private float initialTime;
    private float time_since_lost_health;
    private HealthHandler healthHandler;

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
            Destroy(this.gameObject);
        }
        healthHandler.updateSlider();
        time_since_lost_health = Time.time;
        initialTime = Time.time;
    }
}
