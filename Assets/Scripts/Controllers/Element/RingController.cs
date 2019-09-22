using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class RingController : MonoBehaviour
{
   [SerializeField]
    private float yAngle;
    [SerializeField]
    private GameObject particleSystem_1;
    [SerializeField]
    private GameObject particleSystem_2;
    [SerializeField]
    private GameObject textAdditionnalScore;

    private TextMesh textMesh;
    private bool hasContactWithBall = false;
    private bool hasLeftContact = false;
    private bool hasContactWithUnderRing = false;
    private bool hasContactWithSide = false;

    private void Start()
    {
       // textMesh = textAdditionnalScore.GetComponent<TextMesh>();
    }
    // Update is called once per frame
    void Update()
    {
        if (hasContactWithBall)
        {
            if (hasLeftContact)
            {
                transform.Rotate(0f, yAngle * Time.deltaTime , 0f);
            }
            else
            {
                transform.Rotate(0f, - yAngle * Time.deltaTime , 0f);
            }
        }
    }

    public void childTrigger(bool contactWithUnderRing, bool leftContact)
    {
    
        if (contactWithUnderRing && !hasContactWithSide)
        {
            hasContactWithUnderRing = contactWithUnderRing;
            GameController.instance.incrementAdditionalScore();
            GameController.instance.addToScore();
            //textMesh.text = "+" + GameController.instance.getAdditionnalScore().ToString();
            particleSystem_1.SetActive(true);
            particleSystem_2.SetActive(true);
           // textAdditionnalScore.SetActive(true);

        }
        if(!hasContactWithUnderRing)
        {
            hasContactWithSide = true;
            rotateRing(leftContact);
            GameController.instance.resetAdditionalScore();
        }

    }

    void rotateRing(bool value)
    {
       
        hasContactWithBall = true;
        hasLeftContact = value;
    }

  
}
