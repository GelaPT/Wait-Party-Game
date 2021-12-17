using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public struct Minigame
{
    public string name;
    public string scene;
}

public class LevelManager : Singleton<LevelManager>
{
    public string bootScene = "BootScene";
    public string boardGame = "BoardScene";
    public List<Minigame> minigames = new();

    private string currentLevel = string.Empty;

    private readonly List<AsyncOperation> loadOperations = new();

    private void Start()
    {
        currentLevel = bootScene;
    }

    private void OnLoadOperationComplete(AsyncOperation ao)
    {
        if(loadOperations.Contains(ao))
        {
            loadOperations.Remove(ao);

            SceneManager.SetActiveScene(SceneManager.GetSceneByName(currentLevel));
        }
    }

    private void OnUnloadOperationComplete(AsyncOperation ao)
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(bootScene));
    }

    public void LoadLevel(string level)
    {
        var ao = SceneManager.LoadSceneAsync(level, LoadSceneMode.Additive);

        if(ao == null)
        {
            Debug.LogError("[LevelManager] Load Scene Failed! " + level);
            return;
        }

        ao.completed += OnLoadOperationComplete;
        loadOperations.Add(ao);
        currentLevel = level;
    }

    public void UnloadLevel(string level)
    {
        var ao = SceneManager.UnloadSceneAsync(level);
        if(ao == null)
        {
            Debug.LogError("[LevelManager] Unload Scene Failed! " + level);
            return;
        }

        ao.completed += OnUnloadOperationComplete;
        currentLevel = bootScene;
    }
}
