/*
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;


public class InputTesting : MonoBehaviour
{
    private List<Player> players = new List<Player>();
    [SerializeField] private GameObject playerPrefab;

    private void Start()
    {
        InputSystem.onDeviceChange += (device, change) =>
        {
            Player p = new Player();

            switch (change)
            {
                case InputDeviceChange.Added:
                    p.gamepad = device as Gamepad;
                    p.prefab = Instantiate(playerPrefab, Vector3.zero, quaternion.identity);
                    p.id = Player.currentId++;

                    players.Add(p);
                    break;
                case InputDeviceChange.Removed:
                {
                    p = players.FirstOrDefault(ply => ply.gamepad == device as Gamepad);
                    if (p == null) return;

                    Destroy(p.prefab);
                    players.Remove(p);
                    break;
                }
                case InputDeviceChange.Disconnected:
                    Debug.Log("Device Disconnected");
                    break;
                case InputDeviceChange.Reconnected:
                    Debug.Log("Device Reconnected");
                    break;
                case InputDeviceChange.Enabled:
                    Debug.Log("Device Enabled");
                    break;
                case InputDeviceChange.Disabled:
                    Debug.Log("Device Disabled");
                    break;
                case InputDeviceChange.UsageChanged:
                    Debug.Log("Device Usage Changed");
                    break;
                case InputDeviceChange.ConfigurationChanged:
                    Debug.Log("Device Configuration Changed");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(change), change, null);
            }
        };

        foreach (Gamepad gp in Gamepad.all)
        {
            Player player = new Player
            {
                gamepad = gp,
                prefab = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity),
                id = Player.currentId++
            };
            
            players.Add(player);
        }
    }

    private void Update()
    {
        foreach (Player player in players)
        {
            player.prefab.transform.position += Vector3.Normalize(Vector3.right * player.gamepad.leftStick.x.ReadValue()) * Time.deltaTime;
            
            player.prefab.transform.position += Vector3.Normalize(Vector3.up * player.gamepad.leftStick.y.ReadValue()) * Time.deltaTime;
        }
    }
}*/