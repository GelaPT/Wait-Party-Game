using System.Collections.Generic;
using UnityEngine;

public class StateManager : Singleton<StateManager>
{
    public enum GameState { PreGame, Running, Paused };
    [HideInInspector] public EventGameState onGameStateChanged;

    private StateManager.GameState currentGameState = StateManager.GameState.PreGame;
    public StateManager.GameState CurrentGameState
    {
        get => currentGameState;
        private set => currentGameState = value;
    }

    public void UpdateState(GameState newGameState)
    {
        var previousGameState = CurrentGameState;
        CurrentGameState = newGameState;

        onGameStateChanged.Invoke(CurrentGameState, previousGameState);
    }
}
