using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolyShooter : MonoBehaviour
{

    public GameObject bulletPrefab;

    private List<Transform> childVertices = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform child in transform)
        {
            childVertices.Add(child);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            spawnBullets();
        }
    }

    void spawnBullets()
    {
        foreach (Transform child in childVertices)
        {
            Vector3 bulletDir = (child.position - transform.position).normalized;
            GameObject bullet = Instantiate(bulletPrefab, child.position, Quaternion.identity, transform);
            bullet.GetComponent<Bullet>().setDirection(bulletDir);

        }
    }
}
