using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const float initialBallPositionInY = -0.2F;



    [SerializeField]
    private ParticleSystem bounceParticleSystem;
    private bool isCreatedParticle = false;
    private bool grounded = false;
    private string bounceParticleTag;
    private Animator ballAnimator;
    private Animator jumpPlatformAnimator;
    private GameObject parent;
    private CameraController cameraController;
    private int countNumberOfRowsJump = 0;
    
    public enum StateBall
    {
        isMove,
        isFall,
        isIdle
    }
    private StateBall stateBall;

#region Getter Setter StateBall
    public StateBall getStateBall() => stateBall;

    public void setStateBall(StateBall state) => stateBall = state;

    #endregion

    #region Getter IsCreatedparticle
    public bool getIsCreatedParticle() => isCreatedParticle;
    #endregion

    private void Start()
    {
        initializeParameter();
    }

    private void Update()
    {
        if(stateBall == StateBall.isMove)
        {
            if (checkIsBallFalling())
            {
                setBallAnimator(false);
            }


        }
    }

    public void onBallFlying()
    {
        ScoreManager.SetPlatformHasBeenTouched(false);
    }

    public void setBallAnimator(bool value)
    {
        ballAnimator.enabled = value;
    }

    public void resetPosition()
    {
        ballAnimator.SetBool("inGame", false);
        stateBall = StateBall.isIdle;
        parent.transform.position = Vector3.zero;
        transform.position = Vector3.zero;
    }

    public void play()
    {
        setBallAnimator(true);
        ballAnimator.SetBool("inGame", true);
        stateBall = StateBall.isMove;
    }



    private bool checkIsBallFalling()
    {
        if(stateBall == StateBall.isMove && transform.position.y <= initialBallPositionInY && GetComponent<Rigidbody>().velocity.y < 0)
        {
            if (!grounded)
            {
                return true;
            }
        }
        return false;
    }

   private void initializeParameter()
    {
       
        bounceParticleTag = bounceParticleSystem.tag;
        parent = transform.parent.gameObject;
        ballAnimator = GetComponent<Animator>();
        cameraController = FindObjectOfType<CameraController>();
     
    }


    private bool checkIfBallHasAlreadyBounced(string tagParticle)
    {
        if (GameObject.FindGameObjectWithTag(tagParticle)){
            return true;
        }
        return false;
    }

    private void createParticleBounce()
    {
       
        Instantiate(bounceParticleSystem, transform.position, bounceParticleSystem.transform.rotation);
    }


    private void setBallAnimationParameter(string parameter, bool value) => ballAnimator.SetBool(parameter, value);

    private void launchJumpAnimator(GameObject go)
    {
        jumpPlatformAnimator = go.GetComponent<Animator>();
        jumpPlatformAnimator.SetBool("isJump", true);
    }

    private void setCameraControllerParameter()
    {
        cameraController.setBallIsSuperBouncing(ballAnimator.GetBool("isSuperJump"));
    }

   
    #region Ball Collision
    private void OnCollisionEnter(Collision Col)
    {
            if (Col.gameObject.tag == "Ground" && ballAnimator.GetBool("inGame"))
            {
                countNumberOfRowsJump = 0;
                setBallAnimationParameter("isSuperJump", false);
                setBallAnimationParameter("isJump", false);

                setCameraControllerParameter();
               
                if (grounded == false)
                {
                    grounded = true;
                    
                }
                
                if (!checkIfBallHasAlreadyBounced(bounceParticleTag))
                {
                    isCreatedParticle = true;
                    createParticleBounce();
                }
                else
                {
                    isCreatedParticle = false;
                }
            }

            if (Col.gameObject.tag == "Jump")
            {
                launchJumpAnimator(Col.gameObject);
                countNumberOfRowsJump++;
            if (countNumberOfRowsJump <= 1)
            {
                setBallAnimationParameter("isJump", true);
            }
            else
            {
                setBallAnimationParameter("isSuperJump", true);
            }
                setCameraControllerParameter();
           
                if (grounded == false)
                {
                   
                    grounded = true;
                }
            }

        
    }

    private void OnCollisionExit(Collision Col)
    {
        if (Col.gameObject.tag == "Ground" || Col.gameObject.tag == "Jump")
        {
            if (grounded == true)
            {
               
                grounded = false;
            }

        }

    }
   
#endregion
}

