using UnityEngine;

[System.Serializable]
public enum DialogueMode : int
{
    Cutscene = 0,
    Player = 1
}

[System.Serializable]
public enum DialogueNpcPosition : int
{
    Left = 0,
    Center = 1,
    Right = 2
}

[System.Serializable]
public enum DialogueNpcSprite : int
{
    None = -1,
    Mocho = 0,
    Pontes = 1,
    Cogumelos = 2,
    Jardim = 3,
    Lago = 4,
    Cemiterio = 5
}

[System.Serializable]
public class Dialogue
{
    public string name;
    public string[] text;
    public DialogueMode mode;
    public DialogueNpcPosition position;
    public DialogueNpcSprite sprite;
}

public class DialoguesManager : Singleton<DialoguesManager>
{
    [HideInInspector] public Dialogue[] dialogues;
    public Sprite[] npcSprites;
    
    [HideInInspector] public Dialogue curDialogue = null;
    private int dialogueIndex = 0;
    private int player;

    protected override void Awake()
    {
        base.Awake();

        dialogues = JsonTools.GetDialogues();
    }

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