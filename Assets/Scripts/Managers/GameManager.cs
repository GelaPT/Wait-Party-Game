using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public GameObject[] managers;

    private List<GameObject> instancedManagers = new List<GameObject>();

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        InstantiateManagers();

        LevelManager.Instance.LoadLevel("LobbyScene");

        AudioManager.Instance.PlaySound("music_mainmenu");
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
        if (instancedManagers.Count > 0) for (var i = 0; i < instancedManagers.Count; i++) Destroy(instancedManagers[i]);
        instancedManagers.Clear();
    }

    public void ResetGame() {
        LevelManager.Instance.LoadLevel("LobbyScene");

        UIManager.Instance.SwitchPanel("MainPanel");

        UIManager.Instance.ResetCameraAnimator();

        AudioManager.Instance.StopAnySound();

        AudioManager.Instance.PlaySound("music_mainmenu");
    }

    public void Pause(bool pause)
    {
        StateManager stateManager = StateManager.Instance;

        if (stateManager.CurrentGameState == StateManager.GameState.PreGame) return;

        stateManager.UpdateGameState(pause ? StateManager.GameState.Paused : StateManager.GameState.Running);

        if (pause)
        {
            AudioManager.Instance.PauseAnySound();
        }
        else
        {
            AudioManager.Instance.ResumeAnySound();
        }
    }
}