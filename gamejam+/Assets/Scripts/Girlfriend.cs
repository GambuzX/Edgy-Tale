using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Girlfriend : MonoBehaviour
{
    public float speed = 5f;
    public float reflectSpeed = 5f;
    public float stopDistance = 1f;

    private bool movement_lock = false;
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerMovement>().transform;
        InvokeRepeating("increaseSpeed", 2f, 2f);

        movement_lock = false;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        if (distance <= stopDistance)
        {
            EndGame();
        }
        if (!movement_lock)
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }

    private void EndGame()
    {
        movement_lock = true;
        foreach(Transform obj in GameObject.Find("GirlfriendMsg").transform)
        {
            obj.gameObject.SetActive(true);
        }
        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponentInChildren<PolyShooter>().enabled = false;

        //Invoke game over screen
    }
    
    private void increaseSpeed()
    {
        speed += 0.1f;
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
