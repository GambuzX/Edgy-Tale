using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Polygon : MonoBehaviour
{
    public static string GetName(int edges)
    {
        switch (edges)
        {
            case 0:
                return "Circle";
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
            default:
                return "Octagon";
        }
    }

    public static string GetEnemyName(int edges)
    {
        switch (edges)
        {
            case 0:
                return "CircleEnemy";
            case 3:
                return "TriangleEnemy";
            case 4:
                return "SquareEnemy";
            case 5:
                return "PentagonEnemy";
            case 6:
                return "HexagonEnemy";
            case 7:
                return "HeptagonEnemy";
            case 8:
                return "OctagonEnemy";
            default:
                return "CircleEnemy";
        }
    }
}
