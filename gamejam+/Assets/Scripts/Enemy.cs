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

    // Start is called before the first frame update
    void Start()
    {
        soundSource = GetComponent<AudioSource>();
        soundSource.clip = soundEffect; 
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
            soundSource.Play();
            this.enabled = false;
            this.GetComponent<SpriteRenderer>().enabled = false;
            edginessHandler.addEdginess(kill_points);
            Invoke("destroySelf", 1);
        }
    }

    private void destroySelf()
    {
        Destroy(this.gameObject);
    }
}
