using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
    private static bool isAccelerometerMode = false;

    public static bool GetISAccelerometerMode()
    {
        return isAccelerometerMode;
    }

    public static void SetISAccelerometerMode(bool value)
    {
         isAccelerometerMode = value;
    }



}
