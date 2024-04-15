using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    WIN,
    DEAD,
    PLAYING,
    AUTOSOLVE
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private GameState lastState;
    public GameState State { get; private set; }

    public delegate void onGameStateChanged(GameState oldState, GameState newState);
    public static event onGameStateChanged OnGameStateChanged;
    
    private void Awake()
    {
        ChangeGameState(GameState.PLAYING);
        instance = this;
    }

    public void ChangeGameState(GameState newState)
    {
        if (State == newState)
            return;
        lastState = State;
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
            case GameState.AUTOSOLVE:
                break;
        }
        OnGameStateChanged?.Invoke(lastState, State);
    }

    void Start()
    {
        
    }
    
    void Update()
    {
        
    }
}
