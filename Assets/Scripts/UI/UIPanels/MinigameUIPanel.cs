using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameUIPanel : UIPanel
{
    public GameObject PlayerScorePanel;
    public GameObject TutorialPanel;
    public GameObject timer;

    void Start()
    {
        PlayerScorePanel.SetActive(false);
        TutorialPanel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
