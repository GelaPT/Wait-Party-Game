using UnityEngine;
using UnityEngine.Events;

[System.Serializable] public class EventGameState : UnityEvent<StateManager.GameState, StateManager.GameState> { }
[System.Serializable] public class EventBoardState : UnityEvent<StateManager.BoardState, StateManager.BoardState> { }

public class StateManager : Singleton<StateManager>
{
    public enum GameState { PreGame, Running, Paused };
    public enum BoardState { Board, Minigame };

    [HideInInspector] public EventGameState onGameStateChanged;
    [HideInInspector] public EventBoardState onBoardStateChanged;

    private GameState currentGameState = GameState.PreGame;
    public GameState CurrentGameState
    {
        get => currentGameState;
        private set => currentGameState = value;
    }
    private BoardState currentBoardState = BoardState.Board;
    public BoardState CurrentBoardState
    {
        get => currentBoardState;
        private set => currentBoardState = value;
    }

    public void UpdateGameState(GameState newGameState)
    {
        var previousGameState = CurrentGameState;
        CurrentGameState = newGameState;

        onGameStateChanged.Invoke(CurrentGameState, previousGameState);
    }

    public void UpdateBoardState(BoardState newBoardState)
    {
        var previousBoardState = CurrentBoardState;
        CurrentBoardState = newBoardState;

        onBoardStateChanged.Invoke(CurrentBoardState, previousBoardState);
    }
}
