using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EdginessHandler : MonoBehaviour
{
    public float shoot_edginess_cost = 0.005f;
    public int easter_egg_trigger = 10;

    public int max_edges = 8;

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

    private EdginessGrowth edgeGrowth;

    // Start is called before the first frame update
    void Start()
    {
        soundSource = this.GetComponent<AudioSource>();
        spriteHandler = GameObject.FindObjectOfType<SpriteHandler>();
        spawner = GameObject.FindObjectOfType<Spawner>();
        playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
        powerUpManager = GameObject.FindObjectOfType<PowerUpManager>();
        edgeGrowth = GameObject.FindObjectOfType<EdginessGrowth>();

        trueEndingRotate = false;
        endingReached = false;

        //Power Ups
        canLooseEdginess = true;

        duplicatePoints = false;

        edginess = 3f;
        egg_counter = 0;

        updateEdginessGrowth();
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
        updateEdginessGrowth();
        egg_counter = 0;

        if ((int) edginess > max_edges)
        {
            if (SceneManager.GetActiveScene().name == "Story Mode")
            {
                triggerGameOver();
            }
            /*else if(inc > 0)
            {
                bar.value = bar.maxValue;
                currentLevel.text = ((int)max_edges).ToString();
                nextLevel.text = ((int)max_edges + 1).ToString();
            }*/
        }
    }

    public void handleShoot(int multiplier = 1)
    {
        if (!endingReached && edginess-3f < 10e-6)
        {
            egg_counter += 1;

            if (egg_counter >= easter_egg_trigger && SceneManager.GetActiveScene().name == "Story Mode")
            {
                TrueEnding();
            }
        }
        else
        {
            addEdginess(-shoot_edginess_cost * (int) edginess * multiplier);
        }
    }

    private void updateEdginessGrowth()
    {
        edgeGrowth.updateScale(edginess - (int)edginess);
    }

    public int getEdges()
    {
        return (int)edginess;
    }

    private void triggerGameOver()
    {
        StopGame();        
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

        spawner.unleashGirlfriend(true);
    }
}
