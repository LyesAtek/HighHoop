using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const float initialBallPositionInY = -1F;



    [SerializeField]
    private ParticleSystem bounceParticleSystem;
    [SerializeField]
    private ParticleSystem rollParticle;
    [SerializeField]
    private ParticleSystem endParticleSystem;
    [SerializeField]
    private ParticleSystem particleDestroy;
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
            checkBallCollision(colliders);
        }
    }
    private void initializeParameter()
    {

        bounceParticleTag = bounceParticleSystem.tag;
        parent = transform.parent.gameObject;
        ballAnimator = GetComponent<Animator>();
        cameraController = FindObjectOfType<CameraController>();

    }


    #region Methodes Call in animation
    public void onBallFlying()
    {
        ScoreManager.instance.SetPlatformHasBeenTouched(false);
    }

    public void OnBallRolling()
    {
		ScoreManager.instance.SetPlatformHasBeenTouched(false);
		GameController.instance.incrementScore();
	}

    public void setRollParticleActive()
    {
        if (!rollParticle.gameObject.activeSelf)
        {
            rollParticle.gameObject.SetActive(true);
        }
    }

    public void setRollParticleInactive()
    {
        if (rollParticle.gameObject.activeSelf)
        {
            rollParticle.gameObject.SetActive(false);
        }
    }
    private void createEndParticleSystem()
    {
        endParticleSystem.gameObject.SetActive(true);
    }

    public void accelerometerMode()
    {
        if (!GameController.instance.checkPaused())
        {
            if (GameController.instance.isFirstTime())
            {
                GameController.instance.tutorialEnable(true);
            }
        }
        GamePlayManager.SetISAccelerometerMode(true);
    }

    public void touchMode()
    {
      
        GamePlayManager.SetISAccelerometerMode(false);
    }

    private void createParticleBounce()
    {
        Vector3 positionParticle = new Vector3(transform.position.x,transform.position.y,transform.position.z - 1f);
        Instantiate(bounceParticleSystem, positionParticle, bounceParticleSystem.transform.rotation);
        if (rollParticle.gameObject.activeSelf)
        {
            rollParticle.gameObject.SetActive(false);
        }
    }
    #endregion


    #region Games

    public void resetPosition()
    {
        endParticleSystem.gameObject.SetActive(false);
        ballAnimator.SetBool("inGame", false);
        stateBall = StateBall.isIdle;
        parent.transform.position = Vector3.zero;
        transform.position = Vector3.zero;
    }

    public void play()
    {
        ballAnimator.SetBool("inGame", true);
        stateBall = StateBall.isBounce;
        setEnableBallAnimator(true);
    }

    #endregion

    #region Methodes Animator
    public void setEnableBallAnimator(bool value)
    {
        ballAnimator.enabled = value;
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
    #endregion


    #region Methode Ball Collision
    private void checkBallCollision(Collider[] colliders)
    {
        
       if(colliders.Length == 1 && colliders[0].tag == "DestroyPlayer" && transform.position.y <=0.1f)
        {
            setEnableBallAnimator(false);
            if (rollParticle.gameObject.activeSelf)
            {
                rollParticle.gameObject.SetActive(false);
            }
            return;
        }

       foreach(Collider col in colliders)
        {
            if(col.gameObject.tag == "Ground")
            {
                ground();
                break;
            }
            if (col.gameObject.tag == "SpecialGround")
            {
                specialGround();
                break;
            }
        }
    }

    private void ground()
    {
        setStateBall(StateBall.isBounce);
        countNumberOfRowsJump = 0;
        setBallAnimationParameter("isSuperJump", false);
        setBallAnimationParameter("isJump", false);
        setBallAnimationParameter("isRoll", false);
        setCameraControllerParameter();
    }

    private void jump()
    {
        setBallAnimationParameter("isRoll", false);

        if (countNumberOfRowsJump <= 1)
        {
            setBallAnimationParameter("isJump", true);
        }
        else
        {
            setBallAnimationParameter("isSuperJump", true);
        }
        setCameraControllerParameter();
    }

    private void specialGround()
    {
        setBallAnimationParameter("isSuperJump", false);
        setBallAnimationParameter("isJump", false);
        countNumberOfRowsJump = 0;
        setStateBall(StateBall.isRoll);
        setBallAnimationParameter("isRoll", true);
    }

    private void ballFalling()
    {
        setEnableBallAnimator(false);
        rollParticle.gameObject.SetActive(false);
    }
  
    private void OnTriggerEnter(Collider Col)
    {
        if (Col.gameObject.tag == "Finish")
        {
            createEndParticleSystem();
        }
        if (Col.gameObject.tag == "Ennemie")
        {
            Instantiate(particleDestroy, transform.position, particleDestroy.transform.rotation);
            gameObject.SetActive(false);
        }
        if (Col.gameObject.tag == "Jump")
        {
            countNumberOfRowsJump++;
            launchJumpAnimator(Col.gameObject);
            jump();

        }
    }
    #endregion


}

