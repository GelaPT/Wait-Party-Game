using System;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public class Player
{
    public bool AI { get; set; }

    public int ID { get; set; }

    public Gamepad Gamepad { get; set; }

    public Character Character { get; set; }

    public GameObject PlayerGameObject { get; private set; }
    
    public PlayerController PlayerController { get; private set; }

    public CameraController CameraController { get; private set; }

    public Player()
    {
        AI = true;
    }

    public Player(Gamepad gamepad)
    {
        AI = false;
        Gamepad = gamepad;
    }

    public void Spawn<TOne, TTwo>(Transform playerPosition, RuntimeAnimatorController runtimeAnimatorController)
        where TOne : PlayerController
        where TTwo : CameraController
    {
        PlayerGameObject = UnityEngine.Object.Instantiate(Character.characterPlayable, playerPosition.position, playerPosition.rotation);

        PlayerController = PlayerGameObject.AddComponent<TOne>();
        PlayerController.Init(this);
        CameraController = PlayerGameObject.AddComponent<TTwo>();
        CameraController.Init(this);
        PlayerController.Animator = PlayerGameObject.GetComponentInChildren<Animator>();
        PlayerController.Animator.runtimeAnimatorController = runtimeAnimatorController;
    }

    public void Kill()
    {
        UnityEngine.Object.Destroy(PlayerGameObject);
    }
}