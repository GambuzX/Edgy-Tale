using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolyShooter : MonoBehaviour
{

    public GameObject bulletPrefab;
    public float shootDelay = 0.4f;

    private List<Transform> childVertices = new List<Transform>();

    private bool shootLock;

    // Start is called before the first frame update
    void Start()
    {
        shootLock = false;
        foreach(Transform child in transform)
        {
            childVertices.Add(child);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (!shootLock && Input.GetButtonDown("Fire1"))
        {
            shootLock = true;
            spawnBullets();
            Invoke("unlockShoot", shootDelay);
        }
    }

    void spawnBullets()
    {
        foreach (Transform child in childVertices)
        {
            Vector3 bulletDir = (child.position - transform.position).normalized;
            GameObject bullet = Instantiate(bulletPrefab, child.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().setDirection(bulletDir);

        }
    }

    void unlockShoot()
    {
        this.shootLock = false;
    }
}
