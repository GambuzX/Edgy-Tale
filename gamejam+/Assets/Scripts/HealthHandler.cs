using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthHandler : MonoBehaviour
{
    public AudioClip soundEffect;

    private AudioSource soundSource;

    public float health_cost = 0.01f;

    private Slider bar;
    public PlayerHealth player_health;

    //Power Ups
    private bool canLooseHealth;
    private float startShieldTime;

    // Start is called before the first frame update
    void Start()
    {
        soundSource = GetComponent<AudioSource>();
        soundSource.clip = soundEffect;
        canLooseHealth = true;
        bar = GameObject.Find("HealthBar").GetComponent<Slider>();
        updateSlider();
    }

    private void Update()
    {
        if (!canLooseHealth && Time.time - startShieldTime > 10.0f)
        {
            canLooseHealth = true;
        }
    }

    public void updateSlider()
    {
        bar.value = player_health.GetHealth();
    }

    public void changeHealth(float value)
    {
        if (!canLooseHealth && value < 0)
            return;

        if(value < 0)
        {
            player_health.DecreaseHealth(-value);
            soundSource.Play();
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

    public void StartShield()
    {
        canLooseHealth = false;
        startShieldTime = Time.time;
    }
}
