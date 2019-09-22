using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{

    private void OnTriggerEnter(Collider Col)
    {
        if (Col.gameObject.tag == "Player"){
			StartCoroutine(WaitForResetLevel(2f,Col));
        }
    }

    IEnumerator WaitForResetLevel(float waitTime, Collider Col)
    {
        GameController.instance.setIsMoving(false);
        yield return new WaitForSeconds(waitTime);
        Col.gameObject.SetActive(true);
        GameController.instance.resetGame();
        
    }
}
