using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable] public class EventGameState : UnityEvent<GameManager.GameState, GameManager.GameState> { }

public class GameManager : Singleton<GameManager>
{
    public GameObject[] managers;

    public enum GameState { PreGame, Running, Paused };
    [HideInInspector] public EventGameState onGameStateChanged;

    private GameState currentGameState = GameState.PreGame;
    public GameState CurrentGameState
    {
        get => currentGameState;
        private set => currentGameState = value;
    }

    private List<GameObject> instancedManagers;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        instancedManagers = new List<GameObject>();

        InstantiateManagers();
    }

    private void InstantiateManagers()
    {
        GameObject managerInstance;
        for(var i = 0; i < managers.Length; i++)
        {
            managerInstance = Instantiate(managers[i]);
            instancedManagers.Add(managerInstance);
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        for (var i = 0; i < instancedManagers.Count; i++) Destroy(instancedManagers[i]);
        instancedManagers.Clear();
    }
}