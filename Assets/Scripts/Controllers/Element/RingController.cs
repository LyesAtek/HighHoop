using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingController : MonoBehaviour
{
   [SerializeField]
    private float yAngle;
    private bool hasContactWithBall = false;
    private bool hasLeftContact = false;
    private bool hasContactWithUnderRing = false;
	
	private GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
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
        if (contactWithUnderRing)
        {
            hasContactWithUnderRing = contactWithUnderRing;
            gameController.incrementAdditionalScore();
        }
        if(!hasContactWithUnderRing)
        {
            rotateRing(leftContact);
            gameController.resetAdditionalScore();
        }

    }

    void rotateRing(bool value)
    {
       
        hasContactWithBall = true;
        hasLeftContact = value;
    }

  
}
