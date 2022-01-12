using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class LobbyUIPanel : UIPanel
{
    private UICharacterSelection[] UICharacterSelections = new UICharacterSelection[4];

    private GameObject oldSystem;

    protected override void Awake()
    {
        base.Awake();

        UICharacterSelections = GetComponentsInChildren<UICharacterSelection>();
    }

    protected override void Update() {
        base.Update();

        foreach (Gamepad gamepad in Gamepad.all)
        {
            if(PlayerManager.Instance.HasBeenAssigned(gamepad)) continue;

            if(gamepad.buttonSouth.wasPressedThisFrame)
            {
                int playerID = PlayerManager.Instance.AddPlayer(gamepad);
                UICharacterSelections[playerID].RegisterPlayer();
            }
        }
    }

    public override void OpenPanel()
    {
        base.OpenPanel();
        UICharacterSelections[0].RegisterPlayer();
    }

    public override void FinishedOpening()
    {
        base.FinishedOpening();

        oldSystem = EventSystem.current.gameObject;
        oldSystem.SetActive(false);
    }

    public override void ClosePanel()
    {
        base.ClosePanel();

        if(oldSystem) oldSystem.SetActive(true);
    }

    public void ChangeCharacter(int playerID, string characterName)
    {
        // Mudar Character de Player
    }
}

// Class character possui sprites/models/texturas/etc.