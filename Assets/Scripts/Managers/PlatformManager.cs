using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    private static bool moving = false;

    public static void startMovingPlatforms()
    {
        moving = true;
        PlatformMoving[] platformMovings;
        platformMovings = FindObjectsOfType<PlatformMoving>();
        foreach(PlatformMoving platform in platformMovings)
        {
            platform.startMoving();
        }
    }

    public static void stopMovingPlatforms()
    {
        PlatformMoving[] platformMovings;
        platformMovings = FindObjectsOfType<PlatformMoving>();
        foreach (PlatformMoving platform in platformMovings)
        {
            platform.startMoving();
        }
    }

    public static bool getMovingState()
    {
        return moving;
    }

    public static void setMovingState(bool value)
    {
        moving = value;
    }
  
}
