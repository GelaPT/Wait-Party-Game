using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System.Collections;

public class MinigamesUIPanel : UIPanel
{
    public RectTransform gridPanel;
    public GameObject minigameButtonPrefab;

    private void Start()
    {
        foreach (Minigame minigame in MinigamesManager.Instance.minigames)
        {
            GameObject minigameButton = Instantiate(minigameButtonPrefab, gridPanel);
            minigameButton.GetComponentInChildren<TextMeshProUGUI>().SetText(minigame.title);
            minigameButton.GetComponent<Button>().onClick.AddListener(() => { 
                MinigamesManager.Instance.LoadMinigame(minigame);
                UIManager.Instance.SwitchPanel("TutorialPanel");
            });
        }
    }

    public override void OpenPanel()
    {
        base.OpenPanel();
        
        Button[] minigamesButton = gridPanel.GetComponentsInChildren<Button>();

        Selectable[,] minigameButtonArray = GetMinigameButtonArray();

        for (int i = 0; i < minigamesButton.Length; i++)
        {
            Navigation minigameNav = new();
            minigameNav.mode = Navigation.Mode.Explicit;

            minigameNav.selectOnUp = GetMinigameButton(minigameButtonArray, minigamesButton[i], "Up");
            minigameNav.selectOnDown = GetMinigameButton(minigameButtonArray, minigamesButton[i], "Down");
            minigameNav.selectOnLeft = GetMinigameButton(minigameButtonArray, minigamesButton[i], "Left");
            minigameNav.selectOnRight = GetMinigameButton(minigameButtonArray, minigamesButton[i], "Right");

            minigamesButton[i].navigation = minigameNav;
        }

        startSelectable = minigameButtonArray[0, 0];
    }

    private void Update()
    {
        if (!isFullyOpened) return;

        EventSystem eventSystem = EventSystem.current;
        Button currentSelected = eventSystem.currentSelectedGameObject.GetComponent<Button>();

        if (InputManager.GetButton(0, InputButton.B, 0.3f))
        {
            UIManager.Instance.SwitchToPreviousPanel();
        }

        if(InputManager.GetButton(0, InputButton.A, 0.3f))
        {
            currentSelected.onClick.Invoke();
            AudioManager.Instance.PlaySound("ui_click");
        }

        if (InputManager.GetAxisAndButton(0, InputAxis.Left, InputAxisDir.N, InputButton.Up, 0.15f))
        {
            eventSystem.SetSelectedGameObject(currentSelected.FindSelectableOnUp()?.gameObject);
            currentSelected = eventSystem.currentSelectedGameObject.GetComponent<Button>();
            AudioManager.Instance.PlaySound("ui_select");
        }

        if (InputManager.GetAxisAndButton(0, InputAxis.Left, InputAxisDir.S, InputButton.Down, 0.15f))
        {
            eventSystem.SetSelectedGameObject(currentSelected.FindSelectableOnDown()?.gameObject);
            currentSelected = eventSystem.currentSelectedGameObject.GetComponent<Button>();
            AudioManager.Instance.PlaySound("ui_select");
        }

        if (InputManager.GetAxisAndButton(0, InputAxis.Left, InputAxisDir.W, InputButton.Left, 0.15f))
        {
            eventSystem.SetSelectedGameObject(currentSelected.FindSelectableOnLeft()?.gameObject);
            currentSelected = eventSystem.currentSelectedGameObject.GetComponent<Button>();
            AudioManager.Instance.PlaySound("ui_select");
        }

        if (InputManager.GetAxisAndButton(0, InputAxis.Left, InputAxisDir.E, InputButton.Right, 0.15f))
        {
            eventSystem.SetSelectedGameObject(currentSelected.FindSelectableOnRight()?.gameObject);
            AudioManager.Instance.PlaySound("ui_select");
        }
        
    }

    private Selectable[,] GetMinigameButtonArray()
    {
        //
        // Obter o numero de colunas e filas
        //

        int width = 1;
        int height = 1;

        RectTransform firstSelectable = gridPanel.GetChild(0) as RectTransform;

        if (firstSelectable == null) return null;

        for (int i = 1; i < gridPanel.childCount; i++)
        {
            RectTransform currentChild = gridPanel.GetChild(i) as RectTransform;

            if (currentChild.anchoredPosition.x == firstSelectable.anchoredPosition.x) height++;
            if (height == 1) width++;
        }

        Selectable[,] selectables = new Selectable[height, width];

        //
        // Passar os objetos para um array bidimensional (comprimento, altura)
        //

        width = 0;
        height = 0;

        selectables[0, 0] = gridPanel.GetChild(0).GetComponent<Selectable>();

        for (int i = 1; i < gridPanel.childCount; i++)
        {
            RectTransform currentChild = gridPanel.GetChild(i) as RectTransform;

            if (currentChild.anchoredPosition.x == firstSelectable.anchoredPosition.x)
            {
                height++;
                width = 0;
            }
            else
            {
                width++;
            }

            selectables[height, width] = currentChild.GetComponent<Selectable>();
        }

        return selectables;
    }

    private Selectable GetMinigameButton(Selectable[,] minigameButtons, Selectable minigameButton, string side)
    {
        for (int h = 0; h < minigameButtons.GetLength(0); h++)
        {
            for (int w = 0; w < minigameButtons.GetLength(1); w++)
            {
                if (minigameButtons[h, w] != minigameButton) continue;

                switch (side)
                {
                    case "Up":
                        if(h == 0) return minigameButtons[minigameButtons.GetLength(0) - 1, w];
                        return minigameButtons[h - 1, w];
                    case "Down":
                        if(h == minigameButtons.GetLength(0) - 1) return minigameButtons[0, w];
                        return minigameButtons[h + 1, w];
                    case "Left":
                        if (w == 0) return minigameButtons[h, minigameButtons.GetLength(1) - 1];
                        return minigameButtons[h, w - 1];
                    case "Right":
                        if (w == minigameButtons.GetLength(1) - 1) return minigameButtons[h, 0];
                        return minigameButtons[h, w + 1];
                }
            }
        }

        return null;
    }
}
