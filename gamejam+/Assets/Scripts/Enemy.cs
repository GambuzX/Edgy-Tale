using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public AudioClip soundEffect;

    private AudioSource soundSource;

    public float speed = 1f;

    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        soundSource = GetComponent<AudioSource>();
        soundSource.clip = soundEffect;
        player = GameObject.FindObjectOfType<PlayerMovement>().transform;   
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
            this.GetComponent<PolygonCollider2D>().enabled = false;
            Invoke("destroySelf", 1);
        }
    }

    private void destroySelf()
    {
        Destroy(this.gameObject);
    }
}
