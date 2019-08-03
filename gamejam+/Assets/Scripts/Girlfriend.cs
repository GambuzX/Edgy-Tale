using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Girlfriend : MonoBehaviour
{
    public float speed = 5f;
    public float reflectSpeed = 5f;
    public float stopDistance = 1f;

    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerMovement>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        if (distance <= stopDistance)
        {
            EndGame();
        }
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }

    private void EndGame()
    {
        Debug.Log("Game over");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet bullet = collision.GetComponent<Bullet>();
        if (bullet)
        {
            Vector3 dir = (transform.position - collision.transform.position).normalized;
            bullet.setDirection(-dir);
            bullet.setSpeed(reflectSpeed);
            
        }
    }
}
