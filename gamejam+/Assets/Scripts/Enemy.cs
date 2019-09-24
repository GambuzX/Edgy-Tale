using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public static bool first_enemy = true;

    public AudioClip soundEffect;

    protected AudioSource soundSource;

    public float speed = 1f;
    public float kill_points = 0.1f;
    public float damage = 10f;
    public float health = 1f;

    protected Transform player;

    protected float damagedTime = 1f;

    protected EdginessHandler edginessHandler;
    protected HealthHandler healthHandler;

    //Endless Mode
    private EndlessModePoints points;
    private string sceneName;

    // Start is called before the first frame update
    protected virtual void Start()
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
        if (damagedTime <= 0.3f)
        {
            transform.position += (transform.position - player.position) * Time.deltaTime * 0.4f;
            damagedTime += Time.deltaTime;
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }

        if (health <= 0f)
        {
            transform.Rotate(Vector3.forward, 1000 * Time.deltaTime);
        }
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Bullet>())
        {
            health -= 1f;
            soundSource.Play();
            damagedTime = 0f;
            if (health <= 0)
            {
                this.GetComponent<PolygonCollider2D>().enabled = false;
                edginessHandler.addEdginess(kill_points);
                Invoke("destroySelf", 1);
            }
        }
        
        if (collision.gameObject.CompareTag("Polygon"))
        {
            healthHandler.changeHealth(-damage);
            health = 0f;
            damagedTime = 0f;
            this.GetComponent<PolygonCollider2D>().enabled = false;
            Invoke("destroySelf", 1);
        }
    }

    public void destroySelf()
    {
        if ((sceneName = SceneManager.GetActiveScene().name) == "Endless Mode")
        {
            if (first_enemy) {
                EndlessModePoints.text = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();
                first_enemy = false;
            }
            EndlessModePoints.UpdateScore((uint)(kill_points*50));
        }
        Destroy(this.gameObject);
    }
}
