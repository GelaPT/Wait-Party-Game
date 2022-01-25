using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialPanel : MonoBehaviour
{
    [System.Serializable]
    public class ControlCard
    {
        public Sprite buttonIcon;
        public string buttonActionText;
    }

    public Image tutorialImage;
    public Sprite tutorialSprite;

    public TextMeshProUGUI tutorialText;
    public string tutorialString;

    public ControlsCard controlCardPrefab;
    public ControlCard[] controls;
    public RectTransform controlPanelParent;
    void Start()
    {
        foreach (ControlCard card in controls)
        {
            ControlsCard newControlCard = Instantiate(controlCardPrefab, controlPanelParent);
            newControlCard.buttonIcon.sprite = card.buttonIcon;
            newControlCard.buttonActionText.SetText(card.buttonActionText);
        }

        tutorialImage.sprite = tutorialSprite;
        tutorialText.SetText(tutorialString);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
