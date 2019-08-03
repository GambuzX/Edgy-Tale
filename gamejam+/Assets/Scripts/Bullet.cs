using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 1f, rotateSpeed = 5f;

    private bool dissipating = false;

    private Vector3 direction = Vector3.zero;

    void Start()
    {
        Invoke("dissipate", 4);
        Invoke("selfDestruct", 5);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);
        transform.position += direction * speed * Time.deltaTime;
        if (dissipating)
        {
            Color tmp = GetComponent<SpriteRenderer>().color;
            if(tmp.a < Time.deltaTime)
            {
                tmp.a = 0;
            }
            else
            {
                tmp.a -= Time.deltaTime;
            }
            GetComponent<SpriteRenderer>().color = tmp;
        }
    }

    public void dissipate()
    {
        dissipating = true;
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
