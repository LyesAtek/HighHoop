using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private PlayerController playerController;
    private LevelBuilder levelBuilder;
    private UIController uiController;
    private BallMovingController ballMovingController;
	private int additionalScore = 0;
    private bool isEndLevel = false;
	void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        levelBuilder = FindObjectOfType<LevelBuilder>();
        uiController = FindObjectOfType<UIController>();
        ballMovingController = FindObjectOfType<BallMovingController>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isEndLevel)
            {
                uiController.setMainPanelEnable(true);
                setIsEndLevel(false);
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
        LevelManager.ResetParameters();
    }

    public void startGame()
    {
        setIsEndLevel(false);
        ballMovingController.setIsMoving(true);
        playerController.setBallAnimator(true);
        playerController.play();
        uiController.setOnGamePanelEnable(true);
        uiController.setMaxValueSliderScore(LevelManager.GetTargetScore());
        uiController.SetTargerScore(LevelManager.GetTargetScore());
    }

    public void nextLevel()
    {
        resetParametersGame();

        
        LevelManager.NextLevel();
        uiController.setLevelCompletedPanelEnable(true);
        uiController.setLevelText(LevelManager.GetLevelNumber());
        uiController.setLevelCompletedText(LevelManager.GetLevelNumber());
    }

    public void buildNewPlatform()
    {
        levelBuilder.generatePlatform();
        levelBuilder.cleanOutPlatforms();
    }

    //public void 
    private void resetParametersGame()
    {
        
        ballMovingController.setIsMoving(false);
        uiController.setMainPanelEnable(true);
        setScore(0);
        resetAdditionalScore();
        levelBuilder.buildLevel();
        playerController.resetPosition();
    }



    #endregion

    #region Methodes Scores
    public void incrementScore() {
        ScoreManager.IncrementScore();
        uiController.SetCurrentScore(ScoreManager.GetScore());
        uiController.setCurrentValueSliderScore(ScoreManager.GetScore());
      
    }


    public void addToScore()
    {
        ScoreManager.AddToScore(additionalScore);
        uiController.SetCurrentScore(ScoreManager.GetScore());
        uiController.setCurrentValueSliderScore(ScoreManager.GetScore());
    }
    public void setScore(int value)
    {

        ScoreManager.SetScore(value);
    }
    public void incrementAdditionalScore()
    {
        additionalScore += 1;
        

    }
    public void resetAdditionalScore()
    {
        additionalScore = 0;
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

   



}
