using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EdginessHandler : MonoBehaviour
{
    public float shoot_edginess_cost = 0.005f;
    public int easter_egg_trigger = 10;

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

    private PlayerMovement playerMovement;

    private bool trueEndingRotate;
    private bool endingReached;

    //Power Ups
    private bool canLooseEdginess;
    private bool duplicatePoints;

    private PowerUpManager powerUpManager;

    // Start is called before the first frame update
    void Start()
    {
        soundSource = this.GetComponent<AudioSource>();
        bar = GameObject.Find("EdginessBar").GetComponent<Slider>();
        currentLevel = GameObject.Find("CurrentEdges").GetComponent<Text>();
        nextLevel = GameObject.Find("NextEdges").GetComponent<Text>();
        spriteHandler = GameObject.FindObjectOfType<SpriteHandler>();
        spawner = GameObject.FindObjectOfType<Spawner>();
        playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
        powerUpManager = GameObject.FindObjectOfType<PowerUpManager>();

        trueEndingRotate = false;
        endingReached = false;

        //Power Ups
        canLooseEdginess = true;

        duplicatePoints = false;

        edginess = 3f;
        egg_counter = 0;
        updateSlider();
    }

    private void Update()
    {
        if (trueEndingRotate)
        {
            playerMovement.rotatePlayer(1000f);
        }
    }

    public void StartEdginessShield()
    {
        canLooseEdginess = false;
        Invoke("DisableEdginessShield", 10f);
    }

    private void DisableEdginessShield()
    {
        canLooseEdginess = true;
        powerUpManager.disablePowerUp();
    }

    public void StartDuplicatePoints()
    {
        duplicatePoints = true;
        Invoke("DisableDuplicatePoints", 5f);
    }

    private void DisableDuplicatePoints()
    {
        duplicatePoints = false;
        powerUpManager.disablePowerUp();
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

        if (!canLooseEdginess && inc < 0)
            return;

        if(inc > 0f && duplicatePoints)
        {
            inc *= 2.0f;
        }

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

        if ((int) edginess > max_edges )
        {
            triggerGameOver();
        }
    }

    public void handleShoot(int multiplier = 1)
    {
        if (!endingReached && edginess-3f < 10e-6)
        {
            egg_counter += 1;

            if (egg_counter >= easter_egg_trigger)
            {
                TrueEnding();
            }
        }
        else
        {
            addEdginess(-shoot_edginess_cost * (int) edginess * multiplier);
        }
    }

    public int getEdges()
    {
        return (int)edginess;
    }

    private void triggerGameOver()
    {
        StopGame();

        bar.value = 0;
        currentLevel.text = ((int)max_edges).ToString();
        nextLevel.text = ((int)max_edges + 1).ToString();
        
        spawner.unleashGirlfriend(false);
    }

    private void StopGame()
    {
        PowerUp[] powerUps = GameObject.FindObjectsOfType<PowerUp>();
        foreach (PowerUp powerUp in powerUps)
        {
            Destroy(powerUp.gameObject);
        }

        GameObject.FindObjectOfType<PowerUpSpawner>().stopSpawning();

        shoot_cost_lock = true;
        spawner.stopSpawning();
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Destroy(obj);
        }
    }

    private void TrueEnding()
    {
        StopGame();
        egg_counter = 0;
        trueEndingRotate = true;
        endingReached = true;
        soundSource.clip = soundEffectGrow;
        soundSource.loop = true;
        soundSource.Play();
        Invoke("TransformIntoCircle", 5f);

    }

    private void TransformIntoCircle()
    {
        soundSource.Stop();
        soundSource.loop = false;
        trueEndingRotate = false;

        spriteHandler.changeSprite(0);

        spriteHandler.playerTrueEndingFace();

        bar.value = 1;
        currentLevel.text = "1";
        nextLevel.text = "1";

        spawner.unleashGirlfriend(true);
    }
}
