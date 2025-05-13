using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    //criar estados pra ver se o jogo ainda nao começou, se o jogo ta rodando, se o jogo acabou (influencia o Player)
    public enum GameState {
        WaitingToStart,
        CountdownToStart,
        GameRunning,
        GameOver
    }

    //criar timers pra começar running e pra começar ended
    float waitingToStartTimer = 1f;
    float countdownToStartTimer;
    float gameRunningTimer;
    float countdownToStartMax = 3f;
    float gameRunningTimerMax = 30f;

    public event EventHandler<EventArgs> OnGameStateChanged;

    private GameState gameState;
    

    private void Awake() {
        if(Instance!=null) {
            Debug.LogError($"A game manager is already assigned! Conflicting instance: {gameObject.name}");
        }
        Instance=this;


        countdownToStartTimer=countdownToStartMax;
        gameRunningTimer=gameRunningTimerMax;
        gameState=GameState.WaitingToStart;
    }

    private void Start() {
        GameInput.Instance.OnPause+=GameInput_OnPause;
    }

    private void GameInput_OnPause(object sender, EventArgs e) {
        if(Time.timeScale>0) {
            // Pausa o jogo
            Time.timeScale=0;
            Debug.Log("Game Paused");
        }
        else {
            // Retoma o jogo
            Time.timeScale=1;
            Debug.Log("Game Resumed");
        }
    }

    private void Update() {
        switch(gameState) {
            case GameState.WaitingToStart:
                //change state at the end of the timer  
                waitingToStartTimer-=Time.deltaTime;
                //Debug.Log(waitingToStartTimer);
                if(waitingToStartTimer<=0) {
                    gameState=GameState.CountdownToStart;
                    OnGameStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case GameState.CountdownToStart:
                //change state at the end of the timer
                countdownToStartTimer-=Time.deltaTime;
                //Debug.Log(countdownToStartTimer);
                if(countdownToStartTimer<=0) {
                    gameState=GameState.GameRunning;
                    countdownToStartTimer=countdownToStartMax;
                    OnGameStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case GameState.GameRunning:
                //change state at the end of the timer
                //Debug.Log(gameRunningTimer);
                gameRunningTimer-=Time.deltaTime;
                if(gameRunningTimer<=0) {
                    gameRunningTimer=gameRunningTimerMax;
                    gameState=GameState.GameOver;
                    OnGameStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case GameState.GameOver:
                break;
        }
    }

    public float GetRunningTimer() {
        return countdownToStartTimer;
    }

    public GameState GetGameState() {
        return gameState;
    }

    public bool IsGamePlaying() {
        return gameState==GameState.GameRunning;
    }

    public bool IsGameOnCountdown() {
        return gameState==GameState.CountdownToStart;
    }

    public float GetGameRunningTimerNormalized() {
        return gameRunningTimer/gameRunningTimerMax;
    }
}
