using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class DestroyByTime : MonoBehaviour
{
	
	private float time = 0.3f;

	// Start is called before the first frame update
	void Start()
    {
        Destroy(gameObject, time);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
