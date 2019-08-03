using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float speed = 1f;
    public float kill_points = 0.1f;

    private Transform player;

    private EdginessHandler edginessHandler;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerMovement>().transform;
        edginessHandler = GameObject.FindObjectOfType<EdginessHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Bullet>())
        {
            edginessHandler.addEdginess(kill_points);
            Destroy(this.gameObject);
        }
    }
}
