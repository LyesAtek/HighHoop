using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingChildController : MonoBehaviour
{
    private GameObject parent;
    
    private bool hasTouchUnderRings = false;
	private bool hasEnter = false;
    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent.gameObject;
    }

    private void OnTriggerEnter(Collider Col)
    {
        if (Col.gameObject.tag == "Player")
        {
			if (!hasEnter)
			{
				switch (gameObject.tag)
				{
					case "UnderRing":
						parent.GetComponent<RingController>().childTrigger(true, false);
						break;
					case "LeftSideRing":
						parent.GetComponent<RingController>().childTrigger(false, true);
						break;
					case "RightSideRing":
						parent.GetComponent<RingController>().childTrigger(false, false);
						break;
					default:
						break;
				}
				hasEnter = true;
			}
        }


    }
}
