﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTranslateController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameManager.GetBallState() == GameManager.State.InGame)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * 12f);
        }
    }
}
