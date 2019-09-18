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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            startGame();
        }
        
    }

    #region Game
    public void resetGame()
    {
        resetParametersGame();
        LevelManager.ResetParameters();
    }

    public void startGame()
    {
        uiController.setMainPanelEnable(false);
        ballMovingController.setIsMoving(true);
        playerController.setBallAnimator(true);
        playerController.play();
    }

    public void nextLevel()
    {
        resetParametersGame();
        LevelManager.NextLevel();
        uiController.setLevelText(LevelManager.GetLevelNumber());

    }

    public void buildNewPlatform()
    {
        levelBuilder.generatePlatform();
        levelBuilder.cleanOutPlatforms();
    }

    private void resetParametersGame()
    {
        setIsEndLevel(false);
        ballMovingController.setIsMoving(false);
        uiController.setMainPanelEnable(true);
        setScore(0);
        levelBuilder.buildLevel();
        playerController.resetPosition();
    }
    #endregion

    #region Scores
    public void incrementScore() => ScoreManager.IncrementPoint();//        print(ScoreManager.GetPoint());
    public void addToScore(int value) => ScoreManager.AddToScore(value);
    public void setScore(int value) => ScoreManager.SetPoint(value);
    public void incrementAdditionalScore() => additionalScore += 1;
    public void resetAdditionalScore() => additionalScore = 0;
    #endregion

    #region BallMoving
    public void setIsEndLevel(bool value)
    {
        isEndLevel = value;
        ballMovingController.setIsEndLevel(value);

    }

    public void setIsMoving(bool value) => ballMovingController.setIsMoving(value);

    #endregion

   



}
