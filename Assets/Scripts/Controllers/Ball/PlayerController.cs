using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float rayDistance;
    [SerializeField]
    private LayerMask layers;

    private Animator ballAnimator;
    private Animator jumpPlatformAnimator;
    private GameObject parent;
    // Start is called before the first frame update
    void Start()
    {
        ballAnimator = GetComponent<Animator>();
        parent = transform.parent.gameObject;
     
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.GetBallState() == GameManager.State.InGame &&transform.position.y <= 0 && GetComponent<Rigidbody>().velocity.y < 0)
        {
            RaycastHit hit;
            if (!Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, rayDistance, layers))
            {
                ballAnimator.enabled = false;
            }
        }
       
        if (Input.GetKeyDown(KeyCode.Space))
        {
            play();
        }
        
    }

    private void OnCollisionEnter(Collision Col)
    {
        if (Col.gameObject.tag == "Ground" && ballAnimator.GetBool("inGame"))
        {
           
           
            ballAnimator.SetBool("isSuperJump", false);
        }
        else if (Col.gameObject.tag == "Jump")
        {
            jumpPlatformAnimator = Col.gameObject.GetComponent<Animator>();
            jumpPlatformAnimator.SetBool("isJump", true);
            ballAnimator.SetBool("isSuperJump", true);
           
        }
        if (Col.gameObject.tag == "Finish")
        {
            GameManager.SetBallState(GameManager.State.Dead);
            reset();
        }


    }

    void checkIfIsFalling()
    {
        if (transform.position.y < -0.1)
        {
            ballAnimator.enabled = false;
        }
    }

    void reset()
    {
        ballAnimator.enabled = true;
        ballAnimator.SetBool("inGame", false);
        parent.transform.position = Vector3.zero;
        transform.position = Vector3.zero;
        

    }

    void play()
    {   
        GameManager.SetBallState(GameManager.State.InGame);
        ballAnimator.SetBool("inGame", true);
    }
   
}