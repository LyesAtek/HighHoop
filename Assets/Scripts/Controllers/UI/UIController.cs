using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIController : MonoBehaviour
{
    #region Parameters Main Panel
    [SerializeField]
    private  GameObject mainPanel;
    [SerializeField]
    private Text levelText;
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
    #endregion

    #region Parameters LevelCompleted Panel
    [SerializeField]
    private GameObject levelCompletedPanel;
    [SerializeField]
    private Text levelCompletedText;
    #endregion

    //private  bool mainPanelIsVisible = false;

    private void Start()
    {
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
