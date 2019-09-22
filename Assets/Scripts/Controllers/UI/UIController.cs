using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIController : MonoBehaviour
{
    public static UIController instance;
    #region Parameters Main Panel
    [SerializeField]
    private  GameObject mainPanel;
    [SerializeField]
    private Text levelText;
    [SerializeField]
    private Text diamondsText;
    #endregion

    #region Parameters OnGame Panel
    [SerializeField]
    private GameObject onGamePanel;
    [SerializeField]
    private Text currentScoreText;
    [SerializeField]
    private Text targetScoreText;
    [SerializeField]
    private Slider sliderScore;
    [SerializeField]
    private GameObject tutorial;
    #endregion

    #region Parameters LevelCompleted Panel
    [SerializeField]
    private GameObject levelCompletedPanel;
    [SerializeField]
    private Text levelCompletedText;
    #endregion

    //private  bool mainPanelIsVisible = false;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        setMainPanelEnable(true);
    }

    
    #region Methodes Main Panel
    public void setMainPanelEnable( bool value)
    {
        mainPanel.SetActive(value);
        onGamePanel.SetActive(!value);
        levelCompletedPanel.SetActive(!value);
    }

    public void setLevelText(int levelNumber)
    {
        levelText.text = "Level " + levelNumber.ToString();
    }

    public void setDiamondText(int diamondNumber)
    {
        diamondsText.text = diamondNumber.ToString();
    }
    #endregion

    #region Methodes OnGame Panel
    public void setOnGamePanelEnable(bool value)
    {
        mainPanel.SetActive(!value);
        onGamePanel.SetActive(value);
        levelCompletedPanel.SetActive(!value);
    }

  
    public void SetCurrentScore(int currentScore)
    {
        currentScoreText.text = currentScore.ToString();
    }

    public void SetTargerScore(int targetScore)
    {
        targetScoreText.text = targetScore.ToString();
    }

    public void setMaxValueSliderScore(int targetScore)
    {
        sliderScore.maxValue = targetScore;
    }

    public void setCurrentValueSliderScore(int currentScore)
    {
        sliderScore.value = currentScore;
    }

    public void setVisibleTutorial(bool value)
    {
        tutorial.SetActive(value);
    }

    #endregion

    #region Methodes LevelCompleted Panel

    public void setLevelCompletedPanelEnable(bool value)
    {
        mainPanel.SetActive(!value);
        onGamePanel.SetActive(!value);
        levelCompletedPanel.SetActive(value);
    }

    public void setLevelCompletedText(int levelNumber)
    {
        levelCompletedText.text = "Level " + levelNumber.ToString() + " Completed!";
    }
    #endregion

}
