using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondController : MonoBehaviour
{
    [SerializeField]
    private GameObject particle;
    [SerializeField]
    private GameObject diamond;

    private void OnTriggerEnter(Collider Col)
    {
        if (Col.gameObject.tag == "Player")
        {
            print("test");
            particle.SetActive(false);
            diamond.SetActive(true);
            GameController.instance.incrementNumberOfDiamond();
        }


    }
}
