using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hexagon : Enemy
{

    public float rotateSpeed;

    // Update is called once per frame
    protected override void Update()
    {
        int move = Random.Range(0, 2);
        int rotate = Random.Range(0, 2);

        if (move != 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }

        if (rotate != 0)
        {
            transform.RotateAround(player.position, Vector3.up, rotateSpeed * Time.deltaTime);
        }


        if (wasHit)
        {
            transform.Rotate(Vector3.forward, 1000 * Time.deltaTime);
        }
    }
}
