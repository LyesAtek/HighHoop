using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPlayerController : MonoBehaviour
{
   
    public GameObject player;
    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, player.transform.position.z + offset.z);
    }

    private void OnTriggerEnter(Collider Col)
    {
        if (Col.gameObject.tag == "Player")
        {
            StartCoroutine(WaitForResetLevel(2f));
        }
    }

    IEnumerator WaitForResetLevel(float waitTime)
    {
        GameController.instance.setIsMoving(false) ;
        yield return new WaitForSeconds(waitTime);
        GameController.instance.resetGame();
    }
}
