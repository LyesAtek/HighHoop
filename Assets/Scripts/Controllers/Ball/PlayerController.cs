using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const float initialBallPositionInY = -1F;



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
    public bool isAccelerometerMode = false;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    
    public enum StateBall
    {
        isBounce,
        isFall,
        isRoll,
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

    private void launchBouncingAnimation()
    {
        if(grounded)
             ballAnimator.Play("BallBouncing", 0, 0f);
        
    }

    private void Start()
    {
        initializeParameter();
    }
   
    private void FixedUpdate()
    {
        if (ballAnimator.GetBool("inGame") && stateBall == StateBall.isBounce || stateBall == StateBall.isRoll)
        {
            Collider[] colliders = Physics.OverlapSphere(groundCheck.position, groundCheckRadius, whatIsGround);
            if (checkIsBallFalling(colliders))
            {
                setBallAnimator(false);
            }
        }

    }
    public void onBallFlying()
    {
        ScoreManager.SetPlatformHasBeenTouched(false);
    }

    public void OnBallRolling()
    {
        ScoreManager.IncrementScore();
    }

    

    public void setBallAnimator(bool value)
    {
        ballAnimator.enabled = value;
    }

    public void accelerometerMode()
    {
        GamePlayManager.SetISAccelerometerMode(true);
    }

    public void touchMode()
    {
        GamePlayManager.SetISAccelerometerMode(false);
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
        ballAnimator.SetBool("inGame", true);
        stateBall = StateBall.isBounce;
        setBallAnimator(true);
    }



    private bool checkIsBallFalling(Collider[] colliders)
    {
       if(colliders.Length == 1 && colliders[0].tag == "DestroyPlayer")
        {
            return true;
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
                setStateBall(StateBall.isBounce);
                countNumberOfRowsJump = 0;
                setBallAnimationParameter("isSuperJump", false);
                setBallAnimationParameter("isJump", false);
                setBallAnimationParameter("isRoll", false);
                setCameraControllerParameter();
               
               
                if (!checkIfBallHasAlreadyBounced(bounceParticleTag))
                {
                    isCreatedParticle = true;
                    createParticleBounce();
                }
                else
                {
                    isCreatedParticle = false;
                }
            }else if (Col.gameObject.tag == "Jump")
            {
                setBallAnimationParameter("isRoll", false);
                launchJumpAnimator(Col.gameObject);
                countNumberOfRowsJump++;
                 if (countNumberOfRowsJump <= 1)
                 {
                     setBallAnimationParameter("isJump", true);
                 }else
                 {
                    setBallAnimationParameter("isSuperJump", true);
                 }
                setCameraControllerParameter(); 
            }else if (Col.gameObject.tag == "SpecialGround")
             {
                 setBallAnimationParameter("isSuperJump", false);
                 setBallAnimationParameter("isJump", false);
                 countNumberOfRowsJump = 0;
                 setStateBall(StateBall.isRoll);
                
                 setBallAnimationParameter("isRoll", true);
            } 
    }
#endregion
}

