using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeptagonBehaviour : Enemy
{
    public AudioClip soundEffectShot;

    private AudioSource soundSourceShot;

    private GameObject bulletPrefab;

    private List<Transform> childVertices = new List<Transform>();

    bool shotEnabled = false;

    protected override void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.CompareTag("Vertex"))
                childVertices.Add(child);
        }
        bulletPrefab = Resources.Load<GameObject>("EnemyBullet");
        soundSourceShot = GetComponent<AudioSource>();
        soundSourceShot.clip = soundEffectShot;
        Invoke("enableShot", 1);
        base.Start();
    }

    protected override void Update()
    {
        if (shotEnabled)
        {
            Vector3 playerDir = player.position - transform.position;
            transform.position += playerDir * 1.75f;
            shotEnabled = false;
            spawnBullets();
            Invoke("enableShot", 3);
        }
        base.Update();
    }
    void spawnBullets()
    {
        soundSource.Play();
        foreach (Transform child in childVertices)
        {
            Vector3 bulletDir = (child.position - transform.position).normalized;
            GameObject bullet = Instantiate(bulletPrefab, child.position, Quaternion.identity);
            bullet.transform.localScale *= 2;
            bullet.GetComponent<EnemyBullet>().setDirection(bulletDir);
        }
    }

    private void enableShot()
    {
        shotEnabled = true;
    }
}
