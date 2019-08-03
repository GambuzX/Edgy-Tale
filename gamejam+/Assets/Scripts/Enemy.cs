using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public AudioClip soundEffect;

    protected AudioSource soundSource;

    public float speed = 1f;
    public float kill_points = 0.1f;
    public float damage = 10f;
    public float health = 1f;

    protected Transform player;

    protected bool wasHit = false;

    protected EdginessHandler edginessHandler;
    protected HealthHandler healthHandler;

    // Start is called before the first frame update
   protected void Start()
    {
        soundSource = GetComponent<AudioSource>();
        soundSource.clip = soundEffect; 
        player = GameObject.FindObjectOfType<PlayerMovement>().transform;
        edginessHandler = GameObject.FindObjectOfType<EdginessHandler>();
        healthHandler = GameObject.FindObjectOfType<HealthHandler>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

        if (wasHit)
        {
            transform.Rotate(Vector3.forward, 1000 * Time.deltaTime);
        }
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Bullet>())
        {
            health -= 1f;
            if (health <= 0 && !wasHit)
            {
                soundSource.Play();
                wasHit = true;
                this.GetComponent<PolygonCollider2D>().enabled = false;
                edginessHandler.addEdginess(kill_points);
                Invoke("destroySelf", 1);
            }
        }
        
        if (collision.gameObject.CompareTag("Polygon"))
        {
            healthHandler.changeHealth(-damage);
            wasHit = true;
            this.GetComponent<PolygonCollider2D>().enabled = false;
            Invoke("destroySelf", 1);
        }
    }

    protected void destroySelf()
    {
        Destroy(this.gameObject);
    }
}
