using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    private static int numberOfPoint = 0;
    private static bool platformHasBeenTouched = false;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    public int GetScore()
    {
        return numberOfPoint;
    }

    public void SetScore(int value)
    {
        numberOfPoint = value;
    }

    public void IncrementScore()
    {
        if (!platformHasBeenTouched)
        {
            if (GetScore() < LevelManager.instance.GetTargetScore())
            {
                numberOfPoint++;
            }
            platformHasBeenTouched = true;
        }
       
    }
    public void AddToScore(int value)
    {
        if (numberOfPoint + value > LevelManager.instance.GetTargetScore())
        {
            numberOfPoint = LevelManager.instance.GetTargetScore();
        }
        else
        {
            numberOfPoint += value;
        }
    }

    public void SetPlatformHasBeenTouched(bool value)
    {
        platformHasBeenTouched = value;
    }

   
}
