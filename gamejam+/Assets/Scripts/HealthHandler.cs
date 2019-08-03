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
    private bool shieldEnabled;

    private PowerUpManager powerUpManager;

    // Start is called before the first frame update
    void Start()
    {
        soundSource = GetComponent<AudioSource>();
        soundSource.clip = soundEffect;
        shieldEnabled = false;
        bar = GameObject.Find("HealthBar").GetComponent<Slider>();
        updateSlider();
        powerUpManager = GameObject.FindObjectOfType<PowerUpManager>();
    }

    public void updateSlider()
    {
        bar.value = player_health.GetHealth();
    }

    public void changeHealth(float value)
    {
        if (shieldEnabled && value < 0)
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
        shieldEnabled = true;
        Invoke("DisableShield", 5f);
    }

    private void DisableShield()
    {
        shieldEnabled = false;
        powerUpManager.disablePowerUp();
    }
}
