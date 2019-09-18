using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelController : MonoBehaviour
{
    private GameController gameController;
    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }


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
        gameController.setIsEndLevel(true);
        yield return new WaitForSeconds(waitTime);
        gameController.nextLevel();
    }
}


