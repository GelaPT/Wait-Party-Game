using UnityEngine.InputSystem;

public class PlayerManager : Singleton<PlayerManager>
{
    public Player Host { get; private set; }

    public Player[] Players { get; private set; } = new Player[4];

    private void LateUpdate()
    {
        for(int i = 0; i < Players.Length; i++)
        {
            if(Players[i] != null)
            {
                if(Players[i].AI)
                {
                    (Players[i] as AIPlayer).InputLateUpdate();
                }
            }
        }
    }

    public void MakeHost(Gamepad hostGamepad)
    {
        Players[0] = new(hostGamepad);
        Host = Players[0];
        Host.ID = 0;
    }

    public void ClearPlayers()
    {
        for(int i = 1; i < Players.Length; i++) {
            Players[i] = null;
        }
    }

    public int AddAI()
    {
        int playerID = GetFreePlayer();

        Players[playerID] = new AIPlayer();
        Players[playerID].AI = true;
        Players[playerID].ID = playerID;

        return playerID;
    }

    public int AddPlayer(Gamepad gamepad)
    {
        if (HasBeenAssigned(gamepad)) return -1;

        int playerID = GetFreePlayer();

        Players[playerID] = new Player(gamepad);
        Players[playerID].ID = playerID;

        return playerID;
    }

    public void RemovePlayer(int playerID)
    {
        Players[playerID] = null;
    }

    public int GetPlayerID(Gamepad gamepad)
    {
        for(int i = 0; i < Players.Length;  i++)
        {
            if (Players[i] == null) continue;
            if (Players[i].Gamepad == gamepad) return i;
        }

        return -1;
    }

    public int GetPlayerID(Player player)
    {
        return GetPlayerID(player.Gamepad);
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
            if (player.Gamepad == gamepad) return true;
        }

        return false;
    }
}