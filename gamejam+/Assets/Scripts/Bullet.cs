using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 1f, rotateSpeed = 5f;

    private Vector3 direction = Vector3.zero;

    void Start()
    {
        Invoke("selfDestruct", 5);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);
        transform.position += direction * speed * Time.deltaTime;
    }

    public void setDirection(Vector3 direction)
    {
        this.direction = direction;
    }

    private void selfDestruct()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.gameObject.CompareTag("Polygon"))
            Destroy(gameObject);
    }
}
