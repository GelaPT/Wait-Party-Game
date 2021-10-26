using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputTesting : MonoBehaviour
{
    private List<Gamepad> gamepads = new List<Gamepad>();
    
    private void Update()
    {
        Gamepad gamepad = Gamepad.current;
        
        if (gamepads.Contains(gamepad)) return;
        
        gamepads.Add(gamepad);
        Debug.Log($"Added gamepad {gamepad.name}");
    }
}
