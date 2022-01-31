using UnityEngine;
using System.Collections.Generic;

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

    public void LoadMinigame(string scene)
    {
        Debug.Log(scene);
    }

    public void UnloadMinigame()
    {

    }

    /*public void UpdatePlayerStats()
    {

    }*/
}