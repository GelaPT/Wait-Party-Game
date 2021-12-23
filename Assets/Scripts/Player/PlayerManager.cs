using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic; 

public class PlayerManager : Singleton<PlayerManager>
{
    private Player host;
    public Player Host { get => host; }

    private List<Player> players = new();

    public void MakeHost(Gamepad hostGamepad)
    {
        host = new(hostGamepad);
        players.Add(host);
    }
}
