using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic; 

public class PlayerManager : Singleton<PlayerManager>
{
    public Player Host { get; private set; }

    public Player[] Players { get; private set; } = new Player[4];

    public void MakeHost(Gamepad hostGamepad)
    {
        Players[0] = new(hostGamepad);
        Host = Players[0];
    }

    public int AddPlayer(Gamepad gamepad)
    {
        if (HasBeenAssigned(gamepad)) return -1;

        int playerID = GetFreePlayer();

        Players[playerID] = new Player(gamepad);

        return playerID;
    }

    public void RemovePlayer(int playerID)
    {
        Players[playerID] = null;
    }

    public int GetFreePlayer()
    {
        for(int i = 0; i < Players.Length; i++)
        {
            if (Players[i] == null) return i;
        }

        return -1;
    }

    public bool HasBeenAssigned(Gamepad gamepad)
    {
        if (Host == null) return false;

        foreach(Player player in Players)
        {
            if (player == null) continue;
            if (player.gamepad == gamepad) return true;
        }

        return false;
    }

    // TODO : Implementar
}