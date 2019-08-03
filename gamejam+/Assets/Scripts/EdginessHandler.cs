using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EdginessHandler : MonoBehaviour
{
    public float shoot_edginess_cost = 0.005f;
    public int easter_egg_trigger = 50;

    public int max_edges = 8;

    private Slider bar;
    private Text currentLevel, nextLevel;

    private SpriteHandler spriteHandler;

    private float edginess;
    private int egg_counter;
    private bool shoot_cost_lock = false;

    public AudioClip soundEffectGrow, soundEffectUngrow;

    private AudioSource soundSource;
    private Spawner spawner;


    // Start is called before the first frame update
    void Start()
    {
        soundSource = this.GetComponent<AudioSource>();
        bar = GameObject.Find("EdginessBar").GetComponent<Slider>();
        currentLevel = GameObject.Find("CurrentEdges").GetComponent<Text>();
        nextLevel = GameObject.Find("NextEdges").GetComponent<Text>();
        spriteHandler = GameObject.FindObjectOfType<SpriteHandler>();
        spawner = GameObject.FindObjectOfType<Spawner>();
        edginess = 3f;
        egg_counter = 0;
        updateSlider();
    }

    public void updateSlider()
    {
        bar.value = edginess - (int) edginess;
        currentLevel.text = ((int)edginess).ToString();
        nextLevel.text = ((int)edginess + 1).ToString();
    }

    public void addEdginess(float inc)
    {
        if (shoot_cost_lock) return;

        int previous = (int)edginess;

        edginess += inc;

        if ((int)edginess != previous && (previous > 3 || edginess > 3))
        {
            if((int)edginess < previous)
            {
                soundSource.clip = soundEffectUngrow;
            }
            else
            {
                soundSource.clip = soundEffectGrow;
            }
            soundSource.Play();

            FindObjectOfType<PlayerMovement>().toggleEdgyTransformation();

            FindObjectOfType<PlayerMovement>().Invoke("toggleEdgyTransformation", 0.5f);

            spriteHandler.changeSprite((int)edginess);
        }

        if (edginess < 3f) edginess = 3f;
        updateSlider();
        egg_counter = 0;

        if ((int) edginess >= max_edges )
        {
            triggerGameOver();
        }
    }

    public void handleShoot()
    {
        if (edginess-3f < 10e-6)
        {
            egg_counter += 1;

            if (egg_counter >= easter_egg_trigger)
            {
                Debug.Log("Won game");
            }
        }
        else
        {
            addEdginess(-shoot_edginess_cost * (int) edginess);
        }
    }

    public int getEdges()
    {
        return (int)edginess;
    }

    private void triggerGameOver()
    {
        spawner.stopSpawning();

        bar.value = 0;
        currentLevel.text = ((int)max_edges).ToString();
        nextLevel.text = "∞";

        shoot_cost_lock = true;

        foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Destroy(obj);
        }

        spawner.unleashGirlfriend();
    }
}
