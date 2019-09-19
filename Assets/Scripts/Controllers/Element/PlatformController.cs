using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    private GameController gameController;
	private bool hasTouch;
    // Start is called before the first frame update
    void Awake()
    {
        gameController = FindObjectOfType<GameController>();
    }

    private void OnCollisionEnter(Collision Col)
    {
       // print(gameController.getIfBallHasAlreadyBounce());
        if (Col.gameObject.tag == "Player")
        {
            
			if (!hasTouch)
			{
                
				gameController.incrementScore();
                hasTouch = true;
            }
		
		}
    }

}
