using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const float initialBallPositionInY = -0.2F;
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
    private LevelBuilder levelBuilder;
    // Start is called before the first frame update
    void Start()
    {
        initializeParameter();
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
            playGame();
        }
        
    }


    void initializeParameter()
    {
        levelBuilder = GameObject.Find("LevelBuilder").GetComponent<LevelBuilder>();
        stateBall = StateBall.IsFreezing;
        bounceParticleTag = bounceParticleSystem.tag;
        parent = transform.parent.gameObject;
        ballAnimator = GetComponent<Animator>();
     
    }

    private void OnCollisionEnter(Collision Col)
    {
        
        if (Col.gameObject.tag == "Ground" && ballAnimator.GetBool("inGame"))
        {
            if (!grounded)
            {
                grounded = true;
            }
           
            if (!checkIfBallHasAlreadyBounced(bounceParticleTag))
            {
                createParticleBounce();
                incrementScore();
            }
            ballAnimator.SetBool("isSuperJump", false);

        }
        else if (Col.gameObject.tag == "Jump")
        {
            jumpPlatformAnimator = Col.gameObject.GetComponent<Animator>();
            jumpPlatformAnimator.SetBool("isJump", true);
            ballAnimator.SetBool("isSuperJump", true);
            grounded = true;

        }
        if (Col.gameObject.tag == "Finish")
        {
            resetGame();
        }


    }

    private void OnCollisionExit(Collision Col)
    {
        if (Col.gameObject.tag == "Ground" || Col.gameObject.tag == "Jump")
        {
            if (grounded)
            {
                grounded = false;
            }
           
        }


    }

    private void OnTriggerEnter(Collider Col)
    {

        if (Col.gameObject.tag == "CheckPoint")
        {
            callLevelBuilderForGeneratePlatform();
        }
    }

    void ballIsFalling()
    {
        ballAnimator.enabled = false;
        GameManager.SetBallState(GameManager.State.Dead);
    }

    void resetGame()
    {
        levelBuilder.buildLevel();
        setScore(0);
        ballAnimator.enabled = true;
        ballAnimator.SetBool("inGame", false);
        parent.transform.position = Vector3.zero;
        transform.position = Vector3.zero;
    }

    void playGame()
    {   
        GameManager.SetBallState(GameManager.State.InGame);
        ballAnimator.SetBool("inGame", true);   
    }

    bool checkIfBallHasAlreadyBounced(string tagParticle)
    {
        if (GameObject.FindGameObjectWithTag(tagParticle)){
            return true;
        }
        return false;
    }

    StateBall checkStateBall()
    {
        if (GameManager.GetBallState() == GameManager.State.InGame && transform.position.y <= initialBallPositionInY && GetComponent<Rigidbody>().velocity.y < 0)
        {
            if ( !grounded)
            {
                setStateBall(StateBall.IsFalling);
                return StateBall.IsFalling;   
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

    void callLevelBuilderForGeneratePlatform()
    {
        levelBuilder.generatePlatform();
        levelBuilder.cleanOutPlatforms();
    }

    void createParticleBounce()
    {
        Instantiate(bounceParticleSystem, transform.position, bounceParticleSystem.transform.rotation);
    }

    void incrementScore()
    {
        ScoreManager.IncrementPoint();
        print(ScoreManager.GetPoint());
    }

    void setScore(int value)
    {
        ScoreManager.SetPoint(value);
    }
}