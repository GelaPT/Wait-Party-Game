using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic; 

public class PlayerManager : Singleton<PlayerManager>
{
    public Player Host { get; private set; }

    private Player[] players = new Player[4];

    public void MakeHost(Gamepad hostGamepad)
    {
        players[0] = new(hostGamepad);
    }

    public void AddPlayer(Gamepad gamepad)
    {
        int playerID = GetFreePlayer();

        players[playerID] = new Player(gamepad);
    }

    public void RemovePlayer(int playerID)
    {
        players[playerID] = null;
    }

    public int GetFreePlayer()
    {
        for(int i = 0; i < players.Length; i++)
        {
            if (players[i] == null) return i;
        }

        return -1;
    }

    // TODO : Implementar
}