using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class PlayerManager : Singleton<PlayerManager>
{
    public class Player
    {
        private static int currentId = 0;
        public int id;
        public Gamepad gamepad;

        public Player(Gamepad gamepad)
        {
            id = currentId++;
            this.gamepad = gamepad;
        }
    }

    private Player host;
    public Player Host { get => host; }

    private List<Player> players = new();

    public void MakeHost(Gamepad hostGamepad)
    {
        host = new(hostGamepad);
    }
}
