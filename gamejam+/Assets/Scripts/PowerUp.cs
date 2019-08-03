using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUpType
{
    Health,
    Super_Edginess,
    Bullet,
    Points,
    Shield
}

public class PowerUp : MonoBehaviour
{
    public AudioClip soundEffect;

    private AudioSource soundSource;

    public static float health;
    public static PowerUpType GetPowerUp()
    {
        int random = Random.Range(0, 5);

        return (PowerUpType)random;
    }

    public PowerUpType type;
    private HealthHandler healthHandler;
    private EdginessHandler edginessHandler;
    private PolyShooter polyShooter;

    void Start()
    {
        soundSource = GetComponent<AudioSource>();
        soundSource.clip = soundEffect;
        switch (type)
        {
            case PowerUpType.Health:
            case PowerUpType.Shield:
                healthHandler = GameObject.FindObjectOfType<HealthHandler>();
                break;
            case PowerUpType.Super_Edginess:
            case PowerUpType.Points:
                edginessHandler = GameObject.FindObjectOfType<EdginessHandler>();
                break;
            case PowerUpType.Bullet:
                polyShooter = GameObject.FindObjectOfType<PolyShooter>();
                break;
            default:
                Destroy(this);
                break;
        }
        Invoke("DestroySelf", 10f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Polygon"))
        {
            soundSource.Play();
            switch (type)
            {
                case PowerUpType.Health:
                    healthHandler.changeHealth(health);
                    break;
                case PowerUpType.Super_Edginess:
                    edginessHandler.StartEdginessShield();
                    break;
                case PowerUpType.Bullet:
                    polyShooter.StartBiggerBullets();
                    break;
                case PowerUpType.Points:
                    edginessHandler.StartDuplicatePoints();
                    break;
                case PowerUpType.Shield:
                    healthHandler.StartShield();
                    break;
                default:
                    break;
            }
            this.GetComponent<SpriteRenderer>().enabled = false;
            this.GetComponent<Collider2D>().enabled = false;
            Invoke("DestroySelf", 1);
        }
    }

    private void DestroySelf()
    {
        Destroy(this.gameObject);
    }
}
