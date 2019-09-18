using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private static int numberOfPoint = 0;
    
    
    public static int GetPoint()
    {
        return numberOfPoint;
    }

    public static void SetPoint(int value)
    {
        numberOfPoint = value;
    }

    public static void IncrementPoint()
    {
        numberOfPoint++;
    }
    public static void AddToScore(int value)
    {
        numberOfPoint += value;
    }
}
