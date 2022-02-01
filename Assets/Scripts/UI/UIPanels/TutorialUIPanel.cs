using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using TMPro;

public enum GameButtonIcon : int
{
    A = 0,
    B = 1,
    X = 2,
    Y = 3,
    L = 4,
    R = 5
}

[System.Serializable]
public class GameButton
{
    public string name;
    public GameButtonIcon buttonIcon;
}

public class TutorialUIPanel : UIPanel
{
    public TextMeshProUGUI minigameTitle;
    public TextMeshProUGUI minigameCategory;
    public TextMeshProUGUI minigameDescription;
    public Image minigameTutorial;
    public RectTransform minigameControls;

    public bool[] playersReady = new bool[4] { false, false, false, false };

    private bool isTutorial;

    protected override void Awake()
    {
        base.Awake();
    }

    public override void OpenPanel()
    {
        base.OpenPanel();

        Minigame currentMinigame = MinigamesManager.Instance.currentMinigame;

        for (int i = 0; i < 4; i++)
        {
            playersReady[i] = false;
        }

        minigameTitle.SetText(currentMinigame.title);
        minigameCategory.SetText(currentMinigame.category);
        minigameDescription.SetText(currentMinigame.description);
        minigameTutorial.sprite = Resources.Load<Sprite>("Tutorials/" + currentMinigame.tutorial);

        isTutorial = true;
        gameObject.SetActive(true);
    }

    public override void FinishedOpening()
    {
        base.FinishedOpening();
    }

    private void Update()
    {
        if (!isOpened || !isTutorial) return;

        for (int i = 0; i < PlayerManager.Instance.Players.Length; i++)
        {
            if (playersReady[i]) continue;
            if (InputManager.GetButton(PlayerManager.Instance.Players[i], InputButton.A, 0.3f))
            {
                //ready
                playersReady[i] = true;
            }
        }

        if (playersReady.Contains(false)) return;

        isTutorial = false;
        gameObject.SetActive(false);
        //MinigameManager.StartMinigame();
    }
}
