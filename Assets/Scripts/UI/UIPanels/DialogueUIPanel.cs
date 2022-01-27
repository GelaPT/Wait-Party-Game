
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueUIPanel : UIPanel
{
    public static DialogueUIPanel instance;

    [Header("Texts")]
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI commandsText;
    [Header("Images")]
    public Image dialogueBox;
    public Image npcSprite;
    [Header("Transforms")]
    public RectTransform npcSpriteTransform;
    [Header("Animators")]
    public Animator dialogueBoxAnimator;
    public Animator npcSpriteAnimator;
    public Animator textAnimator;

    private void Start()
    {
        if (instance == null) instance = this;

        //EndDialogue();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        dialogueText.SetText(dialogue.text[0]);

        if((int)dialogue.sprite != -1)
        {
            npcSprite.sprite = DialoguesManager.Instance.npcSprites[(int)dialogue.sprite];
            npcSpriteAnimator.SetTrigger("FadeIn");
        }
        else
            npcSprite.gameObject.SetActive(false);

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

        //animator stuff
        dialogueBoxAnimator.SetTrigger("FadeIn");
        npcSpriteAnimator.SetTrigger("FadeIn");
    }

    public void UpdateDialogue(string text)
    {
        dialogueText.SetText("");
        textAnimator.SetTrigger("NextDialogue");
        dialogueText.SetText(text);
    }

    public void HideDialogue(bool hide)
    {
        dialogueBoxAnimator.SetTrigger(hide ? "FadeOut" : "FadeIn");
        if (!hide) textAnimator.SetTrigger("NextDialogue");
    }

    public void EndDialogue()
    {
        //animator stuff
        dialogueBoxAnimator.SetTrigger("FadeOut");
        npcSpriteAnimator.SetTrigger("FadeOut");

        npcSpriteTransform.anchoredPosition = new Vector2(0, -32);
    }

    public void HideNpc(bool hide)
    {
        npcSpriteAnimator.SetTrigger(hide ? "FadeOut" : "FadeIn");
    }
}
