using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance ;
    private  int targetScore = 100;
    private  int levelNumber = 1;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public int GetTargetScore()
    {
        return targetScore;
    }

    public int GetLevelNumber()
    {
        return levelNumber;
    }

    public void ResetParameters()
    {
        levelNumber = 1;
        targetScore = 100;
    }

    public void NextLevel()
    {
        levelNumber += 1;
        targetScore += 5 ;
       // print("Level : " + levelNumber + " Target : " + targetScore);
    }

   

}
