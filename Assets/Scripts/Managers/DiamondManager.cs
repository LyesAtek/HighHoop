using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondManager : MonoBehaviour
{
    public static DiamondManager instance;
    private int numberOfDiamonds;
    
    private void Awake()
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
        if (!PlayerPrefs.HasKey("numberOfDiamonds"))
        {
            PlayerPrefs.SetInt("numberOfDiamonds", 0);

        }
        numberOfDiamonds = PlayerPrefs.GetInt("numberOfDiamonds");
    }

    public void incrementNumberOfDiamonds()
    {
        numberOfDiamonds += 1;
        PlayerPrefs.SetInt("numberOfDiamonds", numberOfDiamonds);
    }

    public int getNumberOfDiamonds()
    {
        return numberOfDiamonds;
    }
}
