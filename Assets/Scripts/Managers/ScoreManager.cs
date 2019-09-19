using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private static int numberOfPoint = 0;
    private static bool platformHasBeenTouched = false;
    
    public static int GetScore()
    {
        return numberOfPoint;
    }

    public static void SetScore(int value)
    {
        numberOfPoint = value;
    }

    public static void IncrementScore()
    {
        if (!platformHasBeenTouched)
        {
            if (ScoreManager.GetScore() < LevelManager.GetTargetScore())
            {
                numberOfPoint++;
            }
            platformHasBeenTouched = true;
        }
       
    }
    public static void AddToScore(int value)
    {
        if (numberOfPoint + value > LevelManager.GetTargetScore())
        {
            numberOfPoint = LevelManager.GetTargetScore();
        }
        else
        {
            numberOfPoint += value;
        }
    }

    public static void SetPlatformHasBeenTouched(bool value)
    {
        platformHasBeenTouched = value;
    }

   
}
