using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable] public class EventGameState : UnityEvent<StateManager.GameState, StateManager.GameState> { }

[System.Serializable]
public struct Manager
{
    public GameObject gameObject;
    public Type type;
}

public class GameManager : Singleton<GameManager>
{
    public GameObject[] managers;

    private List<GameObject> instancedManagers;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        instancedManagers = new List<GameObject>();

        InstantiateManagers();

        Pause(true);
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

    public void Pause(bool pause)
    {
        if (stateManager == null) return;
        if (stateManager.CurrentGameState == StateManager.GameState.PreGame) return;

        stateManager.UpdateState(pause ? StateManager.GameState.Paused : StateManager.GameState.Running);
    }
}