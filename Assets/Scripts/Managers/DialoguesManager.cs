using UnityEngine;
using UnityEngine.Playables;

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
    public PlayableDirector cutsceneDirector;

    [HideInInspector] public Dialogue[] dialogues;
    public Sprite[] npcSprites;
    
    [HideInInspector] public Dialogue curDialogue = null;
    private int dialogueIndex = 0;
    private int player;

    private DialogueUIPanel dialoguePanel;

    protected override void Awake()
    {
        base.Awake();

        dialogues = JsonTools.GetDialogues();
    }

    private void Start()
    {
        dialoguePanel = UIManager.Instance.GetPanel("DialoguePanel") as DialogueUIPanel;
    }

    private void Update()
    {
        if (UIManager.Instance != null && UIManager.Instance.CurrentPanel.name != "DialoguesPanel") return;

        if (curDialogue == null) return;

        if (curDialogue.mode != DialogueMode.Cutscene)
        {
            if (InputManager.GetButton(player, InputButton.A, 0.3f))
            {
                NextDialogue();
            }
        }

        if (cutsceneDirector.time < cutsceneDirector.duration - 1 && InputManager.GetButton(0, InputButton.Y, 0.3f))
        {
            cutsceneDirector.time = cutsceneDirector.duration - 1;
        }
    }

    public void BeginDialogue(string dialogueName, Player player)
    {
        BeginDialogue(dialogueName, PlayerManager.Instance.GetPlayerID(player));
    }

    public void BeginDialogue(string dialogueName, int player = -1)
    {
        Dialogue dialogue = GetDialogue(dialogueName);
        UIManager.Instance.SwitchPanel("DialoguePanel");
        dialogueIndex = 0;
        curDialogue = dialogue;
        dialoguePanel.StartDialogue(dialogue);
        this.player = player;
        HideNpc(false);
    }

    public void HideNpc(bool hide)
    {
        dialoguePanel.HideNpc(hide);
    }

    public void HideDialogue(bool hide)
    {
        dialoguePanel.HideDialogue(hide);
        if (!hide) NextDialogue();
    }

    public void NextDialogue()
    {
        dialogueIndex++;

        if(dialogueIndex < curDialogue.text.Length)
        {
            dialoguePanel.UpdateDialogue(curDialogue.text[dialogueIndex]);
            return;
        }

        dialoguePanel.EndDialogue();
    }

    public void EndDialogue()
    {
        dialoguePanel.EndDialogue();
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