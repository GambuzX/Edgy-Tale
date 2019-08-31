using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private GameObject pauseScreen;
    private PlayerMovement playerMovement;
    private PolyShooter[] polyShooters;
    private string sceneName;

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void Exit()
    {
        Application.Quit();
    }

    private void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "Game")
        {
            pauseScreen = GameObject.FindGameObjectWithTag("PauseScreen");
            pauseScreen.SetActive(false);
            playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
            polyShooters = GameObject.FindObjectsOfType<PolyShooter>();
        }
    }

    private void Update()
    {
        if (sceneName == "Game" && Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    private void PauseGame()
    {
        pauseScreen.SetActive(true);

        GameObject.FindObjectOfType<Spawner>().stopSpawning();

        playerMovement.enabled = false;
        foreach(PolyShooter polyShooter in polyShooters)
        {
            polyShooter.enabled = false;
        }

        foreach (Enemy enemy in GameObject.FindObjectsOfType<Enemy>())
        {
            enemy.enabled = false;
        }

        foreach (EnemyBullet enemyBullet in GameObject.FindObjectsOfType<EnemyBullet>())
        {
            enemyBullet.enabled = false;
        }

        GameObject.FindObjectOfType<PowerUpSpawner>().stopSpawning();
    } 

    public void ContinueGame()
    {
        foreach (Transform child in this.transform)
        {
            if (child.gameObject.tag == "GameOverScreen" || child.gameObject.tag == "PauseScreen")
            {
                child.gameObject.SetActive(false);
            }
            else
            {
                child.gameObject.SetActive(true);
            }
        }

        GameObject.FindObjectOfType<Spawner>().restartSpawning();

        playerMovement.enabled = true;
        foreach (PolyShooter polyShooter in polyShooters)
        {
            polyShooter.enabled = true;
        }

        foreach (Enemy enemy in GameObject.FindObjectsOfType<Enemy>())
        {
            enemy.enabled = true;
        }

        foreach (EnemyBullet enemyBullet in GameObject.FindObjectsOfType<EnemyBullet>())
        {
            enemyBullet.enabled = true;
        }

        GameObject.FindObjectOfType<PowerUpSpawner>().restartSpawning();
    }
}
