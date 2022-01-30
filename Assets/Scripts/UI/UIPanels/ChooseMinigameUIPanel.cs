using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseMinigameUIPanel : UIPanel
{
    private Minigame[] minigames;
    public RectTransform gridPanelTransform;
    public MinigameButton minigameButtonPrefab;

    private void Start()
    {
        minigames = JsonTools.GetMinigames();
        foreach (Minigame minigame in minigames)
        {
            MinigameButton newMinigameButton = Instantiate(minigameButtonPrefab, gridPanelTransform);
            newMinigameButton.minigameTitle.SetText(minigame.title);
        }
        
        startSelectable = gridPanelTransform.GetChild(0).GetComponent<Button>();

        //Codigo que define os selectables pq essa merda n funciona????????
        //
        //for(int i = 0; i < minigames.Length; i++)
        //{
        //    Navigation customNav = new Navigation();
        //    customNav.mode = Navigation.Mode.Explicit;
        //    if (i == 0)
        //    {
        //        customNav.selectOnUp = gridPanelTransform.GetChild(minigames.Length - 1).GetComponent<Button>();
        //        customNav.selectOnDown = gridPanelTransform.GetChild(i+1).GetComponent<Button>();
        //        gridPanelTransform.GetChild(i).GetComponent<Button>().navigation = customNav;
        //    }
        //    else if(i > 0 && i < minigames.Length - 1)
        //    {
        //        customNav.selectOnUp = gridPanelTransform.GetChild(i - 1).GetComponent<Button>();
        //        customNav.selectOnDown = gridPanelTransform.GetChild(i + 1).GetComponent<Button>();
        //        gridPanelTransform.GetChild(i).GetComponent<Button>().navigation = customNav;
        //    }
        //    else
        //    {
        //        customNav.selectOnUp = gridPanelTransform.GetChild(i - 1).GetComponent<Button>();
        //        customNav.selectOnDown = gridPanelTransform.GetChild(0).GetComponent<Button>();
        //        gridPanelTransform.GetChild(i).GetComponent<Button>().navigation = customNav;
        //    }
        //}
    }
}
