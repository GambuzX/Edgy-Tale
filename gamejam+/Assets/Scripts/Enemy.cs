using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public AudioClip soundEffect;

    private AudioSource soundSource;

    public float speed = 1f;
    public float kill_points = 0.1f;

    private Transform player;

    private EdginessHandler edginessHandler;
    private HealthHandler healthHandler;

    // Start is called before the first frame update
    void Start()
    {
        soundSource = GetComponent<AudioSource>();
        soundSource.clip = soundEffect; 
        player = GameObject.FindObjectOfType<PlayerMovement>().transform;
        edginessHandler = GameObject.FindObjectOfType<EdginessHandler>();
        healthHandler = GameObject.FindObjectOfType<HealthHandler>();
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
            soundSource.Play();
            foreach(Transform child in transform)
            {
                if (child.GetComponent<SpriteRenderer>())
                    child.GetComponent<SpriteRenderer>().enabled = false;
            }
            this.GetComponent<PolygonCollider2D>().enabled = false;
            edginessHandler.addEdginess(kill_points);
            Invoke("destroySelf", 1);
        }
        
        if (collision.gameObject.CompareTag("Polygon"))
        {
            healthHandler.changeHealth(-10.0f);   
            Destroy(this.gameObject);
        }
    }

    private void destroySelf()
    {
        Destroy(this.gameObject);
    }
}
