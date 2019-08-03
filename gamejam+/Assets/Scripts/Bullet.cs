using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 1f;

    private bool moving = false;
    private Vector3 direction = Vector3.zero;

    void Start()
    {
        Invoke("selfDestruct", 5);
    }

    // Update is called once per frame
    void Update()
    {
        if(moving)
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }

    public void setDirection(Vector3 direction)
    {
        this.direction = direction;
        moving = true;
    }

    private void selfDestruct()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
