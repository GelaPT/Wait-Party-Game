using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueUIPanel : UIPanel
{
    public static DialogueUIPanel instance;

    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI commandsText;
    public Image dialogueBox;
    public Image npcSprite;
    public RectTransform npcSpriteTransform;

    private void Start()
    {
        if (instance == null) instance = this;

        EndDialogue();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        dialogueBox.gameObject.SetActive(true);

        dialogueText.SetText(dialogue.text[0]);

        npcSprite.gameObject.SetActive(true);
        npcSprite.sprite = dialogue.sprite;

        int pos = dialogue.position switch
        {
            DialogueNpcPosition.Left => -540 * (Screen.currentResolution.width / 1920),
            DialogueNpcPosition.Center => 0,
            DialogueNpcPosition.Right => 540 * (Screen.currentResolution.width / 1920),
            _ => 0
        };

        npcSpriteTransform.anchoredPosition = new Vector2(pos, -32);

        if (dialogue.mode == DialogueMode.Player) commandsText.SetText("<sprite index= 0>  Next");
        else commandsText.SetText("");
    }

    public void UpdateDialogue(string text)
    {
        dialogueText.SetText(text);
    }

    public void EndDialogue()
    {
        dialogueText.SetText("");
        dialogueBox.gameObject.SetActive(false);
        commandsText.SetText("");
        npcSprite.gameObject.SetActive(false);
        npcSpriteTransform.anchoredPosition = new Vector2(0, -32);
    }

    public void HideNpc(bool hide)
    {
        npcSprite.gameObject.SetActive(!hide);
    }
}
