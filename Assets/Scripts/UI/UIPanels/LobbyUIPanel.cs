using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class LobbyUIPanel : UIPanel
{
    private UICharacterSelection[] UICharacterSelections = new UICharacterSelection[4];

    private bool[] readyPlayer = new bool[4] { false, false, false, false };
    private float[] playerTimer = new float[4];

    private GameObject oldSystem;

    protected override void Awake()
    {
        base.Awake();

        for(int i = 0; i < playerTimer.Length; i++)
        {
            playerTimer[i] = Time.time;
        }

        UICharacterSelections = GetComponentsInChildren<UICharacterSelection>();
    }

    private void Update() {
        if (UIManager.Instance.CurrentPanel != this) return;

        PlayerManager playerManager = PlayerManager.Instance;

        foreach (Gamepad gamepad in Gamepad.all)
        {
            if(gamepad.buttonSouth.wasPressedThisFrame)
            {
                int playerID = playerManager.AddPlayer(gamepad);
                if (playerID == -1) break;
                UICharacterSelections[playerID].RegisterPlayer();
            }
        }

        for(int i = 0; i < 4; i++)
        {
            if (i == 0)
            {
                if(InputManager.GetButton(i, InputButton.B, 0.3f) && !readyPlayer[0])
                {
                    UIManager.Instance.SwitchToPreviousPanel();
                    UIManager.Instance.PlayCameraTrigger("Main");
                    break;
                }
            }

            if(playerManager.Players[i] != null)
            {
                if(readyPlayer[i])
                {
                    if(InputManager.GetButton(i, InputButton.B, 0.3f))
                    {
                        UICharacterSelections[i].UnreadyPlayer();
                        readyPlayer[i] = false;
                    }
                }
                else
                {
                    if(InputManager.GetButton(i, InputButton.A, 0.3f))
                    {
                        UICharacterSelections[i].ReadyPlayer();
                        readyPlayer[i] = true;
                    }
                    if(InputManager.GetButton(i, InputButton.B, 0.3f))
                    {
                        PlayerManager.Instance.RemovePlayer(i);
                        UICharacterSelections[i].UnregisterPlayer();
                    }
                }
            }
        }
    }

    public override void OpenPanel()
    {
        base.OpenPanel();
        UICharacterSelections[0].RegisterPlayer();
        playerTimer[0] = Time.time + 0.5f;
        oldSystem = EventSystem.current.gameObject;
        oldSystem.SetActive(false);

        foreach (UICharacterSelection selection in UICharacterSelections)
        {
            selection.eventSystem.gameObject.SetActive(true);
        }
    }

    public override void ClosePanel()
    {
        base.ClosePanel();
        ClearPlayers();
        if (oldSystem) oldSystem.SetActive(true);
    }

    private void ClearPlayers()
    {
        foreach(UICharacterSelection selection in UICharacterSelections)
        {
            selection.UnreadyPlayer();
            selection.UnregisterPlayer();
            selection.eventSystem.gameObject.SetActive(false);
        }
        PlayerManager.Instance.ClearPlayers();
    }
}