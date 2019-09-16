﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public enum State
    {
        InGame,
        Dead
    }

    private static State ballState;

    public static State GetBallState()
    {
        return ballState;
    }

    public static void SetBallState(State state)
    {
         ballState = state;
    }

    void Start()
    {
        SetBallState(GameManager.State.Dead);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
