using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   public enum StateBall
    {
        IsFreezing,
        IsBouncing,
        IsFalling
    }

    private StateBall stateBall;
    [SerializeField]
    private float rayDistance;
    [SerializeField]
    private LayerMask layers;
    [SerializeField]
    private ParticleSystem bounceParticleSystem;

    private bool particleHasCreated = false;
    private bool grounded = false;


    private string bounceParticleTag;
    private Animator ballAnimator;
    private Animator jumpPlatformAnimator;
    private GameObject parent;
    // Start is called before the first frame update
    void Start()
    {
        stateBall = StateBall.IsFreezing;
        bounceParticleTag = bounceParticleSystem.tag;
        parent = transform.parent.gameObject;
        ballAnimator = GetComponent<Animator>();
      
    }

    // Update is called once per frame
    void Update()
    {
        if(checkStateBall() == StateBall.IsFalling)
        {
            ballIsFalling();
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
            grounded = true;
            if (!checkIfParticleHasCreated(bounceParticleTag))
            {
                Instantiate(bounceParticleSystem, transform.position, bounceParticleSystem.transform.rotation);
                
            }
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
            reset();
        }


    }

    private void OnCollisionExit(Collision Col)
    {
        if (Col.gameObject.tag == "Ground")
        {
            grounded = false;
        }


    }


    void ballIsFalling()
    {
        setStateBall(StateBall.IsFalling);
        ballAnimator.enabled = false;
        GameManager.SetBallState(GameManager.State.Dead);
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

    bool checkIfParticleHasCreated(string tagParticle)
    {
        if (GameObject.FindGameObjectWithTag(tagParticle)){
            return true;
        }
        return false;
    }

    StateBall checkStateBall()
    {
        if (GameManager.GetBallState() == GameManager.State.InGame && transform.position.y <= 0 && GetComponent<Rigidbody>().velocity.y < 0)
        {
            RaycastHit hit;
            if (!Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, rayDistance, layers) && !grounded)
            {
                setStateBall(StateBall.IsFalling);
                return StateBall.IsFalling;
              
                //ballIsFalling();
            }
        }else if(GameManager.GetBallState() == GameManager.State.InGame && grounded)
        {
            setStateBall(StateBall.IsBouncing);
            return StateBall.IsBouncing;
        }else if(GameManager.GetBallState() == GameManager.State.Dead)
        {
            setStateBall(StateBall.IsFreezing);
            return StateBall.IsFreezing;
        }
        setStateBall(StateBall.IsFreezing);
        return StateBall.IsFreezing;

    }

    void setStateBall(StateBall value)
    {
        stateBall = value;
    }

}