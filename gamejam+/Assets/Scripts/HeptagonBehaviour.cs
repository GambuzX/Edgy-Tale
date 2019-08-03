using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeptagonBehaviour : Enemy
{
    public AudioClip soundEffectShot;

    private AudioSource soundSourceShot;

    private GameObject bulletPrefab;

    private List<Transform> childVertices = new List<Transform>();

    bool shotLock = true;

    protected override void Start()
    {
        foreach (Transform child in transform)
        {
            childVertices.Add(child);
        }
        bulletPrefab = Resources.Load<GameObject>("EnemyBullet");
        soundSourceShot = GetComponent<AudioSource>();
        soundSourceShot.clip = soundEffectShot;
        Invoke("toggleShotLock", 1);
        base.Start();
    }

    protected override void Update()
    {
        if (shotLock)
        {
            this.transform.position = player.position + Vector3.one * 5 * Random.Range(-1f, 1f);
            toggleShotLock();
            spawnBullets();
            Invoke("toggleShotLock", 3);
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
            bullet.transform.parent = this.transform;
            bullet.GetComponent<EnemyBullet>().setDirection(bulletDir);
        }
    }

    private void toggleShotLock()
    {
        shotLock = !shotLock;
    }
}
