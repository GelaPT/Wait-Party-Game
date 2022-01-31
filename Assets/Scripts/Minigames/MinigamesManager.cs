using UnityEngine;

[System.Serializable]
public class Minigame
{
    public string scene;
    public string title;
    public string tutorial;
    public string description;
    public string category;
    public GameButton[] buttons;
}

public class MinigamesManager : Singleton<MinigamesManager>
{
    public Minigame[] minigames;
    public Minigame currentMinigame;

    protected override void Awake()
    {
        base.Awake();
        minigames = JsonTools.GetMinigames();
    }

    public void LoadMinigame(Minigame minigame)
    {
        currentMinigame = minigame;

        LevelManager.Instance.LoadLevel(currentMinigame.scene);
        LevelManager.Instance.UnloadLevel("LobbyScene");
    }

    public void UnloadMinigame()
    {
        LevelManager.Instance.LoadLevel("LobbyScene");
        LevelManager.Instance.UnloadLevel(currentMinigame.scene);

        currentMinigame = null;
    }

    /*public void UpdatePlayerStats()
    {

    }*/
}