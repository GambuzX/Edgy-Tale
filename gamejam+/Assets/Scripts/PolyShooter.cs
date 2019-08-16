using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolyShooter : MonoBehaviour
{
    public AudioClip soundEffect;

    private AudioSource soundSource;

    public float shootDelay = 0.4f;
    public float chargeDelay = 1f;

    private List<Transform> childVertices = new List<Transform>();
    private GameObject bulletPrefab;

    private bool shootLock;

    private EdginessHandler edginessHandler;

    //Power Ups
    private bool biggerBullets;

    private PowerUpManager powerUpManager;

    private float attackStartTime;

    //Input
    private string fire_button_name;

    // Start is called before the first frame update
    void Start()
    {
        biggerBullets = false;
        soundSource = this.gameObject.AddComponent<AudioSource>();
        soundSource.clip = soundEffect;
        shootLock = false;
        foreach(Transform child in transform)
        {
            childVertices.Add(child);
        }
        bulletPrefab = Resources.Load<GameObject>("Bullet");
        edginessHandler = GameObject.FindObjectOfType<EdginessHandler>();
        powerUpManager = GameObject.FindObjectOfType<PowerUpManager>();
        fire_button_name = "Fire";
#if UNITY_WEBGL
        fire_button_name += "_WEBGL";
#endif
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(fire_button_name))
        {
            attackStartTime = Time.fixedTime;
        }

        if (Input.GetButtonUp(fire_button_name))
        {
            if (Time.fixedTime - attackStartTime > chargeDelay)
            {
                chargeShot();
            }
            else if (!shootLock)
            {
                shootLock = true;
                spawnBullets();
                Invoke("unlockShoot", shootDelay);
            }

        }
    }

    void spawnBullets()
    {
        soundSource.Play();
        foreach (Transform child in childVertices)
        {
            Vector3 bulletDir = (child.position - transform.position).normalized;
            GameObject bullet = Instantiate(bulletPrefab, child.position, Quaternion.identity);
            if (biggerBullets)
            {
                bullet.transform.localScale *= 2;
            }
            bullet.GetComponent<Bullet>().setDirection(bulletDir);
        }
        edginessHandler.handleShoot();
    }

    void chargeShot()
    {
        soundSource.Play();
        foreach (Transform child in childVertices)
        {
            Vector3 bulletDir = (child.position - transform.position).normalized;
            GameObject bullet = Instantiate(bulletPrefab, child.position, Quaternion.identity);

            Bullet bulletComp = bullet.GetComponent<Bullet>();
            bulletComp.setDirection(bulletDir);
            bulletComp.setPiercing(true);

            bullet.transform.localScale *= 3;
            if (biggerBullets)
            {
                bullet.transform.localScale *= 2;
            }
        }
        edginessHandler.handleShoot(2);
    }

    void unlockShoot()
    {
        this.shootLock = false;
    }

    public void StartBiggerBullets()
    {
        biggerBullets = true;
        shootDelay /= 2.0f;
        Invoke("DisableBiggerBullets", 5f);
    }

    public void DisableBiggerBullets()
    {
        biggerBullets = false;
        shootDelay *= 2.0f;
        powerUpManager.disablePowerUp();
    }
}
