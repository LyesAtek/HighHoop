using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMoving : MonoBehaviour
{
    private const float zPosition = -60f;
    public bool moving;

    public float speed = 12F;
    
    void Start()
    {
        moving = PlatformManager.getMovingState();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (moving)
        {
           
            if (transform.position.z <= zPosition)
            {
                LevelBuilder levelBuilder = GameObject.Find("LevelBuilder").GetComponent<LevelBuilder>();
                levelBuilder.generatePlatform();
                levelBuilder.cleanOutPlatforms();
            }
        }
        
    }

    public void startMoving()
    {
        moving = true;
       
    }
        public void stopMoving()
    {
        moving = false;
    }
}
