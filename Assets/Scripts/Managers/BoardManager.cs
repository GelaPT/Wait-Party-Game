using UnityEngine;

public class BoardManager : Singleton<BoardManager>
{
    private DialoguesManager dialoguesManager;

    public Camera TutorialCamera;
    public Camera BoardCamera;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        dialoguesManager = DialoguesManager.Instance;
    }

    public void BeginTutorial()
    {
        dialoguesManager.BeginDialogue("Tutorial");
    }

    public void StartBoardGame()
    {
        dialoguesManager.EndDialogue();
        TutorialCamera.gameObject.SetActive(false);
        BoardCamera.gameObject.SetActive(true);
        UIManager.Instance.SwitchPanel("BoardPanel");
    }
}