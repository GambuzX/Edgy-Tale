using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuFade : MonoBehaviour
{
    private Text text;
    private float initialTime;
    private float i;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        initialTime = Time.time;
        i = 1;
    }

    private void Update()
    {
        bool fadeAway = false;
        if ((int)(Time.time - initialTime) % 2 == 0)
            fadeAway = true;

        // fade from opaque to transparent
        if (fadeAway)
        {
            i -= Time.deltaTime / 2.0f;
            text.color = new Color(1, 1, 1, i);
        }
        // fade from transparent to opaque
        else
        {
            i += Time.deltaTime / 2.0f;
            text.color = new Color(1, 1, 1, i);
        }
    }

}
