using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hexagon : Enemy
{

    public float rotateSpeed = 100f;

    private int rotationDir = 1;

    protected override void Start()
    {
        base.Start();
        rotationDir = 1;
    }

    // Update is called once per frame
    protected override void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

        if (Random.Range(0, 10) > 8) rotationDir *= -1;
        float theta = rotateSpeed * Time.deltaTime * rotationDir;
        transform.RotateAround(player.position, Vector3.forward, theta);
        transform.RotateAround(transform.position, Vector3.forward, -theta);

        if (wasHit)
        {
            transform.Rotate(Vector3.forward, 1000 * Time.deltaTime);
        }
    }
}
