using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private GameObject pauseScreen;
    private PlayerMovement playerMovement;
    private PolyShooter[] polyShooters;
    private bool hasPauseScreen;

    public void LoadScene(string name)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(name);
    }

    public void Exit()
    {
        Application.Quit();
    }

    private void Start()
    {
        hasPauseScreen = false;
        if (GameObject.FindGameObjectWithTag("PauseScreen"))
        {
            pauseScreen = GameObject.FindGameObjectWithTag("PauseScreen");
            pauseScreen.SetActive(false);
            playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
            polyShooters = GameObject.FindObjectsOfType<PolyShooter>();
            hasPauseScreen = true;
        }
    }

    private void Update()
    {
        if (hasPauseScreen && Input.GetKeyDown(KeyCode.Escape))
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
