using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
	private bool hasTouch;
    private void OnCollisionEnter(Collision Col)
    {
       // print(GameController.instance.getIfBallHasAlreadyBounce());
        if (Col.gameObject.tag == "Player")
        {
            
			if (!hasTouch)
			{
                
				GameController.instance.incrementScore();
                hasTouch = true;
            }
		
		}
    }

}
