using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    private PlayerController playerController;
    private LevelBuilder levelBuilder;
    private BallMovingController ballMovingController;
	private int additionalScore = 0;
    private bool isEndLevel = false;
	private bool blockedUIEvent = false;
	void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        playerController = FindObjectOfType<PlayerController>();
        levelBuilder = FindObjectOfType<LevelBuilder>();
        ballMovingController = FindObjectOfType<BallMovingController>();
        
    }

    private void Start()
    {
        UIController.instance.setDiamondText(DiamondManager.instance.getNumberOfDiamonds());
    }
    private void Update()
    {

        if (Input.GetMouseButtonDown(0) && !blockedUIEvent)
        {
            if (isEndLevel)
            {
                UIController.instance.setMainPanelEnable(true);
                setIsEndLevel(false);
            }else if (checkPaused())
            {
                ballMovingController.setPaused(false);
                setIsFirstTime();
            }
            else
            {
                startGame();
            }
        } 
    }

    #region Methodes Game
    public void resetGame()
    {
        resetParametersGame();
        LevelManager.instance.ResetParameters();
        UIController.instance.setLevelText(LevelManager.instance.GetLevelNumber());
        UIController.instance.setLevelCompletedText(LevelManager.instance.GetLevelNumber());
    }

    public void startGame()
    {
        playerController.play();
        setIsEndLevel(false);
        ballMovingController.setIsMoving(true);
        playerController.setEnableBallAnimator(true);
        UIController.instance.setOnGamePanelEnable(true);
        UIController.instance.setMaxValueSliderScore(LevelManager.instance.GetTargetScore());
        UIController.instance.SetTargerScore(LevelManager.instance.GetTargetScore());

    }

    public void nextLevel()
    {
        resetParametersGame();
        LevelManager.instance.NextLevel();
        UIController.instance.setLevelText(LevelManager.instance.GetLevelNumber());
        UIController.instance.setLevelCompletedText(LevelManager.instance.GetLevelNumber());
        setBlockedUIEvent(false);
    }

    public void buildNewPlatform()
    {
        levelBuilder.generatePlatform();
        levelBuilder.cleanOutPlatforms();
    }

    //public void 
    private void resetParametersGame()
    {
        UIController.instance.setMainPanelEnable(true);
        ballMovingController.resetVelocity();
        ballMovingController.setIsMoving(false);
        setScore(0);
        resetAdditionalScore();
        levelBuilder.buildLevel();
        playerController.resetPosition();
    }



    #endregion

    #region Methodes Scores
    public void incrementScore() {
        ScoreManager.instance.IncrementScore();
        UIController.instance.SetCurrentScore(ScoreManager.instance.GetScore());
        UIController.instance.setCurrentValueSliderScore(ScoreManager.instance.GetScore());
      
    }


    public void addToScore()
    {
        ScoreManager.instance.AddToScore(additionalScore);
        UIController.instance.SetCurrentScore(ScoreManager.instance.GetScore());
        UIController.instance.setCurrentValueSliderScore(ScoreManager.instance.GetScore());
    }
    public void setScore(int value)
    {

        ScoreManager.instance.SetScore(value);
    }
    public void incrementAdditionalScore()
    {
        additionalScore += 1;
        

    }
    public void resetAdditionalScore()
    {
        additionalScore = 0;
    }

    public int getAdditionnalScore()
    {
        return additionalScore;
    }
    #endregion

    #region Methodes BallMoving
    public void setIsEndLevel(bool value)
    {
        isEndLevel = value;
        ballMovingController.setIsEndLevel(value);
    }
    
    public void setIsMoving(bool value) => ballMovingController.setIsMoving(value);

    #endregion

    #region Methodes Diamond

    public void incrementNumberOfDiamond()
    {
        DiamondManager.instance.incrementNumberOfDiamonds();
        UIController.instance.setDiamondText(DiamondManager.instance.getNumberOfDiamonds());
    }
    #endregion

    #region Methode UI
    public void setLevelCompletedPanel(bool value)
    {
        UIController.instance.setLevelCompletedPanelEnable(value);
    }

    public void setBlockedUIEvent(bool value)
	{
		blockedUIEvent = value;
	}
    #endregion

    #region Methode Tutorial
    public bool checkPaused()
    {
        return ballMovingController.getPaused();
    }
    public void tutorialEnable(bool value)
    {
        UIController.instance.setVisibleTutorial(value);
        ballMovingController.setPaused(value);
    }

    public bool isFirstTime()
    {
        return TutorialManager.instance.getIsFirstGame();
    }

    public void setIsFirstTime()
    {
        TutorialManager.instance.setIsFirstTime();
    }
    #endregion
}
