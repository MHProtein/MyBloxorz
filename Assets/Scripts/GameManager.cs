using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    WIN,
    DEAD,
    PLAYING
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameState State { get; private set; }

    private void Awake()
    {
        ChangeGameState(GameState.PLAYING);
        instance = this;
    }

    public void ChangeGameState(GameState newState)
    {
        if (State == newState)
            return;
        State = newState;
        switch (newState)
        {
            case GameState.WIN:
            {
                CubeAppr.instance.isFalling = true;
                break;
            }
            case GameState.DEAD:
                break;
            case GameState.PLAYING:
                break;
        }
    }

    void Start()
    {
        
    }
    
    void Update()
    {
        
    }
}
