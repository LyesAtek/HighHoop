using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance;
    private bool isFirstGame;
    
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
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("isFirstGame"))
        {
            PlayerPrefs.SetInt("isFirstGame", 0);
            isFirstGame = true;
        }
        else
        {
            PlayerPrefs.SetInt("isFirstGame", 1);
            isFirstGame = false;
        }
      
    }

    public bool getIsFirstGame()
    {
        return isFirstGame;
    }

    public void setIsFirstTime()
    {
        PlayerPrefs.SetInt("isFirstGame", 1);
        isFirstGame = false;
        UIController.instance.setVisibleTutorial(false);
    }
   
}
