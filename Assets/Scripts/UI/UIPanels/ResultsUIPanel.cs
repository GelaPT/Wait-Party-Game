using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultsUIPanel : UIPanel
{
    public Camera[] renderTargetCameras;

    public List<MinigameManager.MinigameStats> stats;

    public UICharacterParent[] UICharacterParents;

    public TextMeshProUGUI[] scores;
    public TextMeshProUGUI[] place;
    public TextMeshProUGUI[] prize; // 10 - 7 - 5 - 3

    public override void OpenPanel()
    {
        base.OpenPanel();

        for (int i = 0; i < 4; i++)
        {
            foreach (var stat in stats)
            {
                if(stat.place - 1 == i)
                    UICharacterParents[i].ChangeModel(PlayerManager.Instance.Players[stat.player.ID].Character.characterUI);
            }
        }
        
        for (int i = 0; i < 4; i++)
        {
            if (stats[i].place == 1) UICharacterParents[i].PlayAnimation(true);
            if (stats[i].place != 1) UICharacterParents[i].PlayAnimation(false);
            scores[i].SetText(stats[i].points.ToString());
            place[i].SetText(stats[i].place switch
            {
                1 => "1st",
                2 => "2nd",
                3 => "3rd",
                4 => "4th",
                _ => "Xº"
            });
            prize[i].SetText(stats[i].place switch
            {
                1 => "<color=#F4F09C>+10</color><size=60%><sprite index= 14>",
                2 => "<color=#F4F09C>+7</color><size=60%><sprite index= 14>",
                3 => "<color=#F4F09C>+5</color><size=60%><sprite index= 14>",
                4 => "<color=#F4F09C>+3</color><size=60%><sprite index= 14>",
                _ => "+X"
            });
            renderTargetCameras[i].enabled = true;
        }
    }

    public override void ClosePanel()
    {
        base.ClosePanel();

        for(int i = 0; i < 4; i++) renderTargetCameras[i].enabled = false;
    }

    private void Update()
    {
        if (!isOpened) return;

        if(InputManager.GetButton(0, InputButton.Y))
        {
            MinigamesManager.Instance.UnloadMinigame();

            UIManager.Instance.SwitchPanel("MainPanel");
        }
    }
}
