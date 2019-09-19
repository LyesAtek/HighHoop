using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    private static int targetScore = 100;
    private static int levelNumber = 1;

    public static int GetTargetScore()
    {
        return targetScore;
    }

    public static int GetLevelNumber()
    {
        return levelNumber;
    }

    public static void ResetParameters()
    {
        levelNumber = 1;
        targetScore = 100;
    }

    public static void NextLevel()
    {
        levelNumber += 1;
        targetScore += 5 ;
       // print("Level : " + levelNumber + " Target : " + targetScore);
    }

   

}
