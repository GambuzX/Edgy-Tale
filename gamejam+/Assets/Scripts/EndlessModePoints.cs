using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndlessModePoints : MonoBehaviour
{
    public static Text text;
    private static ulong score = 0;

    public static void UpdateScore(uint new_score = 0)
    {
        score += new_score;
        UpdateUIScore();
    }

    private static void UpdateUIScore()
    {
        text.text = score.ToString();
    }
}
