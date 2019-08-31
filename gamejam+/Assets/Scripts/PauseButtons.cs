using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButtons : MonoBehaviour
{

    private Button[] buttons = new Button[3];

    private int selected;

    private bool buttonlock;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.name == "Continue") buttons[0] = child.GetComponent<Button>();
            if (child.name == "PlayAgain") buttons[1] = child.GetComponent<Button>();
            if (child.name == "ExitToMenu") buttons[2] = child.GetComponent<Button>();
        }
        selected = 0;
        buttonlock = false;

        buttons[selected].Select();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Submit"))
        {
            buttons[selected].onClick.Invoke();
        }

        if (buttonlock) return;

        float vertical = Input.GetAxis("Vertical");
        if (vertical != 0)
        {
            if (vertical > 0)
            {
                selected = 0;
            }
            else
            {
                selected = 1;
            }

            buttons[selected].Select();
            buttonlock = true;
            Invoke("unlockButton", 0.5f);
        }
        
    }

    private void unlockButton()
    {
        buttonlock = false;
    }
}
