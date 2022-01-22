using UnityEngine;
using System.Collections.Generic;

public enum DialogueMode
{
    None,
    Cutscene,
    Player
}

public enum DialogueNpcPosition
{
    Left,
    Center,
    Right
}

[System.Serializable]
public class Dialogue
{
    public string name;
    public string[] text;
    public DialogueMode mode;
    public DialogueNpcPosition position;
    public Sprite sprite;
}

public class DialoguesManager : Singleton<DialoguesManager>
{
    public Dialogue[] dialogues;
    
    public Dialogue curDialogue = null;
    private int dialogueIndex = 0;
    private int player;

    private void Update()
    {
        if (UIManager.Instance.CurrentPanel != DialogueUIPanel.instance) return;

        if (curDialogue == null) return;

        if (curDialogue.mode != DialogueMode.Cutscene)
        {
            if (InputManager.GetButton(player, InputButton.A, 0.3f))
            {
                NextDialogue();
            }
        }
    }

    public void BeginDialogue(string dialogueName, Player player)
    {
        BeginDialogue(dialogueName, PlayerManager.Instance.GetPlayerID(player));
    }

    public void BeginDialogue(string dialogueName, int player = -1)
    {
        Dialogue dialogue = GetDialogue(dialogueName);
        UIManager.Instance.SwitchPanel(DialogueUIPanel.instance);
        dialogueIndex = 0;
        curDialogue = dialogue;
        DialogueUIPanel.instance.StartDialogue(dialogue);
        this.player = player;
    }

    public void HideNpc(bool hide)
    {
        DialogueUIPanel.instance.HideNpc(hide);
    }

    public void NextDialogue()
    {
        dialogueIndex++;

        if(dialogueIndex < curDialogue.text.Length)
        {
            DialogueUIPanel.instance.UpdateDialogue(curDialogue.text[dialogueIndex]);
            return;
        }

        DialogueUIPanel.instance.EndDialogue();
    }

    public void EndDialogue()
    {
        DialogueUIPanel.instance.EndDialogue();
    }

    public Dialogue GetDialogue(string name)
    {
        foreach (Dialogue dialogue in dialogues)
        {
            if (dialogue.name == name) return dialogue;
        }

        return null;
    }
}