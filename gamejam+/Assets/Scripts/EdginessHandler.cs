using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EdginessHandler : MonoBehaviour
{
    public float shoot_edginess_cost = 0.005f;
    public int easter_egg_trigger = 50;

    private Slider bar;
    private Text currentLevel, nextLevel;

    private SpriteHandler spriteHandler;

    private float edginess;
    private int egg_counter;

    public AudioClip soundEffectGrow, soundEffectUngrow;

    private AudioSource soundSource;


    // Start is called before the first frame update
    void Start()
    {
        soundSource = this.GetComponent<AudioSource>();
        bar = GameObject.Find("EdginessBar").GetComponent<Slider>();
        currentLevel = GameObject.Find("CurrentEdges").GetComponent<Text>();
        nextLevel = GameObject.Find("NextEdges").GetComponent<Text>();
        spriteHandler = GameObject.FindObjectOfType<SpriteHandler>();
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
        int previous = (int)edginess;

        edginess += inc;

        if ((int)edginess != previous)
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
            
            spriteHandler.changeSprite((int)edginess);
        }

        if (edginess < 3f) edginess = 3f;
        updateSlider();
        egg_counter = 0;
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
}
