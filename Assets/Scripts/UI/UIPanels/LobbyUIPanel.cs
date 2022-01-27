using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class LobbyUIPanel : UIPanel
{
    private UICharacterSelection[] UICharacterSelections = new UICharacterSelection[4];

    private bool[] readyPlayer = new bool[4] { false, false, false, false };

    private GameObject oldSystem;

    public TextMeshProUGUI commandsText;

    protected override void Awake()
    {
        base.Awake();

        UICharacterSelections = GetComponentsInChildren<UICharacterSelection>();
    }

    private void Update() {
        if (UIManager.Instance.CurrentPanel != this) return;

        RegisterNewPlayer();

        HandlePlayerInput();
    }

    private void RegisterNewPlayer()
    {
        foreach (Gamepad gamepad in Gamepad.all)
        {
            if (gamepad.buttonSouth.wasPressedThisFrame)
            {
                int playerID = PlayerManager.Instance.AddPlayer(gamepad);
                if (playerID == -1) break;
                UICharacterSelections[playerID].RegisterPlayer();
                InputManager.AddButtonTimer(playerID, InputButton.A, 0.3f);
            }
        }
    }

    private void HandlePlayerInput()
    {
        for (int i = 0; i < 4; i++)
        {
            if (PlayerManager.Instance.Players[i] != null)
            {
                if (readyPlayer[i])
                {
                    if (InputManager.GetButton(i, InputButton.B, 0.3f))
                    {
                        UICharacterSelections[i].UnreadyPlayer();
                        readyPlayer[i] = false;
                    }
                }
                else
                {
                    if (InputManager.GetButton(i, InputButton.A, 0.3f))
                    {
                        UICharacterSelections[i].ReadyPlayer();
                        readyPlayer[i] = true;
                    }
                    if (InputManager.GetButton(i, InputButton.B, 0.3f))
                    {
                        if (i == 0)
                        {
                            ClearPlayers();
                            UIManager.Instance.SwitchToPreviousPanel();
                            UIManager.Instance.PlayCameraTrigger("Main");
                            return;
                        }

                        PlayerManager.Instance.RemovePlayer(i);
                        UICharacterSelections[i].UnregisterPlayer();
                    }
                }
            }
        }

        commandsText.SetText("<sprite index= 4>Change Selection  <sprite index= 0>Select");

        for (int i = 0; i < 4; i++)
        {
            if(PlayerManager.Instance.Players[i] != null)
            {
                if (!readyPlayer[i]) return;
            }
        }

        commandsText.SetText("P1 - <sprite index= 3>Start ");

        if (InputManager.GetButton(0, InputButton.Y, 0.3f))
        {
            StartGame();
        }
    }

    private void StartGame()
    {
        List<string> usedCharacters = new();

        for(int i = 0; i < 4; i++)
        {
            if (PlayerManager.Instance.Players[i] == null) continue;

            if (PlayerManager.Instance.Players[i].Character.name != CharacterManager.Instance.RandomCharacter.name) usedCharacters.Add(PlayerManager.Instance.Players[i].Character.name);
        }

        for(int i = 0; i < 4; i++)
        {
            if (PlayerManager.Instance.Players[i] == null) continue;
            if (PlayerManager.Instance.Players[i].Character.name == CharacterManager.Instance.RandomCharacter.name)
            {
                do {
                    PlayerManager.Instance.Players[i].Character = CharacterManager.Instance.Characters[Random.Range(0, CharacterManager.Instance.Characters.Length)];
                } while (usedCharacters.Contains(PlayerManager.Instance.Players[i].Character.name) || PlayerManager.Instance.Players[i].Character.name == CharacterManager.Instance.RandomCharacter.name);

                usedCharacters.Add(PlayerManager.Instance.Players[i].Character.name);
            }
        }

        for(int i = 0; i < 4; i++)
        {
            if (PlayerManager.Instance.Players[i] != null) continue;

            PlayerManager.Instance.AddPlayer(new Gamepad());

            do {
                PlayerManager.Instance.Players[i].Character = CharacterManager.Instance.Characters[Random.Range(0, CharacterManager.Instance.Characters.Length)];
            } while (usedCharacters.Contains(PlayerManager.Instance.Players[i].Character.name) || PlayerManager.Instance.Players[i].Character.name == CharacterManager.Instance.RandomCharacter.name) ;

            usedCharacters.Add(PlayerManager.Instance.Players[i].Character.name);
        }

        LevelManager.Instance.LoadLevel("BoardScene");
        LevelManager.Instance.UnloadLevel("LobbyScene");

        UIManager.Instance.SwitchPanel(DialogueUIPanel.instance);
    }

    public override void OpenPanel()
    {
        base.OpenPanel();
        UICharacterSelections[0].RegisterPlayer();
        UICharacterSelections[0].ChangeCharacter("Random");
        InputManager.AddButtonTimer(0, InputButton.A, 0.3f);
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

        foreach(UICharacterSelection selection in UICharacterSelections)
        {
            selection.eventSystem.gameObject.SetActive(false);
        }

        if (oldSystem) oldSystem.SetActive(true);
    }

    private void ClearPlayers()
    {
        foreach(UICharacterSelection selection in UICharacterSelections)
        {
            selection.UnreadyPlayer();
            selection.UnregisterPlayer();
        }
        PlayerManager.Instance.ClearPlayers();
    }
}