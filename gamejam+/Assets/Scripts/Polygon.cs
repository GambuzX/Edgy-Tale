using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Polygon : MonoBehaviour
{
    public static string GetName(int edges)
    {
        switch (edges)
        {
            case 3:
                return "Triangle";
            case 4:
                return "Square";
            case 5:
                return "Pentagon";
            case 6:
                return "Hexagon";
            case 7:
                return "Heptagon";
            case 8:
                return "Octagon";
            default:
                return "Circle";
        }
    }
}
