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
        Time.timeScale = 0f;
    } 

    public void ContinueGame()
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 1f;
    }
}
