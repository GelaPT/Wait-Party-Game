using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable] public class EventGameState : UnityEvent<StateManager.GameState, StateManager.GameState> { }

public class GameManager : Singleton<GameManager>
{
    #region Managers' Instance
    public StateManager stateManager { get; private set; }
    public LevelManager levelManager { get; private set; }
    #endregion


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

        stateManager = StateManager.Instance;
        levelManager = LevelManager.Instance;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        for (var i = 0; i < instancedManagers.Count; i++) Destroy(instancedManagers[i]);
        instancedManagers.Clear();
    }

    public void Pause(bool pause)
    {
        StateManager stateManager = StateManager.Instance;

        if (stateManager.CurrentGameState == StateManager.GameState.PreGame) return;

        stateManager.UpdateState(pause ? StateManager.GameState.Paused : StateManager.GameState.Running);
    }
}