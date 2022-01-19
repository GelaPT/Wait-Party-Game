using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialoguesManager : Singleton<DialoguesManager>
{
    //referencias de componentes
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI commandsText;
    public Image dialogueBox;
    public Image npcSprite;
    public RectTransform npcSpriteTransform;
    public Animator animator;

    //variaveis
    private bool dialogueMode = false; //falso = controlado pela cutscene | true = controlado pelo player
    private int dialogueIndex = 0;
    
    //sprites dos npcs
    public Sprite[] npcSprites;

    //array de dialogo
    [HideInInspector] public string[] dialogue;

    //enumerators
    public enum NpcSprite : int
    {
        Mocho = 0,
        Pontes = 1,
        Cogumelos = 2,
        Jardim = 3,
        Lago = 4,
        Cemitério = 5
    }

    public enum NpcPosition : int
    {
        Left = -540,
        Center = 0,
        Right = 540
    }

    //unity functions
    private void Start()
    {
        dialogueText.SetText("");
        commandsText.SetText("");
        dialogueBox.gameObject.SetActive(false);
        npcSprite.gameObject.SetActive(false);
        npcSprite.gameObject.transform.position = new Vector3(0, -32, 0);
        dialogueIndex = 0;
    }

    //"construtor" de um dialogo
    public void BeginDialogue(string[] newDialogue, NpcSprite npcSpriteIndex, NpcPosition position, bool autoMode)
    {
        //definir variavel dos dialogos
        dialogueIndex = 0;
        dialogueBox.gameObject.SetActive(true);
        dialogue = newDialogue;
        dialogueText.SetText(dialogue[dialogueIndex]);

        //preparar npc sprite
        npcSprite.sprite = npcSprites[(int)npcSpriteIndex];
        npcSpriteTransform.anchoredPosition = new Vector2((int)position, -32);
        //npcSprite.gameObject.transform.position = new Vector3((int)position, -32, 0);
        Debug.Log((int)position);

        //commands text
        if(autoMode) commandsText.SetText("<sprite index= 0>  Next");

        //mode - control mode or auto mode
        dialogueMode = autoMode;
    }

    public void NextDialogue()
    {
        //Função usada no auto mode
        dialogueIndex += 1;

        if (dialogueIndex < dialogue.Length)
            dialogueText.SetText(dialogue[dialogueIndex]);
        else
            EndDialogue();
    }

    public void EndDialogue()
    {
        dialogueMode = false;
        Start();
    }

    public void EnableNpcSprite(bool enable)
    {
        npcSprite.gameObject.SetActive(enable);
    }

    private void Update()
    {
        if (dialogueMode)
        {
            if (InputManager.GetButton(0, InputButton.A))
                dialogueIndex += 1;

            if(dialogueIndex < dialogue.Length)
                dialogueText.SetText(dialogue[dialogueIndex]);
            else
                EndDialogue();
        }
    }
}