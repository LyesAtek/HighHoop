using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelController : MonoBehaviour
{
    private void OnTriggerEnter(Collider Col)
    {
        if (Col.gameObject.tag == "Player")
        {
            print("levelFinish");
            StartCoroutine(WaitForNextLevel(3f));
        }


    }

    IEnumerator WaitForNextLevel(float waitTime)
    {
        GameController.instance.setIsEndLevel(true);
        GameController.instance.setBlockedUIEvent(true);
        GameController.instance.setLevelCompletedPanel(true);
        yield return new WaitForSeconds(waitTime);
        GameController.instance.nextLevel();
    }
}


