using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private StateManager stateManager;
    private LevelManager levelManager;

    public GameObject[] managers;

    private List<GameObject> instancedManagers;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        instancedManagers = new List<GameObject>();

        InstantiateManagers();

        stateManager = StateManager.Instance;
        levelManager = LevelManager.Instance;
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
        StateManager stateManager = StateManager.Instance;

        if (stateManager.CurrentGameState == StateManager.GameState.PreGame) return;

        stateManager.UpdateGameState(pause ? StateManager.GameState.Paused : StateManager.GameState.Running);
    }
}