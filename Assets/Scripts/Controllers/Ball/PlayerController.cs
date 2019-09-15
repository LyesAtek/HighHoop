using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    private Animator ballAnimator;
	private Animator jumpPlatformAnimator;
	public float speed;
    // Start is called before the first frame update
    void Start()
    {
        ballAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(ballAnimator.enabled == true) {
            checkIfIsFalling();
        }
      
    }

    void OnCollisionEnter(Collision Col)
    {
        if (Col.gameObject.tag == "Ground")
        {
            ballAnimator.enabled = true;
			ballAnimator.SetBool("isSuperJump", false);
            ballAnimator.Play("BallBouncing", -1, 0f);
        }
		else if (Col.gameObject.tag == "Jump")
		{
			jumpPlatformAnimator = Col.gameObject.GetComponent<Animator>();
			jumpPlatformAnimator.SetBool("isJump", true);
			ballAnimator.SetBool("isSuperJump", true);
            ballAnimator.Play("BallSuperBouncing", -1, 0f);
        }



	}

    void checkIfIsFalling()
    {
        if(transform.position.y < 0)
        {
            ballAnimator.enabled = false;
        }
    }
   
}