using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpController : MonoBehaviour
{
    private bool hasTouched;
    private void OnTriggerEnter(Collider Col)
    {
        if (Col.gameObject.tag == "Player")
        {
            if (!hasTouched)
            {
                GameController.instance.incrementScore();
                hasTouched = true;
            }
        }


    }



}
