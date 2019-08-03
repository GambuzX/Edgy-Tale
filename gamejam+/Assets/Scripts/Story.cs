using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Story : MonoBehaviour
{
    public Sprite breakup;
    public Sprite angry;

    public Image actual_image;
    public Image fade_image;

    private int num_image;

    // Start is called before the first frame update
    void Start()
    {
        num_image = 1;
        Invoke("Fade_Anim", 5f);
        Invoke("ChangeImage", 6f);
        Invoke("Fade_Comeback", 7f);
        Invoke("Fade_Anim", 14f);
        Invoke("ChangeImage", 15f);
        Invoke("Fade_Comeback", 16f);
        Invoke("Fade_Anim", 23f);
        Invoke("LoadGame", 25f);
    }

    private void Fade_Anim()
    {
        StartCoroutine(FadeImage(false));
    }

    private void Fade_Comeback()
    {
        StartCoroutine(FadeImage(true));
    }

    private void ChangeImage()
    {
        if(num_image == 1)
        {
            actual_image.sprite = breakup;
            num_image++;
        }
        else if(num_image == 2)
        {
            actual_image.sprite = angry;
            num_image++;
        }
    }


    private void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }

    IEnumerator FadeImage(bool fadeAway)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                // set color with i as alpha
                fade_image.color = new Color(0, 0, 0, i);
                yield return null;
            }
        }
        // fade from transparent to opaque
        else
        {
            // loop over 1 second
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                // set color with i as alpha
                fade_image.color = new Color(0, 0, 0, i);
                yield return null;
            }
        }
    }

}
