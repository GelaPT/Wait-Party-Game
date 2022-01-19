using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneSignalReciever : MonoBehaviour
{
    DialoguesManager dialogueManager = DialoguesManager.Instance;

    private int dialogueIndex = -1;

    //cutscene dialogues
    public List<string[]> dialogues = new List<string[]>()
    {
        new string[] { "Olá a todos Bem vindos ao Glade Party!",
        "A nossa floresta está em perigo e precisamos de ti para a salvares!" },
        new string[] { "Precisas de ajudar a preservar a floresta e a resolver os desastres naturais que a ameaçam" }
    };

    public void BeginNewDialogue()
    {
        dialogueIndex += 1;
        dialogueManager.BeginDialogue(dialogues[dialogueIndex], DialoguesManager.NpcSprite.Mocho, DialoguesManager.NpcPosition.Center, false);
    }

    public void NextDialogue()
    {
        dialogueManager.NextDialogue();
    }

    public void EndDialogue()
    {
        dialogueManager.EndDialogue();
    }

    public void EnableSprite(bool enable)
    {
        dialogueManager.EnableNpcSprite(enable);
    }
}
