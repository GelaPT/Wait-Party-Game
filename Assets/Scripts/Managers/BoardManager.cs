using UnityEngine;

public class BoardManager : Singleton<BoardManager>
{
    private DialoguesManager dialoguesManager;

    public Camera TutorialCamera;
    public Camera BoardCamera;

    public Transform[] preStartPos;
    public Transform[] startPos;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        dialoguesManager = DialoguesManager.Instance;
        for(int i = 0; i < 4; i++)
        {
            //Player player = PlayerManager.Instance.Players[i];
            //player.Spawn<BoardPlayerController, BoardCameraController>();
        }
    }

    public void BeginTutorial()
    {
        dialoguesManager.BeginDialogue("Tutorial");
    }

    public void StartBoardGame()
    {
        //dialoguesManager.EndDialogue();
        TutorialCamera.gameObject.SetActive(false);
        BoardCamera.gameObject.SetActive(true);
        UIManager.Instance.SwitchPanel("MinigamesPanel");
    }
}