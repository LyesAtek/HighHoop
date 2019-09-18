using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    private GameController gameController;
    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }

    private void OnCollisionEnter(Collision Col)
    {
        if (Col.gameObject.tag == "Player")
        {
            gameController.incrementScore();
        }
    }

}
