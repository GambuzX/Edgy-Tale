using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthHandler : MonoBehaviour
{
    public float health_cost = 0.01f;

    private Slider bar;

    public PlayerHealth player_health;

    // Start is called before the first frame update
    void Start()
    {
        bar = GameObject.Find("HealthBar").GetComponent<Slider>();
        updateSlider();
    }

    public void updateSlider()
    {
        bar.value = player_health.GetHealth();
    }

    public void changeHealth(float value)
    {
        if(value < 0)
        {
            player_health.DecreaseHealth(-value);
        }
        else if(value > 0)
        {
            player_health.IncreaseHealth(value);
        }

        if(player_health == null)
        {
            Destroy(this.gameObject);
        }
    }
}
