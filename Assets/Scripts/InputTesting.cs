using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player
{
    public static int currentId = 0;
    public GameObject prefab;
    public Gamepad gamepad;
    public int id;

    public static Player GetPlayer(string gamepadName, IEnumerable<Player> players)
    {
        return players.FirstOrDefault(player => player.gamepad.name == gamepadName);
    }
}

public class InputTesting : MonoBehaviour
{
    private List<Player> players = new List<Player>();
    [SerializeField] private GameObject playerPrefab;

    private void Start()
    {
        foreach (Gamepad gamepad in Gamepad.all)
        {
            Player player = new Player
            {
                gamepad = gamepad,
                prefab = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity),
                id = Player.currentId++
            };
            
            players.Add(player);
        }
    }

    /*private void Update()
    {
        foreach (Player player in players)
        {
            if (player.gamepad.leftStick.down.isPressed)
            {
                player.prefab.transform.position += Vector3.down * Time.deltaTime;
            } 
            else if (player.gamepad.leftStick.up.isPressed)
            {
                player.prefab.transform.position += Vector3.up * Time.deltaTime;
            }
            else if (player.gamepad.leftStick.left.isPressed)
            {
                player.prefab.transform.position += Vector3.left * Time.deltaTime;
            }
            else if (player.gamepad.leftStick.right.isPressed)
            {
                player.prefab.transform.position += Vector3.right * Time.deltaTime;
            }
        }
    }*/

    public void MoveCallback(InputAction.CallbackContext callbackContext)
    {
        Player.GetPlayer(callbackContext.control.parent.device, )
        
        Vector2 input = callbackContext.ReadValue<Vector2>();
    }
}