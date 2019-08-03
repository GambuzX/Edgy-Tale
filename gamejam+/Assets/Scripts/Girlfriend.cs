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

    private bool trueEnding = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerMovement>().transform;
        InvokeRepeating("increaseSpeed", 1f, 1f);

        movement_lock = false;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        if (distance <= stopDistance)
        {
            if (trueEnding)
                EndGame("GirlfriendMsgWin");
            else
                EndGame("GirlfriendMsgLose");
        }
        if (!movement_lock)
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }

    public void setEnding(bool ending)
    {
        trueEnding = ending;
    }

    private void EndGame(string UI_object)
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Minimap");
        foreach(GameObject gameObject in gameObjects)
        {
            gameObject.SetActive(false);
        }

        movement_lock = true;
        foreach(Transform obj in GameObject.Find(UI_object).transform)
        {
            obj.gameObject.SetActive(true);
        }
        player.GetComponent<PlayerMovement>().enabled = false;
        if (player.GetComponentInChildren<PolyShooter>())
            player.GetComponentInChildren<PolyShooter>().enabled = false;

        Invoke("ReturnToMenu", 10f);
    }

    private void ReturnToMenu()
    {
        GameObject.FindObjectOfType<LevelManager>().LoadScene("Menu");
    }
    
    private void increaseSpeed()
    {
        speed += 0.2f;
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
