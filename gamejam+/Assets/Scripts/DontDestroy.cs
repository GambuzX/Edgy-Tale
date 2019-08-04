using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{
    private string actual_scene;

    private void Awake()
    {
        actual_scene = SceneManager.GetActiveScene().name;
        if (actual_scene != "Game" && actual_scene != "Transition")
        {
            GameObject[] objs = GameObject.FindGameObjectsWithTag("Music");
            if (objs.Length > 1)
                Destroy(this.gameObject);
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        actual_scene = SceneManager.GetActiveScene().name;
        if (actual_scene == "Game" || actual_scene == "Transition")
        {
            Destroy(this.gameObject);
        }
    }
}
