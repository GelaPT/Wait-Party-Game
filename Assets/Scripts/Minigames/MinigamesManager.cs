using UnityEngine;
using System.Collections.Generic;

public class MinigamesManager : Singleton<MinigamesManager>
{
    public struct Minigame
    {
        public string name;
        public string description;
        public string scene;
    }

    public List<Minigame> minigames = new();

    public void LoadMinigame()
    {

    }

    public void UnloadMinigame()
    {

    }

    public void UpdatePlayerStats()
    {

    }
}