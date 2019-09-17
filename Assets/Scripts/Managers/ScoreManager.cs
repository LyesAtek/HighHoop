using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private static int numberOfPoint = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

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
}
