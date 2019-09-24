using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    private GameObject pauseScreen;
    private PlayerMovement playerMovement;
    private PolyShooter[] polyShooters;
    private bool hasPauseScreen;
    private string activedButton;

    private Button[] buttons;

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
        activedButton = "Story Mode";

        if(SceneManager.GetActiveScene().name == "Menu")
            buttons = GameObject.FindGameObjectWithTag("ModeButtons").transform.GetComponentsInChildren<Button>(true);

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

    private string getNextButtonName(uint index)
    {
        switch (index)
        {
            case 0:
                return "Story Mode";
            case 1:
                return "Endless Mode";
            default:
                return null;
        }
    }

    private uint getButtonIndex(string name)
    {
        switch (name)
        {
            case "Story Mode":
                return 0;
            case "Endless Mode":
                return 1;
            default:
                return 2;
        }
    }

    public void ChangeButton(bool direction) //If true, direction = right; if false, direction = left;
    {
        foreach (Button button in buttons)
        {
            button.gameObject.SetActive(false);
        }

        uint button_index = getButtonIndex(activedButton);

        uint next_button_index;
        if (direction)
        {
            next_button_index = (button_index + 1) % (uint)buttons.Length;
        }
        else
        {
            next_button_index = (button_index - 1) % (uint)buttons.Length;
        }

        activedButton = getNextButtonName(next_button_index);
        buttons[next_button_index].gameObject.SetActive(true);
    }
}
