using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIController : MonoBehaviour
{
    [SerializeField]
    private  GameObject mainPanel;
    [SerializeField]
    private Text levelText;
    //private  bool mainPanelIsVisible = false;

    private void Start()
    {
       
    }

    public void setMainPanelEnable( bool value)
    {
        mainPanel.SetActive(value);
    }

    public void setLevelText(int levelNumber)
    {
        levelText.text = "Level " + levelNumber.ToString();
    }

}
